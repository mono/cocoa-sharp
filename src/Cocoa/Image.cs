using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Image : Cocoa.Object {
		private static string ObjectiveCName = "NSImage";                                                                                      
		public Image (string filename) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeClass.ToIntPtr (), "alloc", typeof (IntPtr));
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initByReferencingFile:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (filename).NativeObject);
		}

		public Image (IntPtr native_object) : base (native_object) {}
		
		public Color BackgroundColor {
			get {
				return (Color) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "backgroundColor", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setBackgroundColor:", typeof (void), typeof (IntPtr), ((Color)value).NativeObject);
			}
		}

		public Size Size {
			get {
				return (Size) ObjCMessaging.objc_msgSend (NativeObject, "size", typeof (Size));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setSize:", typeof (void), typeof (Size), ((Size)value));
			}
		}
	}
}
