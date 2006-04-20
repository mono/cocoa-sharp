using System;

namespace Cocoa {
	public class URL : Object {
		public static string ObjectiveCName = "NSURL";

		public URL () : base () {}

		public URL (IntPtr native_object) : base (native_object) {}

		public string AbsoluteString {
			get {
				return Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "absoluteString", typeof (IntPtr))).ToString ();
			}
		}
	}
}
