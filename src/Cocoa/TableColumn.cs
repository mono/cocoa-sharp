using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class TableColumn : Cocoa.Object {
		private static string ObjectiveCName = "NSTableColumn";                                                                                      

		static TableColumn () {
			NativeClasses [typeof (TableColumn)] = Native.RegisterClass (typeof (TableColumn)); 
		}

		public TableColumn (Cocoa.Object identifier) : base () {
				NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithIdentifier:", typeof (IntPtr), typeof (IntPtr), identifier.NativeObject);
		}

		public TableColumn (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object Identifier {
			get {
				return (Cocoa.Object) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "identifier", typeof (IntPtr)));
			}
		}

		public float Width {
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setWidth:", typeof (void), typeof (float), value);
			}
		}

		public Cell HeaderCell {
			get {
				return (Cell) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "headerCell", typeof (IntPtr)));
			}
		}
	}
}
