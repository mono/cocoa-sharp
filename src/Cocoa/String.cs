using System;

namespace Cocoa {
	public class String : Object {
		private static string ObjectiveCName = "NSString";                                                                                            
		public String () : base () {}

		public String (IntPtr native_object) : base (native_object) {}

		public String (string init) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeClass.ToIntPtr (), "stringWithUTF8String:", typeof (IntPtr), typeof (string), init);
		}

		public static String FromSelector (IntPtr selector) {
			return new String (selector);
		}

		public override string ToString () {
            
            /// This function used to be just cal NSString's cString, however this cause some downstream
            /// character encoding issues. Especially in the Event class. The fix for this was to
            /// tell this function to encode the string using UTF-8.
            /// Todd Schavey - schaveyt@gmail.com
            ///
			return (string) ObjCMessaging.objc_msgSend (NativeObject, "cStringUsingEncoding:", typeof (string), typeof(int), 4 /*NSUTF8StringEncoding*/ );
		}
	}
}
