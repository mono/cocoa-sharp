using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Color : Cocoa.Object {
		private static string ObjectiveCName = "NSColor";                                                                                      
		public Color (IntPtr native_object) : base (native_object) {}
		
		public static Color FromHueSaturationBrightnessAlpha (float hue, float saturation, float brightness, float alpha) {
			return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "colorWithDeviceHue:saturation:brightness:alpha:", typeof (IntPtr), typeof (float), hue, typeof (float), saturation, typeof (float), brightness, typeof (float), alpha));
		}

		public static Color Black {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "blackColor", typeof (IntPtr)));
			}
		}
		public static Color Blue {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "blueColor", typeof (IntPtr)));
			}
		}
		public static Color Brown {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "brownColor", typeof (IntPtr)));
			}
		}
		public static Color Clear {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "clearColor", typeof (IntPtr)));
			}
		}
		public static Color Cyan {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "cyanColor", typeof (IntPtr)));
			}
		}
		public static Color DarkGray {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "darkGrayColor", typeof (IntPtr)));
			}
		}
		public static Color Gray {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "grayColor", typeof (IntPtr)));
			}
		}
		public static Color Green {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "greenColor", typeof (IntPtr)));
			}
		}
		public static Color LightGray {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "lightGrayColor", typeof (IntPtr)));
			}
		}
		public static Color Magenta {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "magentaColor", typeof (IntPtr)));
			}
		}
		public static Color Orange {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "orangeColor", typeof (IntPtr)));
			}
		}
		public static Color Purple {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "purpleColor", typeof (IntPtr)));
			}
		}
		public static Color Red {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "redColor", typeof (IntPtr)));
			}
		}
		public static Color White {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "whiteColor", typeof (IntPtr)));
			}
		}
		public static Color Yellow {
			get {
				return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (Color)).ToIntPtr (), "yellowColor", typeof (IntPtr)));
			}
		}

		public float RedComponent {
			get {
				return (float) ObjCMessaging.objc_msgSend (NativeObject, "redComponent", typeof (float));
			}
		}

		public float BlueComponent {
			get {
				return (float) ObjCMessaging.objc_msgSend (NativeObject, "blueComponent", typeof (float));
			}
		}

		public float GreenComponent {
			get {
				return (float) ObjCMessaging.objc_msgSend (NativeObject, "greenComponent", typeof (float));
			}
		}

		public float AlphaComponent {
			get {
				return (float) ObjCMessaging.objc_msgSend (NativeObject, "alphaComponent", typeof (float));
			}
		}

		public Color ToRGB () {
			return (Color) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "colorUsingColorSpaceName:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String ("NSDeviceRGBColorSpace").NativeObject));
		}
        
        public void Set(){
            ObjCMessaging.objc_msgSend (NativeObject, "set", typeof (void));
        }
        
        public void SetFill(){
            ObjCMessaging.objc_msgSend (NativeObject, "setFill", typeof (void));
        }
        
        public void SetStroke(){
            ObjCMessaging.objc_msgSend (NativeObject, "setStroke", typeof (void));
        }
        
        public void DrawSwatchInRect( Rect rect ){
            ObjCMessaging.objc_msgSend (NativeObject, "drawSwatchInRect:", typeof (void), typeof (Rect), rect);
        }
	}
}
