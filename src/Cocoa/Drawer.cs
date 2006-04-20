using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Drawer : Cocoa.Object {
		private static string ObjectiveCName = "NSDrawer";                                                                                      
		public Drawer (IntPtr native_object) : base (native_object) {}

		public void Open () {
			ObjCMessaging.objc_msgSend (NativeObject, "open", typeof (void));
		}

		public void Close () {
			ObjCMessaging.objc_msgSend (NativeObject, "close", typeof (void));
		}
	}
}
