using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class SearchField : TextField {
		private static string ObjectiveCName = "NSSearchField";                                                                                      
		public SearchField (IntPtr native_object) : base (native_object) {}

	}
}
