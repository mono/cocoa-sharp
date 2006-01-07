using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class SavePanel : Panel {
		private static string ObjectiveCName = "NSSavePanel";
		

		static SavePanel () {
			NativeClasses [typeof (SavePanel)] = Native.RegisterClass (typeof (SavePanel)); 
		}

		public SavePanel () : base () {}
		
		public SavePanel (IntPtr native_object) : base (native_object) {}

		public Cocoa.View AccessoryView {
			get {
				return (Cocoa.View) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "accessoryView", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setAccessoryView:", typeof (void), typeof (System.IntPtr), (value == null) ? IntPtr.Zero : value.NativeObject);
			}
		}
		
		public string Prompt {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "prompt", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setPrompt:", typeof (void), typeof (System.IntPtr), new Cocoa.String ((value == null) ? "" : value).NativeObject);
			}
		}

		public string NameFieldLabel {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "nameFieldLabel", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setNameFieldLabel:", typeof (void), typeof (System.IntPtr), new Cocoa.String ((value == null) ? "" : value).NativeObject);
			}
		}

		public string Message {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "message", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setMessage:", typeof (void), typeof (System.IntPtr), new Cocoa.String ((value == null) ? "" : value).NativeObject);
			}
		}

		public bool CanSelectHiddenExtension {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "canSelectHiddenExtension", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setCanSelectHiddenExtension:", typeof (void), typeof (bool), value);
			}
		}

		public bool ExtensionHidden {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "isExtensionHidden", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setExtensionHidden:", typeof (void), typeof (bool), value);
			}
		}

		public string Directory {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "directory", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setDirectory:", typeof (void), typeof (System.IntPtr), (value == null) ? IntPtr.Zero : new Cocoa.String (value).NativeObject);
			}
		}

		public string RequiredFileType {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "requiredFileType", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setRequiredFileType:", typeof (void), typeof (System.IntPtr), (value == null) ? IntPtr.Zero : new Cocoa.String (value).NativeObject);
			}
		}

		public string [] AllowedFileTypes {
			get {
				return (string []) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "allowedFileTypes", typeof (System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setAllowedFileTypes:", typeof (void), typeof (System.IntPtr), (value == null) ? IntPtr.Zero : new MutableArray (value).NativeObject);
			}
		}

		public bool AllowsOtherFileTypes {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "allowsOtherFileTypes", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setAllowsOtherFileTypes:", typeof (void), typeof (bool), value);
			}
		}

		public bool TreatsFilePackagesAsDirectories {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "treatsFilePackagesAsDirectories", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setTreatsFilePackagesAsDirectories:", typeof (void), typeof (bool), value);
			}
		}

		public bool CanCreateDirectories {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "canCreateDirectories", typeof (bool));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setCanCreateDirectories:", typeof (void), typeof (bool), value);
			}
		}

		public string Filename {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "filename", typeof (System.IntPtr)));
			}
		}

		public Cocoa.URL URL {
			get {
				return (Cocoa.URL) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "URL", typeof (System.IntPtr)));
			}
		}

		public bool Expanded {
			get {
				return (bool) ObjCMessaging.objc_msgSend (NativeObject, "isExpanded", typeof (bool));
			}
		}

		public int RunModal () {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "runModal", typeof (System.Int32));
		}

		public int RunModal (string directory, string filename) {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "runModalForDirectory:file:", typeof (System.Int32), typeof (System.IntPtr), (directory == null) ? IntPtr.Zero : new Cocoa.String (directory).NativeObject, typeof (System.IntPtr), (filename == null) ? IntPtr.Zero : new Cocoa.String (filename).NativeObject);
		}

		public void BeginSheet (string directory, string filename, Cocoa.Window docWindow, SavePanelHandler modalDelegate, System.IntPtr contextInfo) {
			if (modalDelegate == null)
				throw new ArgumentNullException ("modalDelegate");
			Cocoa.Object target = (Cocoa.Object) modalDelegate.Target;
			MethodInfo method = modalDelegate.Method;
			string selector = method.Name;
			foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				if (export_attribute.Selector != null)
					selector = export_attribute.Selector;
			}
			ObjCMessaging.objc_msgSend (NativeObject, "beginSheetForDirectory:file:modalForWindow:modalDelegate:didEndSelector:contextInfo:", typeof (void), typeof (System.IntPtr), (directory == null) ? IntPtr.Zero : new Cocoa.String (directory).NativeObject, typeof (System.IntPtr), (filename == null) ? IntPtr.Zero : new Cocoa.String (filename).NativeObject, typeof (System.IntPtr), (docWindow == null) ? IntPtr.Zero : docWindow.NativeObject, typeof (System.IntPtr), target.NativeObject, typeof (System.IntPtr), Native.ToSelector (selector), typeof (System.IntPtr), contextInfo);
		}
	}
}
