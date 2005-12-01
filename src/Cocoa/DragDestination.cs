using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class DragDestination : Object {
		private static string ObjectiveCName = "NSDragDestination";                                                                                      

		static DragDestination () {
			NativeClasses [typeof (DragDestination)] = Native.RegisterClass (typeof (DragDestination)); 
		}

		public DragDestination (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object Source {
			get {
				return (Cocoa.Object) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "draggingSource", typeof (IntPtr)));
			}
		}

		public Cocoa.Pasteboard Pasteboard {
			get {
				return (Pasteboard) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "draggingPasteboard", typeof (IntPtr)));
			}
		}
	}
}
