using System;

namespace Cocoa {
	public class URLResponse : Object {
		public static string ObjectiveCName = "NSURLResponse";

		static URLResponse () {
			NativeClasses [typeof (URLResponse)] = Native.RegisterClass (typeof (URLResponse));
		} 

		public URLResponse () : base () {}

		public URLResponse (IntPtr native_object) : base (native_object) {}

	}
}
