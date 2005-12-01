using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class BrowserCell : Cell {
		private static string ObjectiveCName = "NSBrowserCell";                                                                                      

		static BrowserCell () {
			NativeClasses [typeof (BrowserCell)] = Native.RegisterClass (typeof (BrowserCell)); 
		}

		public BrowserCell (IntPtr native_object) : base (native_object) {}

		public bool LeafNode {
			get {
				return (bool)ObjCMessaging.objc_msgSend (NativeObject, "isLeaf", typeof (void));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setLeaf:", typeof (void), typeof (bool), value);
			}
		}
	}
}
