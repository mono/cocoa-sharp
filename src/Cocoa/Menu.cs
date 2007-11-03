using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Menu : Cocoa.Object {
		private static string ObjectiveCName = "NSMenu";

		public Menu (string title) : base() {
			Init (title);
		}

		public Menu () : base() {
			Init ("");
		}

        public Menu(IntPtr native_object) : base(native_object){
        }

		public MenuItem AddItem (string title, ActionHandler action, string keyEquivalent) {
			MenuItem item = (MenuItem) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (
                NativeObject, "addItemWithTitle:action:keyEquivalent:", 
                typeof (IntPtr), 
				typeof (IntPtr), new Cocoa.String (title).NativeObject, 
				typeof (IntPtr), IntPtr.Zero, 
				typeof (IntPtr), new Cocoa.String (keyEquivalent).NativeObject));
			if (action != null)
				item.Click += action;
			return item;
		}
		

		public MenuItem AddItem (string title, string action, string keyEquivalent) {
			return (MenuItem) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "addItemWithTitle:action:keyEquivalent:", typeof (IntPtr),
				typeof (IntPtr), new Cocoa.String (title).NativeObject,
				typeof (IntPtr), ObjCMethods.sel_getUid (action),
				typeof (IntPtr), new Cocoa.String (keyEquivalent).NativeObject));
		}
		

		public void AddItem (MenuItem item) {
			 ObjCMessaging.objc_msgSend (NativeObject, "addItem:", typeof (void), typeof (IntPtr), item.NativeObject);
		}

		private void Init (string title) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithTitle:", typeof (System.IntPtr), typeof (IntPtr), new Cocoa.String (title).NativeObject);
		}

        public MenuItem ItemWithTag( int tag ){
            return (MenuItem) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (
                NativeObject, "itemWithTag:", 
                typeof (IntPtr),
                typeof(int), tag));
        }

        public MenuItem ItemWithTitle( string title ){
            return (MenuItem) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (
                NativeObject, "itemWithTitle:", 
                typeof (IntPtr),
                typeof(IntPtr), new Cocoa.String(title).NativeObject));
        }

        public string Title
        {
            get
            {
                return Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "title", typeof(IntPtr))).ToString();
            }
            set
            {
                ObjCMessaging.objc_msgSend(NativeObject, "setTitle:", typeof(IntPtr), typeof(IntPtr), new Cocoa.String(value).NativeObject);
            }
        }

	}
}
