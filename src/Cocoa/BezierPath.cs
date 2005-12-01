using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class BezierPath : Cocoa.Object {
		private static string ObjectiveCName = "NSBezierPath";                                                                                      

		static BezierPath () {
			NativeClasses [typeof (BezierPath)] = Native.RegisterClass (typeof (BezierPath)); 
		}

		public BezierPath (IntPtr native_object) : base (native_object) {}
		
		public static void FillRect (Rect bounds) {
			ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (BezierPath)], "fillRect:", typeof (void), typeof (Rect), bounds);
		}
	}
}
