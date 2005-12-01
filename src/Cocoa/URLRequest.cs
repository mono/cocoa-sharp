using System;

namespace Cocoa {
	public class URLRequest : Object {
		public static string ObjectiveCName = "NSURLRequest";

		static URLRequest () {
			NativeClasses [typeof (URLRequest)] = Native.RegisterClass (typeof (URLRequest)); 
		}

		public URLRequest () : base () {}

		public URLRequest (IntPtr native_object) : base (native_object) {}

		public URL URL {
			get {
				return (URL) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "URL", typeof (IntPtr)));
			}
		}
	}
}
