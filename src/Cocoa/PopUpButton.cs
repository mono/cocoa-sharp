using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class PopUpButton : Control {
		private static string ObjectiveCName = "NSPopUpButton";                                                                                      
		public PopUpButton (IntPtr native_object) : base (native_object) {}

		public string TitleOfSelectedItem {
			get {
				return Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "titleOfSelectedItem", typeof(System.IntPtr))).ToString ();
			}
		}

		public int NumberOfItems {
			get {
				return(int) ObjCMessaging.objc_msgSend(NativeObject, "numberOfItems", typeof(System.Int32));
			}
		}

		public void RemoveAllItems () {
			ObjCMessaging.objc_msgSend(NativeObject, "removeAllItems", typeof(void));
		}

		public void AddItemWithTitle (string title) {
			ObjCMessaging.objc_msgSend(NativeObject, "addItemWithTitle:", typeof(void), typeof(System.IntPtr), new Cocoa.String (title).NativeObject);
		}

		public void SelectItemAtIndex (int index) {
			ObjCMessaging.objc_msgSend(NativeObject, "selectItemAtIndex:", typeof(void), typeof(System.Int32), index);
		}

		public string ItemTitleAtIndex (int index) {
			return Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "itemTitleAtIndex:", typeof(System.IntPtr), typeof(System.Int32), index)).ToString ();
		}

	}
}
