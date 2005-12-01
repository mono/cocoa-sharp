using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace WebKit {
	public class WebFrame : Cocoa.Object {
		private static string ObjectiveCName = "WebFrame";                                                                                      

		static WebFrame () {
			NativeClasses [typeof (WebFrame)] = Native.RegisterClass (typeof (WebFrame)); 
		}

		public WebFrame (IntPtr native_object) {
			NativeObject = native_object;
		}

		public void Render (string content) {
			Cocoa.String native_content = new Cocoa.String (content);
			ObjCMessaging.objc_msgSend (NativeObject, "loadHTMLString:baseURL:", typeof (void), typeof (IntPtr), native_content.NativeObject, typeof (IntPtr), IntPtr.Zero);
		}
	}
}
