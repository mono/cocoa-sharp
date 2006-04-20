using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class TextField : Control {
		private static string ObjectiveCName = "NSTextField";                                                                                      
		public TextField (IntPtr native_object) : base (native_object) {}

	}
}
