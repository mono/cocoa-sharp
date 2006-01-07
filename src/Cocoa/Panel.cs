using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Panel : Window {
		private static string ObjectiveCName = "NSPanel";

		
		public const int OKButton = 1;
		public const int CancelButton = 0;

		static Panel () {
			NativeClasses [typeof (Panel)] = Native.RegisterClass (typeof (Panel)); 
		}
		
		public Panel () : base () {}
		
		public Panel (IntPtr native_object) : base (native_object) {}

		public bool FloatingPanel {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "isFloatingPanel", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setFloatingPanel:", typeof (void), typeof (bool), value);
			}
		}

		public bool BecomesKeyOnlyIfNeeded {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "becomesKeyOnlyIfNeeded", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setBecomesKeyOnlyIfNeeded:", typeof (void), typeof (bool), value);
			}
		}

		public bool WorksWhenModal {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "worksWhenModal", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setWorksWhenModal:", typeof (void), typeof (bool), value);
			}
		}

	}
}
