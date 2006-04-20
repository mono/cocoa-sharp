using System;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {

	public class Object : IDisposable {	
		private static string ObjectiveCName = "CSObject";
		private static Hashtable instances = new Hashtable ();

		private IntPtr objc_object;
		private ObjCClass native_class;

		static Object () {
			ObjCMethods.dlopen ("/System/Library/Frameworks/Foundation.framework/Foundation", 0x1);
			ObjCMethods.dlopen ("/System/Library/Frameworks/AppKit.framework/AppKit", 0x1);
			ObjCMethods.class_poseAs (ObjCClass.FromType (typeof (Object)).ToIntPtr (), ObjCMethods.objc_getClass ("NSObject"));
		}

		public Object () {
			native_class = ToObjCClass (); 
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (native_class.ToIntPtr (), "alloc", typeof (IntPtr));
		}

		public Object (IntPtr native_object) {
			NativeObject = native_object;
		}

		~ Object () {
		}

		public void Dispose () {
			lock (instances) {
				if (objc_object != IntPtr.Zero)
					instances.Remove (objc_object);
			}
		}

		public ObjCClass NativeClass {
			get {
				return native_class;
			}
		}

		public IntPtr NativeObject {
			get {
				return objc_object;
			}
			set {
				if (value == IntPtr.Zero)
					throw new InvalidOperationException ("A native object cannot be null");

				lock (instances) {
					if (objc_object != IntPtr.Zero)
						instances.Remove (objc_object);

					objc_object = value;
					
					/*
					 * This doesn't work until dealloc is fixed
					 * if (instances.ContainsKey (objc_object) && instances [objc_object] != this)
					 * 	throw new InvalidOperationException ("Two objects are attempting to map to the same id.");
					 */
					instances [objc_object] = this;
				}
			}
		}

		public IntPtr Zone {
			get {
				return (IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "zone", typeof (System.IntPtr));
			}
		}

		[Export ("dealloc")]
		public void Dealloc () {
			this.Dispose ();
			ObjCMessaging.objc_msgSend (ObjCClass.FromType (typeof (Object)).ToNativeClass ().super_class, "dealloc", typeof (void));
		}

		public void Initialize () {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "init", typeof (IntPtr));
		}  

		public void Retain () {
			ObjCMessaging.objc_msgSend (NativeObject, "retain", typeof (void));
		}

		public static Object FromIntPtr (IntPtr from) {
			lock (instances) {
				if (instances.ContainsKey (from))
					return (Object) instances [from];

				Type type = ObjCClass.TypeForIntPtr (from);

				if (type == typeof (void))
					return null;

				return (Object) Activator.CreateInstance (type, new object [] {from});
			}
		}

		private ObjCClass ToObjCClass () {
			if (native_class != null)
				return native_class;

			native_class = ObjCClass.FromObject (this);

			return native_class;
		}

		public void ImportMembers () {
			foreach (FieldInfo field in this.GetType ().GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				ConnectAttribute attr = (ConnectAttribute) Attribute.GetCustomAttribute (field, typeof (ConnectAttribute));
				if (attr != null) {
					string name = (attr.Name != null ? attr.Name : field.Name);
					IntPtr native_value = Marshal.AllocHGlobal (Marshal.SizeOf (typeof (IntPtr)));
					ObjCMethods.object_getInstanceVariable (this.NativeObject, name, native_value);
					field.SetValue (this, field.FieldType.IsPrimitive ? Marshal.PtrToStructure (native_value, field.FieldType) : Object.FromIntPtr (Marshal.ReadIntPtr (native_value)));
					Marshal.FreeHGlobal (native_value);
				}
			}
		}

		public void ExportMembers () {
			foreach (FieldInfo field in this.GetType ().GetFields (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				ConnectAttribute attr = (ConnectAttribute) Attribute.GetCustomAttribute (field, typeof (ConnectAttribute));
				if (attr != null) {
					string name = (attr.Name != null ? attr.Name : field.Name);
					object value = field.GetValue (this);
					bool is_null = value == null;
					Type type = is_null ? null : value.GetType ();
					bool is_value_type = !is_null && type.IsPrimitive;

					IntPtr native_value = Marshal.AllocHGlobal (is_value_type ? Math.Max (8, Marshal.SizeOf (value)) : Marshal.SizeOf (typeof (IntPtr)));

                                        if (is_null) {
                                                Marshal.WriteIntPtr (native_value, IntPtr.Zero);
                                                ObjCMethods.object_setInstanceVariable (this.NativeObject, name, native_value);
                                                Marshal.FreeHGlobal (native_value);
                                        } else if (is_value_type) {
                                                Marshal.WriteIntPtr (native_value, IntPtr.Zero);
                                                Marshal.StructureToPtr (value, native_value, false);
                                                ObjCMethods.object_setInstanceVariable (this.NativeObject, name, native_value);
                                                Marshal.FreeHGlobal (native_value);
                                        } else if (value is Object) {
                                                Marshal.FreeHGlobal (native_value);
                                                native_value = ((Object)value).NativeObject;
                                                ObjCMethods.object_setInstanceVariable (this.NativeObject, name, native_value);
                                        } else {
                                                throw new ArgumentException ("Unhandled exporting of {0}" + value.GetType ());
                                        }    
				}
			}
		}


	}

}
