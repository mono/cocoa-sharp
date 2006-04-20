using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class BezierPath : Cocoa.Object {
		private static string ObjectiveCName = "NSBezierPath";                                                                                      
		public BezierPath (IntPtr native_object) : base (native_object) {}
		
		public static void FillRect (Rect bounds) {
			ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (BezierPath)).ToIntPtr (), "fillRect:", typeof (void), typeof (Rect), bounds);
		}
	}
}
