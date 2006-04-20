using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace WebKit {
	public class WebBackForwardList : Cocoa.Object {
		private static string ObjectiveCName = "WebBackForwardList";                                                                                      

		public WebBackForwardList (IntPtr native_object) {
			NativeObject = native_object;
		}

		public int Capacity {
			get {
				return (int)ObjCMessaging.objc_msgSend (NativeObject, "capacity", typeof (int));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setCapacity:", typeof (void), typeof (int), (int)value);
			}
		}
		
		public int BackListCount {
			get {
				return (int)ObjCMessaging.objc_msgSend (NativeObject, "backListCount", typeof (int));
			}
		}

		public int ForwardListCount {
			get {
				return (int)ObjCMessaging.objc_msgSend (NativeObject, "forwardListCount", typeof (int));
			}
		}
		
		public WebHistoryItem CurrentItem {
			get {
				return (WebHistoryItem) Cocoa.Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "currentItem", typeof (IntPtr)));
			}
		}

		public void Add (string url) {
			ObjCMessaging.objc_msgSend (NativeObject, "addItem:", typeof (void), typeof (IntPtr), new WebHistoryItem (url).NativeObject);
		}
			
		public void GoForward () {
			ObjCMessaging.objc_msgSend (NativeObject, "goForward", typeof (void));
		}
			
		public void GoBack () {
			ObjCMessaging.objc_msgSend (NativeObject, "goBack", typeof (void));
		}

	}
}
