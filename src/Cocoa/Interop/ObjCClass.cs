using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cocoa {
	public class ObjCClass {
		public static Hashtable objc_classes;
		public static Hashtable native_classes;
		private IntPtr native_pointer;
		private objc_class native_class;

		static ObjCClass () {
			objc_classes = new Hashtable ();
			native_classes = new Hashtable ();
		}

		public ObjCClass (objc_class native_class, IntPtr native_pointer) {
			this.native_class = native_class;
			this.native_pointer = native_pointer;
		}

		public objc_class ToNativeClass () {
			return native_class;
		}

		public IntPtr ToIntPtr () {
			return native_pointer;
		}

		public static Type TypeForIntPtr (IntPtr cls) {
			if (cls == IntPtr.Zero)
				return typeof (void);

			string class_name = (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (cls, "className", typeof (IntPtr)), "cString", typeof (string));

			if (class_name.StartsWith ("%")) class_name = class_name.Substring (1);

			Type type = TypeForClassname (class_name);

			IntPtr superclass = (IntPtr)ObjCMessaging.objc_msgSend (cls, "superclass", typeof (IntPtr));

			while (type == null) {
				class_name = (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (superclass, "className", typeof (IntPtr)), "cString", typeof (string));
				
				if (class_name == null || class_name == "")
					throw new InvalidOperationException ("There is no managed type reflecting: " + (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (cls, "className", typeof (IntPtr)), "cString", typeof (string)));

				type = TypeForClassname (class_name);

				superclass = (IntPtr)ObjCMessaging.objc_msgSend (superclass, "superclass", typeof (IntPtr));
			}

			return type;
		}

		public static Type TypeForClassname (string class_name) {
			if (class_name.StartsWith ("NS") || class_name.StartsWith ("CS"))
				class_name = class_name.Substring (2);

			if (native_classes.ContainsKey (class_name))
				return (Type) native_classes [class_name];

			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies ()) {
				if (assembly is System.Reflection.Emit.AssemblyBuilder)
					continue;

				foreach (Type type in assembly.GetTypes ()) {
					if (type == typeof (Object) || type.IsSubclassOf (typeof (Object))) {
						RegisterAttribute attr = (RegisterAttribute) Attribute.GetCustomAttribute (type, typeof (RegisterAttribute));
						if (attr == null) {
							native_classes [type.Name] = type;
						} else {
							ObjCClass.FromType (type);
							native_classes [attr.Name] = type;
						}
					}
				}
			}

			if (native_classes.Contains (class_name))
				return (Type) native_classes [class_name];
			
			return null;
		}

		public static ObjCClass FromObject (Object toconvert) {
			return FromType (toconvert.GetType ());
		}

		public static ObjCClass FromType (Type type) {
			if (type == null)
				throw new ArgumentException ();

			if (objc_classes [type] != null) 
				return (ObjCClass) objc_classes [type];

			FieldInfo class_name = type.GetField ("ObjectiveCName", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			FieldInfo base_name = type.BaseType.GetField ("ObjectiveCName", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

			string cls_name = type.Name;
			string super_name = type.BaseType.Name;

			if (class_name != null)
				cls_name = (string)class_name.GetValue (type);

			RegisterAttribute attr = (RegisterAttribute) Attribute.GetCustomAttribute (type, typeof (RegisterAttribute));
			
			if (attr != null)
				cls_name = attr.Name;

			IntPtr this_ptr = ObjCMethods.objc_lookUpClass (cls_name);
			if (this_ptr != IntPtr.Zero)
				return new ObjCClass ((objc_class) Marshal.PtrToStructure (this_ptr, typeof (objc_class)), this_ptr);

			if (base_name != null)
				super_name = (string)base_name.GetValue (type.BaseType);

			if (super_name == "CSObject" || super_name == "Object")
				super_name = "NSObject";
			
			if (cls_name == "CSThread")
				super_name = "NSThread";

			IntPtr super_ptr = ObjCMethods.objc_getClass (super_name);

			if (super_ptr == IntPtr.Zero)
				return null;

			objc_class super_class = (objc_class) Marshal.PtrToStructure (super_ptr, typeof (objc_class));

			objc_class root_class = RootClass (super_class);
			objc_class objc_class = new objc_class ();
			objc_class meta_class = new objc_class ();

			// setup the meta class
			meta_class.isa = root_class.isa;
			meta_class.super_class = super_class.isa;
			meta_class.instance_size = ((objc_class) Marshal.PtrToStructure (super_class.isa, typeof (objc_class))).instance_size;
			meta_class.name = Marshal.StringToHGlobalAnsi (cls_name);
			meta_class.info = 2;
			meta_class.methodLists = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (IntPtr)));
			Marshal.WriteIntPtr (meta_class.methodLists, (IntPtr) (-1));

			IntPtr meta_class_ptr = Marshal.AllocHGlobal (Marshal.SizeOf (objc_class));
			Marshal.StructureToPtr (meta_class, meta_class_ptr, true);

			// setup the class
			objc_class.isa = meta_class_ptr;
			objc_class.name = meta_class.name;
			objc_class.info = 1;
			objc_class.super_class = super_ptr;
			objc_class.methodLists = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (IntPtr)));
			Marshal.WriteIntPtr (objc_class.methodLists, (IntPtr) (-1));

			// Add the ivars
			ArrayList ivars = new ArrayList ();
			foreach (FieldInfo field in type.GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				ConnectAttribute cattr = (ConnectAttribute) Attribute.GetCustomAttribute (field, typeof (ConnectAttribute));
				if (cattr != null) {
					ivars.Add (field);
				}
			}
			if (ivars.Count > 0) {
				objc_ivar_list ivar_list = new objc_ivar_list ();
				ivar_list.count = ivars.Count;
				
				IntPtr ivar_list_ptr = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (objc_ivar_list)) + (ivars.Count * Marshal.SizeOf (typeof (objc_method))));
				int ivar_offset = super_class.instance_size;
				for (int i = 0; i < ivars.Count; i++) { 
					ConnectAttribute cattr = (ConnectAttribute) Attribute.GetCustomAttribute ((FieldInfo) ivars [i], typeof (ConnectAttribute));
					objc_ivar ivar = new objc_ivar ();
					int ivar_size = cattr.Size;
					ivar.ivar_name = Marshal.StringToHGlobalAnsi (cattr.Name != null ? cattr.Name : ((FieldInfo) ivars [i]).Name); 
					ivar.ivar_type = Marshal.StringToHGlobalAnsi (cattr.Type != null ? cattr.Type : ObjCTypes.FromType (((FieldInfo) ivars [i]).FieldType, out ivar_size));   
					ivar.ivar_offset = ivar_offset;
					ivar_offset += ivar_size;
					
					Marshal.StructureToPtr (ivar, (IntPtr) ((long) ivar_list_ptr + Marshal.SizeOf (typeof (objc_ivar_list)) + (i * Marshal.SizeOf (typeof (objc_ivar)))), true);
				}
	
				Marshal.StructureToPtr (ivar_list, ivar_list_ptr, false);
				objc_class.instance_size = ivar_offset;
				objc_class.ivars = ivar_list_ptr;
			} else {
				objc_class.instance_size = super_class.instance_size;
			}

			// Register the class
			IntPtr objc_class_ptr = Marshal.AllocHGlobal (Marshal.SizeOf (objc_class));
			Marshal.StructureToPtr (objc_class, objc_class_ptr, true);
			ObjCMethods.objc_addClass (objc_class_ptr);

			// Add the methods
			objc_method_list method_list = new objc_method_list ();
			ArrayList methods = new ArrayList ();
			foreach (MethodInfo method in type.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) { 
				if (type.Namespace != "Cocoa") {
					ExportAttribute eattr = (ExportAttribute) Attribute.GetCustomAttribute (method, typeof (ExportAttribute));
					if (eattr != null)
						methods.Add (ObjCMethod.FromMethodInfo (method));
				} else {
					methods.Add (ObjCMethod.FromMethodInfo (method));
				}
			}
// FIXME
//			foreach (ConstructorInfo constructor in type.GetConstructors (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
//				methods.Add (ObjCMethod.FromConstructorInfo (constructor));
			
			method_list.count = methods.Count;

			IntPtr method_list_ptr = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (objc_method_list)) + (methods.Count * Marshal.SizeOf (typeof (objc_method))));
			
			Marshal.StructureToPtr (method_list, method_list_ptr, false);

			for (int i = 0; i < methods.Count; i++) {
				Marshal.StructureToPtr ((objc_method)methods [i], (IntPtr) ((long) method_list_ptr + Marshal.SizeOf (typeof (objc_method_list)) + (i * Marshal.SizeOf (typeof (objc_method)))), true);
			}

			ObjCMethods.class_addMethods (objc_class_ptr, method_list_ptr);

			// Add the static methods
			method_list = new objc_method_list ();
			methods = new ArrayList ();
			foreach (MethodInfo method in type.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)) { 
				if (type.Namespace != "Cocoa") {
					ExportAttribute eattr = (ExportAttribute) Attribute.GetCustomAttribute (method, typeof (ExportAttribute));
					if (eattr != null)
						methods.Add (ObjCMethod.FromMethodInfo (method));
				} else {
					methods.Add (ObjCMethod.FromMethodInfo (method));
				}
			}
			
			method_list.count = methods.Count;

			IntPtr static_method_list_ptr = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (objc_method_list)) + (methods.Count * Marshal.SizeOf (typeof (objc_method))));
			
			Marshal.StructureToPtr (method_list, static_method_list_ptr, false);

			for (int i = 0; i < methods.Count; i++) {
				Marshal.StructureToPtr ((objc_method)methods [i], (IntPtr) ((long) static_method_list_ptr + Marshal.SizeOf (typeof (objc_method_list)) + (i * Marshal.SizeOf (typeof (objc_method)))), true);
			}

			ObjCMethods.class_addMethods (meta_class_ptr, static_method_list_ptr);

			return new ObjCClass (objc_class, objc_class_ptr);
		}
			
		private static objc_class RootClass (objc_class super_class) {
			objc_class root_class = super_class;

			while (root_class.super_class != IntPtr.Zero) {
				root_class = (objc_class) Marshal.PtrToStructure (root_class.super_class, typeof (objc_class));
			}

			return root_class;
		}
	}
}
		
