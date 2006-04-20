using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class ImageView : Control {
		private static string ObjectiveCName = "NSImageView";                                                                                      
		public ImageView (Rect rect) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), rect);
		}

		public ImageView (IntPtr native_object) : base (native_object) {}

		public Image Image {
			get {
				return (Image) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "image", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setImage:", typeof (IntPtr), typeof(IntPtr), ((Image)value).NativeObject);
			}
		}
	}
}
