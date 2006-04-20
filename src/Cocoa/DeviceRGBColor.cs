using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class DeviceRGBColor : Color {
		private static string ObjectiveCName = "NSDeviceRGBColor";                                                                                      
		public DeviceRGBColor (IntPtr native_object) : base (native_object) {}
	}
}
