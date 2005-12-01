using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CFTimer : Timer {
		private static string ObjectiveCName = "NSCFTimer";                                                                                      

		static CFTimer () {
			NativeClasses [typeof (CFTimer)] = Native.RegisterClass (typeof (CFTimer)); 
		}

		public CFTimer (IntPtr native_object) : base (native_object) {}
	}
}
