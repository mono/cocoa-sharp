using System;
using System.Collections;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {

	public class Object {	
		public static IDictionary NativeObjects = new Hashtable ();
		public static IDictionary ManagedObjects = new Hashtable ();
		public static IDictionary NativeClasses = new Hashtable ();

		private static string ObjectiveCName = "NSObject";

		private IntPtr objc_object;
		protected bool autorelease = false;

		#region Constructors
		static Object () {
			// This is a hideous hack to load the dylib into our address space
			strlen ("Load Foundation Into My Addressspace.");
			NativeClasses [typeof (Object)] = Native.RegisterClass (typeof (Object)); 
		}

		public Object () {
			objc_object = IntPtr.Zero;

			if (NativeClasses [this.GetType ()] == null)
				NativeClasses [this.GetType ()] = Native.RegisterClass (this.GetType ());

			NativeObject = (IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [this.GetType ()], "alloc", typeof (IntPtr));
			autorelease = true;
		}

		public Object (IntPtr native_object) {
			NativeObject = native_object;
		}
		#endregion

		#region Deconstructor
		~ Object () {
		/*
		 * The objc object might be collected at this point in which case this causes a mach_exception
			if (NativeObject != IntPtr.Zero && autorelease == true) {
				if ((uint)ObjCMessaging.objc_msgSend (NativeObject, "retainCount", typeof (uint)) == 0) 
					ObjCMessaging.objc_msgSend (NativeObject, "release", typeof (void));
			}
		*/
			lock (NativeObjects) {
				if (NativeObjects.Contains (objc_object)) {
					NativeObjects.Remove (objc_object);
				}
			}
			lock (ManagedObjects) {
				if (ManagedObjects.Contains (objc_object)) {
					ManagedObjects.Remove (objc_object);
				}
			}
		}
		#endregion

		#region Properties
		public IntPtr NativeObject {
			get {
				return objc_object;
			}
			set {
				if (objc_object == value) return;
				if (objc_object != IntPtr.Zero) {
					lock (NativeObjects) {
						if (!NativeObjects.Contains (objc_object)) 
							throw new ArgumentException ("Attempt to change an object from an unknown value.");
						NativeObjects.Remove (objc_object);
					}
				}

				objc_object = value;

				lock (NativeObjects) {
					if (NativeObjects.Contains (objc_object)) {
						NativeObjects.Remove (objc_object);
					}
					NativeObjects.Add (objc_object, new CachedObject (new WeakReference (this), this.GetType ()));
				}
			}
		}

		// AUDIT ME
		public IntPtr Zone {
			get {
				return (IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "zone", typeof (System.IntPtr));
			}
		}


		#endregion
		
		#region Methods
		public void Initialize () {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "init", typeof (IntPtr));
		}  

		public void Retain () {
			ObjCMessaging.objc_msgSend (NativeObject, "retain", typeof (void));
		}
		#endregion

		#region PInvokes
		[DllImport ("/System/Library/Frameworks/Foundation.framework/Foundation")]
		private static extern int strlen (string str);
		
		[DllImport ("/System/Library/Frameworks/Foundation.framework/Foundation")]
		private static extern IntPtr sel_getUid (string str);
		#endregion
	}

}
