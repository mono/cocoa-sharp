using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Image : Cocoa.Object {
		private static string ObjectiveCName = "NSImage";                                                                                      

		static Image () {
			NativeClasses [typeof (Image)] = Native.RegisterClass (typeof (Image)); 
		}

		public Image (string filename) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Image)], "alloc", typeof (IntPtr));
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initByReferencingFile:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (filename).NativeObject);
			autorelease = true;
		}

		public Image (IntPtr native_object) : base (native_object) {}
		
		public Color BackgroundColor {
			get {
				return (Color) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "backgroundColor", typeof (IntPtr)));
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
