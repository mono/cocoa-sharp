using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace WebKit {
	public class WebHistoryItem : Cocoa.Object {
		private static string ObjectiveCName = "WebHistoryItem";                                                                                      
		public WebHistoryItem (IntPtr native_object) {
			NativeObject = native_object;
		}

		public WebHistoryItem (string url) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) Cocoa.ObjCClass.FromType (typeof (WebHistoryItem)).ToIntPtr (), "alloc", typeof (IntPtr)), "initWithURLString:title:lastVisitedTimeInterval:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (url).NativeObject, typeof (IntPtr), IntPtr.Zero, typeof (double), 0);
		}

		public string URL {
			get {
				return (string) ((Cocoa.String) Cocoa.Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "URLString", typeof (IntPtr)))).ToString ();
			}
		}

		public string OriginalURL {
			get {
				return (string) ((Cocoa.String) Cocoa.Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "originalURLString", typeof (IntPtr)))).ToString ();
			}
		}

	}
}
