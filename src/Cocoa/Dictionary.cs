using System;

namespace Cocoa {
	public class Dictionary : Object {
		public static string ObjectiveCName = "NSDictionary";

		static Dictionary () {
			NativeClasses [typeof (Dictionary)] = Native.RegisterClass (typeof (Dictionary));
		} 

		public Dictionary () : base () {}

		public Dictionary (IntPtr native_object) : base (native_object) {}

		public Dictionary (string key, Object native_object) : base () {
			String s = new String (key);
                        NativeObject = (IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [this.GetType ()], "dictionaryWithObject:forKey:", typeof (IntPtr), typeof (IntPtr), native_object.NativeObject, typeof (IntPtr), s.NativeObject);                  
		}
	}
}
