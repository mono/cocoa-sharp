using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CFTimer : Timer {
		private static string ObjectiveCName = "NSCFTimer";                                                                                      
		public CFTimer (IntPtr native_object) : base (native_object) {}
	}
}
