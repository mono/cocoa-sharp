using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CalibratedRGBColor : Color {
		private static string ObjectiveCName = "NSCalibratedRGBColor";                                                                                      
		public CalibratedRGBColor (IntPtr native_object) : base (native_object) {}
	}
}
