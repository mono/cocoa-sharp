using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace WebKit {
	public class WebView : Cocoa.Object {
		private static string ObjectiveCName = "WebView";                                                                                      
		public WebView (IntPtr native_object) {
			NativeObject = native_object;
		}

		public bool HasBackForwardList {
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setMaintainsBackForwardList:", typeof (void), typeof (bool), value);
			}
		}

		public WebBackForwardList BackForwardList {
			get {
				return (WebBackForwardList) Cocoa.Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "backForwardList", typeof (IntPtr)));
			}
		}

		public WebFrame MainFrame {
			get {
				return (WebFrame) Cocoa.Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "mainFrame", typeof (IntPtr)));
			}
		}

		public void Render (string content) {
			MainFrame.Render (content);
		}
	}
}
