using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class DragDestination : Object {
		private static string ObjectiveCName = "NSDragDestination";                                                                                      
		public DragDestination (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object Source {
			get {
				return (Cocoa.Object) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "draggingSource", typeof (IntPtr)));
			}
		}

		public Cocoa.Pasteboard Pasteboard {
			get {
				return (Pasteboard) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "draggingPasteboard", typeof (IntPtr)));
			}
		}
	}
}
