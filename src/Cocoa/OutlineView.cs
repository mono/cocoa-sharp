using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class OutlineView : Control {
		private static string ObjectiveCName = "NSOutlineView";                                                                                      

		static OutlineView () {
			NativeClasses [typeof (OutlineView)] = Native.RegisterClass (typeof (OutlineView)); 
		}

		public OutlineView (IntPtr native_object) : base (native_object) {}
		
		public Cocoa.Object SelectedItem {
			get {
				return ItemAtRow (SelectedRow);
			}
		}
	
		public int SelectedRow {
			get {
				return (int)ObjCMessaging.objc_msgSend (NativeObject, "selectedRow", typeof (int));
			}
		}
	
		public Cocoa.Object ItemAtRow (int row) {
			return (Cocoa.Object) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "itemAtRow:", typeof (IntPtr), typeof (int), row));
		}
	
		public void ExpandItem (Cocoa.Object item) {
			ObjCMessaging.objc_msgSend (NativeObject, "expandItem:", typeof (void), typeof (IntPtr), item.NativeObject);
		}
	}
}
