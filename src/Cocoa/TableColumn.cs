using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class TableColumn : Cocoa.Object {
		private static string ObjectiveCName = "NSTableColumn";                                                                                      

		static TableColumn () {
			NativeClasses [typeof (TableColumn)] = Native.RegisterClass (typeof (TableColumn)); 
		}

		public TableColumn (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object Identifier {
			get {
				return (Cocoa.Object) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "identifier", typeof (IntPtr)));
			}
		}
	}
}
