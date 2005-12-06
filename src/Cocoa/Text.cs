using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Text : Responder {
		private static string ObjectiveCName = "NSText";                                                                                      

		static Text () {
			NativeClasses [typeof (Text)] = Native.RegisterClass (typeof (Text)); 
		}

		public Text () : base () {}

		public Text (IntPtr native_object) : base (native_object) {}

		public string String {
			get {
				return(string) Native.NativeToManaged((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "string", typeof(System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setString:", typeof(void), typeof(System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}

	}
}
