using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace WebKit {
	public class WebDataSource : Cocoa.Object {
		private static string ObjectiveCName = "WebDataSource";                                                                                      
		public WebDataSource (IntPtr native_object) {
			NativeObject = native_object;
		}

	}
}
