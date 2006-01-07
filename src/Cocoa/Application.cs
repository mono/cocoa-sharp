using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Application : Cocoa.Object {
		private static string ObjectiveCName = "NSApplication";                                                                                      

		static Application () {
			NativeClasses [typeof (Application)] = Native.RegisterClass (typeof (Application)); 
			// This is a hideous hack to load the dylib into our address space
			strlen ("Load AppKit Into My Addressspace.");
		}

		public Application (IntPtr native_object) : base (native_object) {}

		private static AutoreleasePool pool;
	
		public static void Init () {
			pool = new AutoreleasePool ();
		}

		public static Application SharedApplication {
			get {
				return (Application)Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend ((IntPtr) Cocoa.Object.NativeClasses [typeof (Application)], "sharedApplication", typeof (IntPtr)));
			}
		}

		public Window MainWindow {
			get {
				return (Window)Native.NativeToManaged ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "mainWindow", typeof (IntPtr)));
			}
		}

		public Cocoa.Image Icon {
			get {
				return(Cocoa.Image) Native.NativeToManaged ((System.IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "applicationIconImage", typeof(System.IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setApplicationIconImage:", typeof(void), typeof(System.IntPtr), (value == null) ? IntPtr.Zero : ((Cocoa.Image) value).NativeObject);
			}
		}

		public static void LoadNib (string nibname) {
			Dictionary dict = new Dictionary ("NSOwner", Application.SharedApplication);

			Bundle.LoadNib (nibname);
		}

		public static void LoadFramework (string frameworkname) {
			Bundle.LoadFramework (frameworkname);
		}

		public static void Run () {
			Application.SharedApplication.RunApplication ();
		}

		public IntPtr ModalSessionForWindow (Window window) {
			return (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "beginModalSessionForWindow:", typeof (IntPtr), typeof (IntPtr), window.NativeObject);
		}
		
		public void EndModalSession (IntPtr modalsession) {
			ObjCMessaging.objc_msgSend (NativeObject, "endModalSession:", typeof (void), typeof (IntPtr), modalsession);
		}
		
		public int RunModalSession (IntPtr modalsession) {
			return (int)ObjCMessaging.objc_msgSend (NativeObject, "runModalSession:", typeof (int), typeof (IntPtr), modalsession);
		}

		public void RunApplication () {
			ObjCMessaging.objc_msgSend (NativeObject, "run", typeof (void));
		}

		public void BeginSheet (Cocoa.Window sheet, Cocoa.Window docWindow, SheetHandler modalDelegate, System.IntPtr contextInfo) {
			if (sheet == null)
				throw new ArgumentNullException ("sheet");
			if (modalDelegate == null)
				throw new ArgumentNullException ("modalDelegate");
			Cocoa.Object target = (Cocoa.Object) modalDelegate.Target;
			MethodInfo method = modalDelegate.Method;
			string selector = method.Name;
			foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				if (export_attribute.Selector != null)
					selector = export_attribute.Selector;
			}
			ObjCMessaging.objc_msgSend (NativeObject, "beginSheet:modalForWindow:modalDelegate:didEndSelector:contextInfo:", typeof(void), typeof(System.IntPtr), sheet.NativeObject, typeof(System.IntPtr), (docWindow == null) ? IntPtr.Zero : docWindow.NativeObject, typeof(System.IntPtr), target.NativeObject, typeof(System.IntPtr), Native.ToSelector (selector), typeof(System.IntPtr), contextInfo);
		}

		public void EndSheet (Cocoa.Window sheet) {
			if (sheet == null)
				throw new ArgumentNullException ("sheet");
			ObjCMessaging.objc_msgSend (NativeObject, "endSheet:", typeof (void), typeof (System.IntPtr), sheet.NativeObject);
		}

		public void EndSheet (Cocoa.Window sheet, int returnCode) {
			if (sheet == null)
				throw new ArgumentNullException ("sheet");
			ObjCMessaging.objc_msgSend (NativeObject, "endSheet:returnCode:", typeof (void), typeof (System.IntPtr), sheet.NativeObject, typeof (int), returnCode);
		}
		
		[DllImport ("/System/Library/Frameworks/AppKit.framework/AppKit")]
		private static extern int strlen (string str);
	}
}
