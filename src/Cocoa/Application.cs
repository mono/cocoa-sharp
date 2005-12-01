using System;
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
		
		[DllImport ("/System/Library/Frameworks/AppKit.framework/AppKit")]
		private static extern int strlen (string str);
	}
}
