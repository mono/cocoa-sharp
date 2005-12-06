using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Button : Control {
		private static string ObjectiveCName = "NSButton";
		

		static Button () {
			NativeClasses [typeof (Button)] = Native.RegisterClass (typeof (Button)); 
		}

		public Button (IntPtr native_object) : base (native_object) {}

		public string Title {
			get {
				return(string) Native.NativeToManaged((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "title", typeof(System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setTitle:", typeof(void), typeof(System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}

	}
}
