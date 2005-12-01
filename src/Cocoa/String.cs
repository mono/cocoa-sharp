using System;

namespace Cocoa {
	public class String : Object {
		private static string ObjectiveCName = "NSString";                                                                                            

		static String () {
			NativeClasses [typeof (String)] = Native.RegisterClass (typeof (String)); 
		}

		public String () : base () {}

		public String (IntPtr native_object) : base (native_object) {}

		public String (string init) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) Cocoa.Object.NativeClasses [typeof (String)], "stringWithUTF8String:", typeof (IntPtr), typeof (string), init);
		}

		public static String FromSelector (IntPtr selector) {
			return new String (selector);
		}

		public override string ToString () {
			return (string) ObjCMessaging.objc_msgSend (NativeObject, "cString", typeof (string));
		}
	}
}
