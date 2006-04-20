using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Screen : Cocoa.Object {
		private static string ObjectiveCName = "NSScreen";                                                                                      
		public Screen (IntPtr native_object) : base (native_object) {}
		
		public static Screen Main {
			get {
				return (Screen) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Screen)).ToIntPtr (), "mainScreen", typeof (IntPtr)));
			}
		}

		public Rect Frame {
			get {
				return (Rect) ObjCMessaging.objc_msgSend (NativeObject, "frame", typeof (Rect));	
			}
		}
	}
}
