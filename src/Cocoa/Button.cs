using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Button : Control {
		private static string ObjectiveCName = "NSButton";
		
		public Button (Rect frame) : base (frame) {
		}

		public Button (IntPtr native_object) : base (native_object) {}

		public string Title {
			get {
				return Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "title", typeof(System.IntPtr))).ToString ();
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setTitle:", typeof(void), typeof(System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}

		public BezelStyle BezelStyle {
			get { 
				return (Cocoa.BezelStyle)ObjCMessaging.objc_msgSend (NativeObject, "bezelStyle", typeof (System.Int32));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setBezelStyle:", typeof (void), typeof (System.Int32), value);
			}
		}

	}
}
