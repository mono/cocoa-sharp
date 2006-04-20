using System;

namespace Cocoa {
	public class URLRequest : Object {
		public static string ObjectiveCName = "NSURLRequest";

		public URLRequest () : base () {}

		public URLRequest (IntPtr native_object) : base (native_object) {}

		public URL URL {
			get {
				return (URL) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "URL", typeof (IntPtr)));
			}
		}
	}
}
