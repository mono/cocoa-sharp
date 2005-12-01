using System;
using System.Runtime.InteropServices;

namespace Cocoa {
	public class Notification : Cocoa.Object {
		private static string ObjectiveCName = "NSNotification";                                                                                      
		static Notification () {
			NativeClasses [typeof (Notification)] = Native.RegisterClass (typeof (Notification)); 
		}

		public Notification () : base () {}

		public Notification (IntPtr native_object) : base (native_object) {}
	}
}
