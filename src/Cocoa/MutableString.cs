using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class MutableString : String {
		private static string ObjectiveCName = "NSMutableString";                                                                                      
		public MutableString (IntPtr native_object) : base (native_object) {}
	}
}
