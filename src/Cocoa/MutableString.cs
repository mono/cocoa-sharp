using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class MutableString : String {
		private static string ObjectiveCName = "NSMutableString";                                                                                      

		static MutableString () {
			NativeClasses [typeof (MutableString)] = Native.RegisterClass (typeof (MutableString)); 
		}

		public MutableString (IntPtr native_object) : base (native_object) {}
	}
}
