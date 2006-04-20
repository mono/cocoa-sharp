using System;

namespace Cocoa {
	public class Dictionary : Object {
		public static string ObjectiveCName = "NSDictionary";

		public Dictionary () : base () {}

		public Dictionary (IntPtr native_object) : base (native_object) {}

		public Dictionary (string key, Object native_object) : base () {
			String s = new String (key);
                        NativeObject = (IntPtr)ObjCMessaging.objc_msgSend (NativeClass.ToIntPtr (), "dictionaryWithObject:forKey:", typeof (IntPtr), typeof (IntPtr), native_object.NativeObject, typeof (IntPtr), s.NativeObject);                  
		}
	}
}
