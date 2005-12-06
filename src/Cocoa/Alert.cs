using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Alert : Cocoa.Object {
		private static string ObjectiveCName = "NSAlert";

		
		static Alert () {
			NativeClasses [typeof (Alert)] = Native.RegisterClass (typeof (Alert)); 
		}

		public Alert (IntPtr native_object) : base (native_object) {}

		public Cocoa.AlertStyle AlertStyle {
			get { 
				return(Cocoa.AlertStyle)ObjCMessaging.objc_msgSend(NativeObject, "alertStyle", typeof(System.Int32));
			}
			set { 
				ObjCMessaging.objc_msgSend(NativeObject, "setAlertStyle:", typeof(void), typeof(System.Int32), value);
			}
		}

		public Image Icon {
			get {
				return(Image) Native.NativeToManaged((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "icon", typeof(System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setIcon:", typeof(void), typeof(System.IntPtr), ((Image) value).NativeObject);
			}
		}

		public static Alert AlertWithMessage (string messageTitle, string defaultButtonTitle, string alternateButtonTitle, string otherButtonTitle, string format) {
			return(Alert) Native.NativeToManaged (
				(IntPtr) ObjCMessaging.objc_msgSend (
					(IntPtr) NativeClasses [typeof (Alert)],
					"alertWithMessageText:defaultButton:alternateButton:otherButton:informativeTextWithFormat:",
					typeof(System.IntPtr),
					typeof(System.IntPtr),
					new Cocoa.String (messageTitle).NativeObject,
					typeof(System.IntPtr),
					new Cocoa.String (defaultButtonTitle).NativeObject,
					typeof(System.IntPtr),
					new Cocoa.String (alternateButtonTitle).NativeObject,
					typeof(System.IntPtr), 
					new Cocoa.String (otherButtonTitle).NativeObject, 
					typeof(System.IntPtr), 
					new Cocoa.String (format).NativeObject
				)
			);
		}

		public int RunModal() {
			return(int) ObjCMessaging.objc_msgSend(NativeObject, "runModal", typeof(System.Int32));
		}

	}
}
