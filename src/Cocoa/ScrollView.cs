using System;

namespace Cocoa {
	public class ScrollView : View {
		private static string ObjectiveCName = "NSScrollView";

		public ScrollView (Rect frame) {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), frame);
}

		public ScrollView (IntPtr native) : base (native) {}

		public View DocumentView {
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setDocumentView:", typeof(void), typeof(System.IntPtr), value.NativeObject);
			}
		}

		public bool HasVerticalScroller {
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setHasVerticalScroller:", typeof(void), typeof(bool), value);
			}
		}
	}
}
