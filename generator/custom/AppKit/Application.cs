//
//  Application.cs
//
//  (c) 2004 Geoff Norton

using System;
using Apple.Foundation;
using Apple.AppKit;
using Apple.Tools;

namespace Apple.AppKit
{
	public class Application
	{
		static Application ()
		{
		}

		public static void Init ()
		{
			// We import Foundation and AppKit by default
			LoadFramework ("Foundation");
			LoadFramework ("AppKit");
		}

		public static void Run ()
		{
			((NSApplication)NSApplication.SharedApplication).run ();
		}

		public static void LoadFramework (string frameworkName)
		{
			NSBundle frmwrkBundle = (NSBundle)NSBundle.BundleWithPath (new NSString ("/System/Library/Frameworks/" + frameworkName + ".framework"));
			frmwrkBundle.load ();
		}
			
		public static bool LoadNib (string nibName)
		{
			NSApplication sharedApp = (NSApplication)NSApplication.SharedApplication;
			ObjCMessaging.objc_msgSend(Apple.Foundation.Class.Get("NSBundle"),"loadNibNamed:owner:",typeof(System.SByte), typeof(System.IntPtr), NSObject.Net2NS (new NSString(nibName)), typeof(System.IntPtr), NSObject.Net2NS (sharedApp));
			return true;
		}
	}
}
