using System;

namespace Cocoa {
	public class URL : Object {
		public static string ObjectiveCName = "NSURL";

		static URL () {
			NativeClasses [typeof (URL)] = Native.RegisterClass (typeof (URL)); 
		}

		public URL () : base () {}

		public URL (IntPtr native_object) : base (native_object) {}

		public string AbsoluteString {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "absoluteString", typeof (IntPtr)));
			}
		}
	}
}
