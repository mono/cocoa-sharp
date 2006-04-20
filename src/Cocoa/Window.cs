using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Window : Responder {
		private static string ObjectiveCName = "NSWindow";                                                                                      
		public Window () : base () {
			Initialize ();
		}
		
		public Window (IntPtr native_object) : base (native_object) {}

		public Window (Rect rect, WindowStyle stylemask, BackingStoreType backingstore, bool defer) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeClass.ToIntPtr (), "alloc", typeof (IntPtr));
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithContentRect:styleMask:backing:defer:", typeof (IntPtr), typeof (Rect), rect, typeof (int), stylemask, typeof (int), backingstore, typeof (bool), defer);
		}
		
		public Rect Frame {
			get {
				return (Rect) ObjCMessaging.objc_msgSend (NativeObject, "frame", typeof (Rect));	
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setFrame:display:", typeof (IntPtr), typeof (Rect), ((Rect)value), typeof (bool), true);	
			}
		}

		public string Title {
			get {
				return Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "title", typeof (IntPtr))).ToString ();	
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setTitle:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (value).NativeObject);
			}
		}
		
		public View View {
			get {
				return (View) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "contentView", typeof (IntPtr)));
			}
		}

		public void Center () {
			ObjCMessaging.objc_msgSend (NativeObject, "center", typeof (void));
		}

		public void Show () {
			ObjCMessaging.objc_msgSend (NativeObject, "makeKeyAndOrderFront:", typeof (void), typeof (IntPtr), IntPtr.Zero);
		}

		public void Hide () {
			ObjCMessaging.objc_msgSend (NativeObject, "orderOut:", typeof (void), typeof (IntPtr), IntPtr.Zero);
		}
		
		public void Close () {
			ObjCMessaging.objc_msgSend (NativeObject, "close", typeof (void));
		}
	}
}
