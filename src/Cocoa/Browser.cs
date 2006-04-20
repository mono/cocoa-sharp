using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Browser : Control {
		private static string ObjectiveCName = "NSBrowser";                                                                                      
		public Browser (IntPtr native_object) : base (native_object) {}
		
		public int SelectedRowInColumn (int column) {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "selectedRowInColumn:", typeof (int), typeof (int), column);
		}

		public void SelectRowInColumn (int row, int column) {
			ObjCMessaging.objc_msgSend (NativeObject, "selectRow:inColumn:", typeof (void), typeof (int), row, typeof (int), column);
		}
	}
}
