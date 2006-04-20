using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class TextView : Text {
		private static string ObjectiveCName = "NSTextView";                                                                                      
		public TextView (IntPtr native_object) : base (native_object) {}

		public bool FieldEditor {
			get {
				return(bool)ObjCMessaging.objc_msgSend(NativeObject, "isFieldEditor", typeof(System.Boolean));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setFieldEditor:", typeof(void), typeof(System.Boolean), value);
			}
		}
	}
}
