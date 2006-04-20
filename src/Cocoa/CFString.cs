using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CFString : String {
		private static string ObjectiveCName = "NSCFString";                                                                                      
		public CFString (IntPtr native_object) : base (native_object) {}
	}
}
