using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Screen : Cocoa.Object {
		private static string ObjectiveCName = "NSScreen";                                                                                      

		static Screen () {
			NativeClasses [typeof (Screen)] = Native.RegisterClass (typeof (Screen)); 
		}

		public Screen (IntPtr native_object) : base (native_object) {}
		
		public static Screen Main {
			get {
				return (Screen) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Screen)], "mainScreen", typeof (IntPtr)));
			}
		}

		public Rect Frame {
			get {
				return (Rect) ObjCMessaging.objc_msgSend (NativeObject, "frame", typeof (Rect));	
			}
		}
	}
}
