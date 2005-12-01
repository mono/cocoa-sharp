using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Color : Cocoa.Object {
		private static string ObjectiveCName = "NSColor";                                                                                      

		static Color () {
			NativeClasses [typeof (Color)] = Native.RegisterClass (typeof (Color)); 
		}

		public Color (IntPtr native_object) : base (native_object) {}
		
		public static Color FromHueSaturationBrightnessAlpha (float hue, float saturation, float brightness, float alpha) {
			return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "colorWithDeviceHue:saturation:brightness:alpha:", typeof (IntPtr), typeof (float), hue, typeof (float), saturation, typeof (float), brightness, typeof (float), alpha));
		}

		public static Color Black {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "blackColor", typeof (IntPtr)));
			}
		}
		public static Color Blue {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "blueColor", typeof (IntPtr)));
			}
		}
		public static Color Brown {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "brownColor", typeof (IntPtr)));
			}
		}
		public static Color Clear {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "clearColor", typeof (IntPtr)));
			}
		}
		public static Color Cyan {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "cyanColor", typeof (IntPtr)));
			}
		}
		public static Color DarkGray {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "darkGrayColor", typeof (IntPtr)));
			}
		}
		public static Color Gray {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "grayColor", typeof (IntPtr)));
			}
		}
		public static Color Green {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "greenColor", typeof (IntPtr)));
			}
		}
		public static Color LightGray {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "lightGrayColor", typeof (IntPtr)));
			}
		}
		public static Color Magenta {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "magentaColor", typeof (IntPtr)));
			}
		}
		public static Color Orange {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "orangeColor", typeof (IntPtr)));
			}
		}
		public static Color Purple {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "purpleColor", typeof (IntPtr)));
			}
		}
		public static Color Red {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "redColor", typeof (IntPtr)));
			}
		}
		public static Color White {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "whiteColor", typeof (IntPtr)));
			}
		}
		public static Color Yellow {
			get {
				return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (Color)], "yellowColor", typeof (IntPtr)));
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
			return (Color) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "colorUsingColorSpaceName:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String ("NSDeviceRGBColorSpace").NativeObject));
		}
	}
}
