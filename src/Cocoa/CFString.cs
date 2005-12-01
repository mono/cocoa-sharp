using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CFString : String {
		private static string ObjectiveCName = "NSCFString";                                                                                      

		static CFString () {
			NativeClasses [typeof (CFString)] = Native.RegisterClass (typeof (CFString)); 
		}

		public CFString (IntPtr native_object) : base (native_object) {}
	}
}
