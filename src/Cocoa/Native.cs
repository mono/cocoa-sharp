#define DEBUG 

using System;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;

namespace Cocoa {

	public class Native {
		protected static IDictionary RegisteredClasses = new Hashtable ();
		protected static IDictionary ClassTypeMapping = new Hashtable ();
		protected static IDictionary ConstructorInfoCache = new Hashtable ();

		public static ClassHandlerDelegate class_handler;
		public static IMPDelegate implement_method;
		public static IMPDelegate construct_object;

		private static IntPtr init_ptr;
		private static IntPtr size_init_ptr;

                public delegate IntPtr IMPDelegate (IntPtr cls, IntPtr sel, StackPadding args);
		public delegate int ClassHandlerDelegate (string classname);

#if DEBUG
		private static int CacheHits = 0;
		private static int CacheHitBadType = 0;
		private static int ConstructorHits = 0;
		private static int NativeToManagedHits = 0;
		private static int ImplementMethodHits = 0;
		private static int ConstructManagedObjectHits = 0;
		private static int TypeWalks = 0;
		private static IDictionary WalkedTypes = new Hashtable ();

		private static Thread debug_thread;

		static void DebugThread () {
			while (true) {
				Console.WriteLine ("Runtime Performance:");
				Console.WriteLine ("\tCacheHits: {0}", CacheHits);
				Console.WriteLine ("\tCacheHitBadType: {0}", CacheHitBadType);
				Console.WriteLine ("\tConstructorHits: {0}", ConstructorHits);
				Console.WriteLine ("\tNativeToManagedHits: {0}", NativeToManagedHits);
				Console.WriteLine ("\tImplementMethodHits: {0}", ImplementMethodHits);
				Console.WriteLine ("\tConstructManagedObjectHits: {0}", ConstructManagedObjectHits);
				Console.WriteLine ("\tTypeWalks: {0}", TypeWalks);
				foreach (string key in WalkedTypes.Keys)
					Console.WriteLine ("\t\t{0}: {1}", key, WalkedTypes [key]);
				System.Threading.Thread.Sleep (5000);
			}
		}
#endif
		static Native () {
#if DEBUG
			debug_thread = new Thread (new ThreadStart (DebugThread));
			debug_thread.Start ();
#endif

			class_handler = new ClassHandlerDelegate (ClassHandler);

			implement_method = new IMPDelegate (ImplementMethod);
			construct_object = new IMPDelegate (ConstructManagedObject);
			
			init_ptr = Marshal.StringToCoTaskMemAnsi ("init");
			size_init_ptr = Marshal.StringToCoTaskMemAnsi ("@8@0:4");

			// UGLY HACKS: This causes delegate_trampoline to get created
			memcmp (implement_method, implement_method, 0);
			memcmp (construct_object, construct_object, 0);

			// Install the class handler
			objc_setClassHandler (class_handler);

			// Disable Mach Exceptions
			Mach.InstallExceptionHandler ();
		}

		public static int ClassHandler (string classname) {
			try {
				Type type = NativeClassToManagedType (classname);
				if (type == null)
					return 0;
	
				RegisterClass (type);
				return 1;
			} catch {
				return 0;
			}
		}

		// StackPadding is another horrible hack to let us do varargs
		public static IntPtr ImplementMethod (IntPtr cls, IntPtr sel, StackPadding args) {
#if DEBUG
			ImplementMethodHits ++;
#endif

			IntPtr native_method = class_getInstanceMethod ((IntPtr)ObjCMessaging.objc_msgSend (cls, "class", typeof (IntPtr)), sel);
			int num_arguments = method_getNumberOfArguments (native_method);
			ArrayList arguments = new ArrayList ();
			string selector = Marshal.PtrToStringAuto (sel);
			Object item = (Object)NativeToManaged (cls);
			Type type = item.GetType ();
			string method = SelectorToMethod (item.GetType (), selector); 
			bool method_invoke = true;


			ParameterInfo [] parameters = null;
			
			if (method != null && type.GetMethod (method) != null)
				parameters = type.GetMethod (method).GetParameters (); 
			else {
				foreach (ConstructorInfo constructor in type.GetConstructors (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)) {
					foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (constructor, typeof (ExportAttribute))) {
						string ctor_selector = "";
						if (export_attribute.Selector != null)
							ctor_selector = export_attribute.Selector;
						else
							try { ctor_selector = ConstructorToSelector (constructor); } catch {}

						if (selector == ctor_selector) {
							method_invoke = false;
							parameters = constructor.GetParameters ();
						}
					}
				}
				if (parameters == null)
					throw new Exception ("Unknown what to invoke for: " + selector);
			}

			if (parameters.Length != (num_arguments > 0 ? num_arguments - 2 : 0))
				throw new Exception ("parameters.Length != num_arguments (" + parameters.Length + ":" + num_arguments + ")");

			unsafe {
				int argptr = (int)(&sel) + 4;
				for (int i = 0; i < num_arguments - 2; i++) {
					if (parameters [i].ParameterType.IsPrimitive || parameters [i].ParameterType.IsValueType) {
						arguments.Add (Marshal.PtrToStructure ((IntPtr)argptr, parameters [i].ParameterType));
					} else {
						try {
							arguments.Add (NativeToManaged (Marshal.ReadIntPtr ((IntPtr)argptr)));
						} catch {
							Console.WriteLine ("Couldn't load argument type {0}: ({1:x} {2:x})", parameters [i].ParameterType, (int)argptr, Marshal.ReadIntPtr ((IntPtr)argptr));
							arguments.Add (null);
						}
					}
					argptr += Marshal.SizeOf (typeof (IntPtr));
				}
			}

			object return_value = null;

			if (method_invoke)
				return_value = Native.InvokeMethod (item, selector, (object [])arguments.ToArray (typeof (object)));
			else
				return_value = Native.InvokeConstructor (type, selector, (object [])arguments.ToArray (typeof (object)));
			if (return_value == null)
				return IntPtr.Zero;
			if (return_value is Cocoa.Object)
				return ((Cocoa.Object)return_value).NativeObject;
			if (return_value is System.Int32)
				return (IntPtr)(int)return_value;
			if (return_value is System.Boolean)
				return ((bool)return_value ? (IntPtr)1 : (IntPtr)0);
			if (return_value is System.IntPtr)
				return (IntPtr)return_value;
	
			throw new Exception ("Unimplmented return type: " + return_value.GetType ());
		}

		public static IntPtr ConstructManagedObject (IntPtr cls, IntPtr sel, StackPadding args) {
#if DEBUG
			ConstructManagedObjectHits ++;
#endif
			Object.ManagedObjects.Add (cls, NativeToManaged (cls));
			return cls;
		}
		
		internal static Type NativeClassToManagedType (string classname) {
			//FIXME: What if my external class is called NSTFQWERT
			if (classname.StartsWith ("NS")) classname = classname.Substring (2);

			if (ClassTypeMapping.Contains (classname))
				return (Type) ClassTypeMapping [classname];

			ArrayList assemblies = new ArrayList ();
			assemblies.Add (Assembly.GetEntryAssembly ());
			foreach (AssemblyName assemblyname in Assembly.GetEntryAssembly ().GetReferencedAssemblies ())
				assemblies.Add (Assembly.Load (assemblyname));

			foreach (Assembly assembly in assemblies) {
				try {
					foreach (Type type in assembly.GetTypes ()) {
						if (type.IsClass && (type == typeof (Object) || type.IsSubclassOf (typeof (Object)))) {
							bool added_by_attribute = false;
							foreach (RegisterAttribute register_attribute in Attribute.GetCustomAttributes (type, typeof (RegisterAttribute))) {
								if (register_attribute.Name != null) {
									ClassTypeMapping [register_attribute.Name] = type;
									added_by_attribute = true;
								}
								break;
							}
							if (!added_by_attribute)
								ClassTypeMapping [type.Name] = type;
						}
					}
				} catch {}
			}

			if (ClassTypeMapping.Contains (classname))
				return (Type) ClassTypeMapping [classname];

			return null;
		}

		public static IntPtr RegisterClass (Type type) {
			if (RegisteredClasses.Contains (type))
				return (IntPtr) RegisteredClasses [type];

			if (type == null)
				throw new ArgumentException ("Attempting to register a null type");

			FieldInfo class_name = type.GetField ("ObjectiveCName", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			FieldInfo base_class_name = type.BaseType.GetField ("ObjectiveCName", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			string name = type.Name;
			if (class_name != null)
				name = (string)class_name.GetValue (type);

			string base_name = type.BaseType.Name;
			if (base_class_name != null)
				base_name = (string)base_class_name.GetValue (type.BaseType);

			IntPtr super_class = objc_lookUpClass (base_name);

			if (super_class == IntPtr.Zero)
				return IntPtr.Zero;
			
			foreach (RegisterAttribute attribute in Attribute.GetCustomAttributes (type, typeof (RegisterAttribute))) {
				if (attribute.Name != null)
					name = attribute.Name;
				break;
			}

			IntPtr return_value = objc_lookUpClass (name);
			if (return_value != IntPtr.Zero)
				return return_value;
			NativeRepresentation representation = GenerateNativeRepresentation (type);
			IntPtr [] methods = new IntPtr [representation.Methods.Length];
			IntPtr [] signatures = new IntPtr [representation.Methods.Length];
			IntPtr [] member_names = new IntPtr [representation.Members.Length];
			IntPtr [] member_types = new IntPtr [representation.Members.Length];

			for (int i = 0; i < representation.Methods.Length; i++) {
				methods [i] = Marshal.StringToCoTaskMemAnsi (representation.Methods [i]);
				signatures [i] = Marshal.StringToCoTaskMemAnsi (representation.Signatures [i]);
			}
				
			for (int i = 0; i < representation.Members.Length; i++) {
				member_names [i] = Marshal.StringToCoTaskMemAnsi (representation.Members [i].Name);
				member_types [i] = Marshal.StringToCoTaskMemAnsi (representation.Members [i].Type);
			}

			// MAGIC WARNING: This is serious voodoo; touch at your own risk
			unsafe {
				// This is from this layout:
				/*
					struct objc_class {
						struct objc_class *isa;
						struct objc_class *super_class;
						const char *name;
						long version;
						long info;
						long instance_size;
						struct objc_ivar_list *ivars;
						struct objc_method_list **methodLists;
						struct objc_cache *cache;
						struct objc_protocol_list *protocols;
					}
				*/

				// lets cache some frequently used values
				FieldInfo delegate_target = typeof (Delegate).GetField ("delegate_trampoline", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				int ptrsize = Marshal.SizeOf (typeof (IntPtr));
				int longsize = Marshal.SizeOf (typeof (int)); // Longs are 4 in native land
				int intsize = Marshal.SizeOf (typeof (int));

				char *nameptr = (char*)Marshal.StringToHGlobalAuto (name);
				void *root_class = (void*)super_class;

				// We first need to find the root_class; we do this by walking up ->super_class
				while ((int)*((int *)((int)root_class+ptrsize)) != 0) {
					root_class = (void *)(int)*((int *)((int)root_class+ptrsize));
				}
				// allocate the class
				void *new_class = (void*)Marshal.AllocHGlobal ((ptrsize*7) + (longsize*3));
				void *meta_class = (void*)Marshal.AllocHGlobal ((ptrsize*7) + (longsize*3));

				// setup the class
				(int)*(int *)((int)new_class+0) = (int)meta_class;
				(int)*(int *)((int)new_class+(ptrsize*3)+longsize) = 0x1;
				(int)*(int *)((int)meta_class+(ptrsize*3)+longsize) = 0x2;

				// set the class name
				(int)*(int *)((int)new_class+(ptrsize*2)) = (int)nameptr;
				(int)*(int *)((int)meta_class+(ptrsize*2)) = (int)nameptr;

				// connect the class heirarchy
				(int)*(int *)((int)new_class+ptrsize) = (int)super_class;
				(int)*(int *)((int)meta_class+ptrsize) = (int)*(int *)((int)super_class);
				(int)*(int *)((int)meta_class) = (int)*(int *)((int)root_class);

				// put in empty method lists for now
				void *new_class_mlist = (void *)Marshal.AllocHGlobal ((ptrsize*2)+intsize);
				void *meta_class_mlist = (void *)Marshal.AllocHGlobal ((ptrsize*2)+intsize);

				(int)*(int *)((int)new_class_mlist) = -1;
				(int)*(int *)((int)meta_class_mlist) = -1;

				(int)*(int *)((int)new_class+(ptrsize*4)+(longsize*3)) = (int)new_class_mlist;
				(int)*(int *)((int)meta_class+(ptrsize*4)+(longsize*3)) = (int)meta_class_mlist;

				// add the ivars
				int ivar_size = (int)*(int *)((int)super_class+(ptrsize*3)+(longsize*2));
				void *ivar_list = (void *)Marshal.AllocHGlobal (intsize+ptrsize+((representation.Members.Length+1)*((ptrsize*2)+intsize)));
				void *ivar = (void*)((int)ivar_list+intsize);
				(int)*(int *)((int)ivar_list) = representation.Members.Length;
				for (int i = 0; i < representation.Members.Length; i++) {
					(int)*(int *)((int)ivar) = (int)member_names [i];
					(int)*(int *)((int)ivar+ptrsize) = (int)member_types [i];
					(int)*(int *)((int)ivar+(ptrsize*2)) = ivar_size;
					ivar_size += representation.Members [i].Size;
					ivar = (void *)((int)ivar+(ptrsize*2)+intsize);
				}
				(int)*(int *)((int)new_class+(ptrsize*3)+(longsize*3)) = (int)ivar_list;

				(int)*(int *)((int)ivar) = (int)Marshal.StringToCoTaskMemAnsi ("methodCallback");
				(int)*(int *)((int)ivar+ptrsize) = (int)Marshal.StringToCoTaskMemAnsi ("^?");
				(int)*(int *)((int)ivar+(ptrsize*2)) = ivar_size;
				ivar_size += 4;
				
				(int)*(int *)((int)new_class+(ptrsize*3)+(longsize*2)) = ivar_size;
				(int)*(int *)((int)meta_class+(ptrsize*3)+(longsize*2)) = (int)*(int *)(((int)((int*)(int)*(int *)((int)meta_class+ptrsize)))+(ptrsize*3)+(longsize*2));

				// zero the cache and protocols;
				(int)*(int *)((int)new_class+(ptrsize*5)+(longsize*3)) = 0;
				(int)*(int *)((int)new_class+(ptrsize*6)+(longsize*3)) = 0;
				(int)*(int *)((int)meta_class+(ptrsize*5)+(longsize*3)) = 0;
				(int)*(int *)((int)meta_class+(ptrsize*6)+(longsize*3)) = 0;

				objc_addClass ((IntPtr)new_class);

				// DIRTY HACK: get the implment method target
				IntPtr implement_method_target = (IntPtr)delegate_target.GetValue (implement_method);
				
				// add the methods
				void *method_list = (void *)Marshal.AllocHGlobal (ptrsize+intsize+((representation.Methods.Length+1)*(ptrsize*3)));
				(int)*(int *)((int)method_list+ptrsize) = representation.Methods.Length+1;
				void *methodptr = (void*)((int)method_list+ptrsize+intsize);
				for (int i = 0; i < representation.Methods.Length; i++) {
					(int)*(int *)((int)methodptr) = (int)sel_getUid (methods [i]);
					(int)*(int *)((int)methodptr+ptrsize) = (int)signatures [i];
					(int)*(int *)((int)methodptr+(ptrsize*2)) = (int)implement_method_target;
					methodptr = (void *)((int)methodptr+(ptrsize*3));
				}

				IntPtr construct_object_target = (IntPtr)delegate_target.GetValue (construct_object);

				(int)*(int *)((int)methodptr) = (int)sel_getUid (init_ptr);
				(int)*(int *)((int)methodptr+ptrsize) = (int)size_init_ptr;
				(int)*(int *)((int)methodptr+(ptrsize*2)) = (int)construct_object_target;
				methodptr = (void *)((int)methodptr+(ptrsize*3));

				class_addMethods ((IntPtr)new_class, (IntPtr)method_list);
				return_value = (IntPtr)new_class;
			}
			// END MAGIC
			
			return return_value;
		}
		
		internal static NativeRepresentation GenerateNativeRepresentation (Type type) {
			NativeRepresentation representation = new NativeRepresentation ();

			ArrayList methods = new ArrayList ();
			ArrayList signatures = new ArrayList ();
			ArrayList nativemembers = new ArrayList ();

			MethodInfo [] methodinfos = type.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

			foreach (ConstructorInfo constructor in type.GetConstructors (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)) {
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (constructor, typeof (ExportAttribute))) {
					signatures.Add (export_attribute.Signature != null ? export_attribute.Signature : GenerateConstructorSignature (constructor));
					methods.Add (export_attribute.Selector != null ? export_attribute.Selector : ConstructorToSelector (constructor));
				}
			}

			foreach (MethodInfo method in type.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)) {
				bool attribute_processed = false;
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
					signatures.Add (export_attribute.Signature != null ? export_attribute.Signature : GenerateMethodSignature (method));
					methods.Add (export_attribute.Selector != null ? export_attribute.Selector : MethodToSelector (method));
					attribute_processed = true;
					break;
				}
				if (!attribute_processed) { 
					signatures.Add (GenerateMethodSignature (method));
					methods.Add (MethodToSelector (method));
				}
			}

			foreach (FieldInfo field in type.GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				foreach (ConnectAttribute connect_attribute in Attribute.GetCustomAttributes (field, typeof (ConnectAttribute))) {
					string name = (connect_attribute.Name != null ? connect_attribute.Name : field.Name);
					int size = connect_attribute.Size;
					string typename = (connect_attribute.Type != null ? connect_attribute.Type : EncodedType (field.FieldType, out size));
					nativemembers.Add (new NativeMember (name, typename, size));
				}
			}

			representation.Methods = (string[])methods.ToArray (typeof (string));
			representation.Signatures = (string[])signatures.ToArray (typeof (string));
			representation.Members = (NativeMember[])nativemembers.ToArray (typeof (NativeMember));

			return representation;
		}

		public static string EncodedType (Type type, out int size) {
			if (type == typeof (char)) {
				size = Marshal.SizeOf (typeof (char));
				return "c";
			}
			if (type == typeof (Int32)) {
				size = Marshal.SizeOf (typeof (Int32));
				return "i";
			}
			if (type == typeof (short)) {
				size = Marshal.SizeOf (typeof (short));
				return "s";
			}
			if (type == typeof (long)) {
				size = Marshal.SizeOf (typeof (long));
				return "l";
			}
			if (type == typeof (Int64)) {
				size = Marshal.SizeOf (typeof (Int64));
				return "q";
			}
			if (type == typeof (UInt32)) {
				size = Marshal.SizeOf (typeof (UInt32));
				return "I";
			}
			if (type == typeof (ushort)) {
				size = Marshal.SizeOf (typeof (ushort));
				return "S";
			}
			if (type == typeof (ulong)) {
				size = Marshal.SizeOf (typeof (ulong));
				return "L";
			}
			if (type == typeof (UInt64)) {
				size = Marshal.SizeOf (typeof (UInt64));
				return "Q";
			}
			if (type == typeof (float)) {
				size = Marshal.SizeOf (typeof (float));
				return "f";
			}
			if (type == typeof (double)) {
				size = Marshal.SizeOf (typeof (double));
				return "d";
			}
			if (type == typeof (bool)) {
				size = Marshal.SizeOf (typeof (bool));
				return "B";
			}
			if (type == typeof (string)) {
				size = Marshal.SizeOf (typeof (IntPtr));
				return "@";
			}
			if (type == typeof (void)) {
				size = 0;
				return "v";
			}
			size = 4;
			return "@";
		}

		public static string GenerateConstructorSignature (ConstructorInfo constructor) {
			int size = 0;
			int p = 0;
			int q = 0;
			string signature = "";

			foreach (ParameterInfo param in constructor.GetParameters ()) {
				if (param.ParameterType.IsPrimitive)
					size += Marshal.SizeOf (param.ParameterType);
				else
					size += Marshal.SizeOf (typeof (IntPtr));
			}

			signature += EncodedType (typeof (IntPtr), out p);
			signature += size;
			signature += "@0:4";
			p = 4;

			foreach (ParameterInfo param in constructor.GetParameters ()) {
				signature += EncodedType (param.ParameterType, out q);
				p += q;
				signature += p;
			}
			
			return signature;
		}

		public static string GenerateMethodSignature (MethodInfo method) {
			int size = 0;
			int p = 0;
			int q = 0;
			string signature = "";

			foreach (ParameterInfo param in method.GetParameters ()) {
				if (param.ParameterType.IsPrimitive)
					size += Marshal.SizeOf (param.ParameterType);
				else
					size += Marshal.SizeOf (typeof (IntPtr));
			}

			signature += EncodedType (method.ReturnType, out p);
			signature += size;
			signature += "@0:4";
			p = 4;

			foreach (ParameterInfo param in method.GetParameters ()) {
				signature += EncodedType (param.ParameterType, out q);
				p += q;
				signature += p;
			}
			
			return signature;
		}

		private static string SelectorToMethod (Type type, string selector) {
			MethodInfo [] methods = type.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach (MethodInfo method in methods) {
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
					if (export_attribute.Selector != null && export_attribute.Selector == selector)
						return method.Name;
				}
			} 

			string methodname = selector;

			if (methodname.IndexOf (":") > 0)
				methodname = methodname.Substring (0, methodname.IndexOf (":"));

			return methodname;
		}

		private static string ConstructorToSelector (ConstructorInfo constructor) {
			if (constructor.GetParameters ().Length != 1)
				throw new Exception ("Unimplemented for constructors with: " + constructor.GetParameters ().Length  + " arguments");

			ParameterInfo param = constructor.GetParameters () [0];

			if (param.ParameterType != typeof (Rect))
				throw new Exception ("Unimplemented for constructors with parameters: " + param.ParameterType);

			return "initWithFrame:";
		}

		private static string MethodToSelector (MethodInfo method) {
			string selector = method.Name;
			ParameterInfo [] parameters = method.GetParameters ();
			if (parameters.Length > 0)
				selector += ":";
			for (int i = 1; i < parameters.Length; i++)
				selector += parameters [i].Name + ":";

			return selector;
		}

		public static object NativeToManaged (IntPtr native_object) {
#if DEBUG
			NativeToManagedHits ++;
#endif
			if (native_object == IntPtr.Zero)
				return null;

			Object return_value = null;
			
			string classname = (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (native_object, "className", typeof (IntPtr)), "cString", typeof (string));                                                                                
			if (classname.StartsWith ("%")) classname = classname.Substring (1);

			Type type = NativeClassToManagedType (classname);

			// Walk up the superclass chain looking for a known type
			IntPtr superclass = (IntPtr)ObjCMessaging.objc_msgSend (native_object, "superclass", typeof (IntPtr));
			while (type == null) {
#if DEBUG
				TypeWalks ++;
				if (WalkedTypes [classname] == null)
					WalkedTypes [classname] = 0;
				WalkedTypes [classname] = ((int)WalkedTypes [classname]) + 1;
#endif
				classname = (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (superclass, "className", typeof (IntPtr)), "cString", typeof (string));                                                                                
				if (classname == null || classname == "")
					throw new ArgumentException ("Error finding a managed object to represent: " + (string)ObjCMessaging.objc_msgSend ((IntPtr)ObjCMessaging.objc_msgSend (native_object, "className", typeof (IntPtr)), "cString", typeof (string)));

				type = NativeClassToManagedType (classname);
				superclass = (IntPtr)ObjCMessaging.objc_msgSend (superclass, "superclass", typeof (IntPtr));
			}

			lock (Object.NativeObjects) {
				if (Object.NativeObjects.Contains (native_object)) {
					CachedObject cache = (CachedObject) Object.NativeObjects [native_object];
#if DEBUG
					if (cache.Type != type) {
						CacheHitBadType ++;
					}
#endif
					if (cache.Reference.Target == null || cache.Type != type) {
						Object.NativeObjects.Remove (native_object);
					} else {
#if DEBUG
						CacheHits ++;
#endif
						return_value = cache.Reference.Target as Object;
						if (return_value is Cocoa.String)
							return return_value.ToString ();
						if (return_value is Cocoa.Array)
							return ((Array)return_value).ToArray ();
						if (return_value is Cocoa.MutableArray)
							return ((MutableArray)return_value).ToArray ();
						return return_value;
					}
				}
			}
			
#if DEBUG
			ConstructorHits++;
#endif

			ConstructorInfo constructor = ConstructorForType (type);
			if (constructor == null)
				throw new Exception ("Class " + classname + " does not have an appropriate native object constructor");

			return_value = (Object)constructor.Invoke (new object [] { native_object });

			if (return_value is Cocoa.String)
				return return_value.ToString ();
			if (return_value is Cocoa.Array)
				return ((Array)return_value).ToArray ();
			if (return_value is Cocoa.MutableArray)
				return ((MutableArray)return_value).ToArray ();
			return return_value;
		}

		public static void ImportMembers (Object item) {
			foreach (FieldInfo field in item.GetType ().GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				foreach (ConnectAttribute connect_attribute in Attribute.GetCustomAttributes (field, typeof (ConnectAttribute))) {
					string name = (connect_attribute.Name != null ? connect_attribute.Name : field.Name);
					IntPtr native_value = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (IntPtr)));
					object_getInstanceVariable (item.NativeObject, name, native_value);
					object value = field.FieldType.IsPrimitive ? Marshal.PtrToStructure (native_value, field.FieldType) : NativeToManaged (Marshal.ReadIntPtr (native_value));
					Marshal.FreeHGlobal (native_value);
					field.SetValue (item, value);
				}
			}
		}
		
		public static void ExportMembers (Object item) {
			foreach (FieldInfo field in item.GetType ().GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				foreach (ConnectAttribute connect_attribute in Attribute.GetCustomAttributes (field, typeof (ConnectAttribute))) {
					string name = (connect_attribute.Name != null ? connect_attribute.Name : field.Name);
					object value = field.GetValue (item);
					bool is_null = value == null;
					Type type = is_null ? null : value.GetType ();
					bool is_value_type = !is_null && type.IsPrimitive;

					IntPtr native_value = Marshal.AllocHGlobal (is_value_type ? Math.Max (8, Marshal.SizeOf (value)) : Marshal.SizeOf (typeof (IntPtr)));

					if (is_null) {
						Marshal.WriteIntPtr (native_value, IntPtr.Zero);
						object_setInstanceVariable (item.NativeObject, name, native_value);
						Marshal.FreeHGlobal (native_value);
					} else if (is_value_type) {
						Marshal.WriteIntPtr (native_value, IntPtr.Zero);
						Marshal.StructureToPtr (value, native_value, false);
						object_setInstanceVariable (item.NativeObject, name, native_value);
						Marshal.FreeHGlobal (native_value);
					} else if (value is Object) {
						Marshal.FreeHGlobal (native_value);
						native_value = ((Object)value).NativeObject;
						object_setInstanceVariable (item.NativeObject, name, native_value);
					} else {
						throw new ArgumentException ("Unhandled exporting of {0}" + value.GetType ());
					}
				}
			}
		}

		public static Object InvokeConstructor (Type type, string selector, object [] arguments) {
			Type [] types = new Type [arguments.Length];

			for (int i = 0; i < arguments.Length; i++)
				types [i] = arguments [i].GetType ();
				
			ConstructorInfo constructor = type.GetConstructor (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);

			return (Object)constructor.Invoke (arguments);
		}

		public static object InvokeMethod (Object item, string selector, object [] arguments) {
			string method = SelectorToMethod (item.GetType (), selector); 

			ImportMembers (item);

			object return_value = item.GetType ().InvokeMember (method, BindingFlags.Default | BindingFlags.InvokeMethod, null, item, arguments);

			ExportMembers (item);

			return return_value;
		}

		private static ConstructorInfo ConstructorForType (Type type) {
			ConstructorInfo constructor = (ConstructorInfo) ConstructorInfoCache [type];
			if (constructor != null)
				return constructor;
			constructor = type.GetConstructor (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type [] { typeof (IntPtr) }, null);
			ConstructorInfoCache [type] = constructor;
	
			return constructor;
		}

		[DllImport("libobjc.dylib")]
		public static extern void objc_setClassHandler(ClassHandlerDelegate class_handler);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr object_getInstanceVariable(IntPtr id, string name, IntPtr val);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr object_setInstanceVariable(IntPtr id, string name, IntPtr val);  

		[DllImport ("libobjc.dylib")]
		private static extern IntPtr objc_lookUpClass (string name);
		
		[DllImport ("libobjc.dylib")]
		private static extern IntPtr sel_getUid (IntPtr name);
		
		[DllImport ("libobjc.dylib")]
		private static extern void objc_addClass (IntPtr cls);
		
		[DllImport ("libobjc.dylib")]
		private static extern void class_addMethods (IntPtr cls, IntPtr methods);
		
		[DllImport ("libobjc.dylib")]
		private static extern IntPtr class_getInstanceMethod (IntPtr cls, IntPtr sel);
		
		[DllImport ("libobjc.dylib")]
		private static extern int method_getNumberOfArguments (IntPtr method);
		
		[DllImport ("libc.dylib")]
		public static extern int memcmp (IMPDelegate a, IMPDelegate b, int size);
	}
}
