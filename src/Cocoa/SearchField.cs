using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class SearchField : Control {
		private static string ObjectiveCName = "NSSearchField";                                                                                      

		static SearchField () {
			NativeClasses [typeof (SearchField)] = Native.RegisterClass (typeof (SearchField)); 
		}

		public SearchField (IntPtr native_object) : base (native_object) {}

	}
}
