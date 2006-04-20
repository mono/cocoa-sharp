using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Cell : Cocoa.Object {
		private static string ObjectiveCName = "NSCell";                                                                                      
		public Cell (IntPtr native_object) : base (native_object) {}
		
		public string Value {
			get {
				return Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "stringValue:", typeof (void))).ToString ();
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setStringValue:", typeof (void), typeof (System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}
	}
}
