using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class OpenPanel : SavePanel {
		private static string ObjectiveCName = "NSOpenPanel";
		
		public OpenPanel () : base () {}
		
		public OpenPanel (IntPtr native_object) : base (native_object) {}

		public bool AllowsMultipleSelection {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "allowsMultipleSelection", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setAllowsMultipleSelection:", typeof (void), typeof (bool), value);
			}
		}

		public bool CanChooseDirectories {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "canChooseDirectories", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setCanChooseDirectories:", typeof (void), typeof (bool), value);
			}
		}

		public bool CanChooseFiles {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "canChooseFiles", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setCanChooseFiles:", typeof (void), typeof (bool), value);
			}
		}

		public Cocoa.URL [] URLs {
			get {
				return (Cocoa.URL []) ((Array)Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "URLs",typeof (System.IntPtr)))).ToArray ();
			}
		}

		public string [] Filenames {
			get {
				return (string []) ((Array) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "filenames", typeof (System.IntPtr)))).ToArray ();
			}
		}

		public int RunModal (string [] fileTypes) {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "runModalForTypes:", typeof (System.Int32), typeof (System.IntPtr), (fileTypes == null) ? IntPtr.Zero : new MutableArray (fileTypes).NativeObject);
		}

		public int RunModal (string directory, string filename, string [] fileTypes) {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "runModalForDirectory:file:types:", typeof (System.Int32), typeof (System.IntPtr), (directory == null) ? IntPtr.Zero : new Cocoa.String (directory).NativeObject, typeof (System.IntPtr), (filename == null) ? IntPtr.Zero : new Cocoa.String (filename).NativeObject, typeof (System.IntPtr), (fileTypes == null) ? IntPtr.Zero : new MutableArray (fileTypes).NativeObject);
		}

		public void BeginSheet (Cocoa.Window docWindow, OpenPanelHandler modalDelegate, System.IntPtr contextInfo) {
			if (modalDelegate == null)
				throw new ArgumentNullException ("modalDelegate");
			Cocoa.Object target = (Cocoa.Object) modalDelegate.Target;
			MethodInfo method = modalDelegate.Method;
			string selector = method.Name;
			foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				if (export_attribute.Selector != null)
					selector = export_attribute.Selector;
			}
			ObjCMessaging.objc_msgSend (NativeObject, "beginSheetForDirectory:file:types:modalForWindow:modalDelegate:didEndSelector:contextInfo:", typeof (void), typeof (System.IntPtr), IntPtr.Zero, typeof (System.IntPtr), IntPtr.Zero, typeof (System.IntPtr), IntPtr.Zero, typeof (System.IntPtr), (docWindow == null) ? IntPtr.Zero : docWindow.NativeObject, typeof (System.IntPtr), target.NativeObject, typeof (System.IntPtr), ObjCMethods.sel_getUid (selector), typeof (System.IntPtr), contextInfo);
		}
		
		public void BeginSheet (string directory, string filename, string [] fileTypes, Cocoa.Window docWindow, OpenPanelHandler modalDelegate, System.IntPtr contextInfo) {
			if (modalDelegate == null)
				throw new ArgumentNullException ("modalDelegate");
			Cocoa.Object target = (Cocoa.Object) modalDelegate.Target;
			MethodInfo method = modalDelegate.Method;
			string selector = method.Name;
			foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				if (export_attribute.Selector != null)
					selector = export_attribute.Selector;
			}
			ObjCMessaging.objc_msgSend (NativeObject, "beginSheetForDirectory:file:types:modalForWindow:modalDelegate:didEndSelector:contextInfo:", typeof (void), typeof (System.IntPtr), (directory == null) ? IntPtr.Zero : new Cocoa.String (directory).NativeObject, typeof (System.IntPtr), (filename == null) ? IntPtr.Zero : new Cocoa.String (filename).NativeObject, typeof (System.IntPtr), (fileTypes == null) ? IntPtr.Zero : new MutableArray (fileTypes).NativeObject, typeof (System.IntPtr), (docWindow == null) ? IntPtr.Zero : docWindow.NativeObject, typeof (System.IntPtr), target.NativeObject, typeof (System.IntPtr), ObjCMethods.sel_getUid (selector), typeof (System.IntPtr), contextInfo);
		}
	}
}
