using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class ProgressIndicator : View {
		private static string ObjectiveCName = "NSProgressIndicator";                                                                                      
		public ProgressIndicator (Rect rect) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), rect);
		}

		public ProgressIndicator (IntPtr native_object) : base (native_object) {}
		
		public void StartAnimation () {
			ObjCMessaging.objc_msgSend (NativeObject, "startAnimation:", typeof (void), typeof (IntPtr), IntPtr.Zero);
		}
		
		public void StopAnimation () {
			ObjCMessaging.objc_msgSend (NativeObject, "stopAnimation:", typeof (void), typeof (IntPtr), IntPtr.Zero);
		}
	}
}
