using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Alert : Cocoa.Object {
		private static string ObjectiveCName = "NSAlert";

		public Alert () : base () {
			Initialize ();
		}

		public Alert (IntPtr native_object) : base (native_object) {}

		public string MessageTitle {
			get {
				return Object.FromIntPtr ((System.IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "messageText", typeof (System.IntPtr))).ToString ();
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setMessageText:", typeof (void), typeof (System.IntPtr), new Cocoa.String ((value == null) ? "" : value).NativeObject);
			}
		}

		public string InformativeText {
			get {
				return Object.FromIntPtr ((System.IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "informativeText", typeof (System.IntPtr))).ToString ();
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setInformativeText:", typeof (void), typeof (System.IntPtr), new Cocoa.String ((value == null) ? "" : value).NativeObject);
			}
		}

		public Cocoa.AlertStyle AlertStyle {
			get { 
				return (Cocoa.AlertStyle) ObjCMessaging.objc_msgSend (NativeObject, "alertStyle", typeof (System.Int32));
			}
			set { 
				ObjCMessaging.objc_msgSend (NativeObject, "setAlertStyle:", typeof (void), typeof (System.Int32), value);
			}
		}

		public Image Icon {
			get {
				return(Image) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "icon", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setIcon:", typeof (void), typeof (System.IntPtr), (value == null) ? IntPtr.Zero : ((Image) value).NativeObject);
			}
		}

		public Cocoa.Button AddButtonWithTitle (string buttonTitle) {
			if (buttonTitle == null)
				throw new ArgumentNullException ("buttonTitle");
			if (buttonTitle.Length == 0)
				throw new ArgumentException ("buttonTitle: The buttonTitle parameter must be a non-empty string.");
			return (Cocoa.Button) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "addButtonWithTitle:", typeof (System.IntPtr), typeof (System.IntPtr), new Cocoa.String (buttonTitle).NativeObject));
		}

		public static Alert AlertWithMessage (string messageTitle, string defaultButtonTitle, string alternateButtonTitle, string otherButtonTitle, string informativeText) {
			// Documentation indicates that nil values can be passed but they actually generate runtime errors.
			if (messageTitle == null) messageTitle = "";
			if ((defaultButtonTitle == null) || (defaultButtonTitle.Length == 0)) defaultButtonTitle = "OK";
			if (alternateButtonTitle == null) alternateButtonTitle = "";
			if (otherButtonTitle == null) otherButtonTitle = "";
			if (informativeText == null) informativeText = "";

			return(Alert) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (ObjCClass.FromType (typeof (Alert)).ToIntPtr (), "alertWithMessageText:defaultButton:alternateButton:otherButton:informativeTextWithFormat:", typeof (System.IntPtr), typeof (System.IntPtr), new Cocoa.String (messageTitle).NativeObject, typeof (System.IntPtr), new Cocoa.String (defaultButtonTitle).NativeObject, typeof (System.IntPtr), new Cocoa.String (alternateButtonTitle).NativeObject, typeof (System.IntPtr), new Cocoa.String (otherButtonTitle).NativeObject, typeof (System.IntPtr), new Cocoa.String (informativeText).NativeObject));
		}

		public int RunModal () {
			return(int) ObjCMessaging.objc_msgSend (NativeObject, "runModal", typeof (System.Int32));
		}

		public void BeginSheet (Cocoa.Window theWindow, AlertHandler modalDelegate, System.IntPtr contextInfo) {
			if (modalDelegate == null)
				throw new ArgumentNullException ("modalDelegate");
			Cocoa.Object target = (Cocoa.Object) modalDelegate.Target;
			MethodInfo method = modalDelegate.Method;
			string selector = method.Name;
			foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				if (export_attribute.Selector != null)
					selector = export_attribute.Selector;
			}
			ObjCMessaging.objc_msgSend (NativeObject, "beginSheetModalForWindow:modalDelegate:didEndSelector:contextInfo:", typeof (void), typeof (System.IntPtr), (theWindow == null) ? IntPtr.Zero : theWindow.NativeObject, typeof (System.IntPtr), target.NativeObject, typeof (System.IntPtr), ObjCMethods.sel_getUid (selector), typeof (System.IntPtr), contextInfo);
		}  
	}
}
