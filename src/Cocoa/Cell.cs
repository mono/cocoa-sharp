using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Cell : Cocoa.Object {
		private static string ObjectiveCName = "NSCell";                                                                                      

		static Cell () {
			NativeClasses [typeof (Cell)] = Native.RegisterClass (typeof (Cell)); 
		}
		
		public Cell (IntPtr native_object) : base (native_object) {}
		
		public string Value {
			get {
				return (string)Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "stringValue:", typeof (void)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setStringValue:", typeof (void), typeof (System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}
	}
}
