using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CFArray : Array {
		private static string ObjectiveCName = "NSCFArray";                                                                                      
		public CFArray (IntPtr native_object) : base (native_object) {}
	}
}
