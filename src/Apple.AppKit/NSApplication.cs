//
//  NSApplication.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSApplication.cs,v 1.9 2004/06/23 17:55:46 urs Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{

	enum NSBackingStoretype {
		NSBackingStoreRetained = 0,
		NSBackingStoreNonretained = 1,
		NSBackingStoreBuffered = 2,
	};

	public class NSApplication : NSResponder {
		protected internal static IntPtr NSApplication_class = Class.Get("NSApplication");

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSApplication__sharedApplication0(IntPtr CLASS);

		[DllImport("AppKitGlue")]
		protected internal static extern int NSApplication_runModalForWindow1(IntPtr THIS, IntPtr theWindow);

		[DllImport("AppKitGlue")]
		protected internal static extern int NSApplication_setApplicationIconImage1(IntPtr THIS, IntPtr image);

		[DllImport("AppKitGlue")]
		protected internal static extern void NSApplication_stopModal0(IntPtr THIS);

		private NSApplication() : this(IntPtr.Zero,false) {}
		protected internal NSApplication(IntPtr raw,bool release) : base(raw,release) {}

		public static NSApplication sharedApplication()
		{
			return new NSApplication(NSApplication__sharedApplication0(IntPtr.Zero),false);
		}
		
		public void setApplicationIconImage(NSImage image)
		{
			NSApplication_setApplicationIconImage1(Raw, image.Raw);
		}

		public void stopModal()
		{
			NSApplication_stopModal0(Raw);
		}

		public int runModalForWindow(NSWindow theWindow)
		{
			return NSApplication_runModalForWindow1(Raw, theWindow.Raw);
		}
	}
}

//***************************************************************************
//
// $Log: NSApplication.cs,v $
// Revision 1.9  2004/06/23 17:55:46  urs
// Make test compile with the lasted glue API name change
//
// Revision 1.8  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.7  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.6  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
