using System;

namespace Cocoa {
	public class TableView : Control {
		private static string ObjectiveCName = "NSTableView";

		static TableView() {
			NativeClasses [typeof (TableView)] = Native.RegisterClass (typeof (TableView)); 
		}

		public TableView (Rect frame) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), frame);
		}

		public TableView (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object DataSource {
			get {
				return(Cocoa.Object) Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "dataSource", typeof(System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setDataSource:", typeof (void), typeof (System.IntPtr), value.NativeObject);
			}
		}

		public void AddTableColumn (TableColumn column) {
			ObjCMessaging.objc_msgSend (NativeObject, "addTableColumn:", typeof (void), typeof (IntPtr), column.NativeObject);
		}

		public void ReloadData () {
			ObjCMessaging.objc_msgSend (NativeObject, "reloadData", typeof (void));
		}

	}
}
