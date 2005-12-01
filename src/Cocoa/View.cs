using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class View : Responder {
		private static string ObjectiveCName = "NSView";                                                                                      

		static View () {
			NativeClasses [typeof (View)] = Native.RegisterClass (typeof (View)); 
		}

		public View () : base () {}

		public View (IntPtr native_object) : base (native_object) {}

		public View (Rect frame) : base () {
			if (this.GetType ().IsSubclassOf (typeof (View))) 
				NativeObject = (IntPtr) ObjCMessaging.objc_msgSendSuper (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), frame);
			else
				throw new ArgumentException ("initWithFrame: directly on NSView is unsupported");
		}
		
		public Rect Bounds {
			get {
				return (Rect)ObjCMessaging.objc_msgSend (NativeObject, "bounds", typeof (Rect));
			}
		}

		public void Display () {
			ObjCMessaging.objc_msgSend (NativeObject, "display", typeof (void));
		}

		public void AddSubview (View view) {
			ObjCMessaging.objc_msgSend (NativeObject, "addSubview:", typeof (void), typeof (IntPtr), view.NativeObject);
		}

		public void Invalidate (Rect bounds) {
			ObjCMessaging.objc_msgSend (NativeObject, "invalidate:", typeof (void), typeof (Rect), bounds);
		}

		public void RegisterDragType (string type) {
			RegisterDragTypes (new string [] {type});
		}

		public void RegisterDragTypes (string [] types) {
			MutableArray array = new MutableArray ();
			foreach (string t in types) {
				array.Add (new Cocoa.String (t));
			}

			ObjCMessaging.objc_msgSend (NativeObject, "registerForDraggedTypes:", typeof (void), typeof (IntPtr), array.NativeObject);
		}
	}
}
