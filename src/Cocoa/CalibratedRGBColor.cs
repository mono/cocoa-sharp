using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class CalibratedRGBColor : Color {
		private static string ObjectiveCName = "NSCalibratedRGBColor";                                                                                      

		static CalibratedRGBColor () {
			NativeClasses [typeof (CalibratedRGBColor)] = Native.RegisterClass (typeof (CalibratedRGBColor)); 
		}

		public CalibratedRGBColor (IntPtr native_object) : base (native_object) {}
	}
}
