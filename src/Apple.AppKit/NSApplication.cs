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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSApplication.cs,v 1.6 2004/06/16 12:20:26 urs Exp $
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
		static IntPtr NSApplication_class = Apple.Foundation.NSString.NSClass("NSApplication");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSApplication__sharedApplication(IntPtr CLASS);

		[DllImport("AppKitGlue")]
		static extern int NSApplication_runModalForWindow(IntPtr THIS, IntPtr theWindow);

		[DllImport("AppKitGlue")]
		static extern int NSApplication_setApplicationIconImage(IntPtr THIS, IntPtr image);

		[DllImport("AppKitGlue")]
		static extern void NSApplication_stopModal(IntPtr THIS);

		private NSApplication() : this(IntPtr.Zero) {}
		protected internal NSApplication(IntPtr raw) : base(raw) {}

		public static NSApplication sharedApplication()
		{
			return new NSApplication(NSApplication__sharedApplication(NSApplication_class));
		}
		
		public static void setApplicationIconImage(NSImage image)
		{
			NSApplication_setApplicationIconImage(NSApplication__sharedApplication(NSApplication_class), image.Raw);
		}

		public static void stopModal()
		{
			NSApplication_stopModal(NSApplication__sharedApplication(NSApplication_class));
		}

		public int runModalForWindow(NSWindow theWindow)
		{
			return NSApplication_runModalForWindow(Raw, theWindow.Raw);
		}
	}
}

//***************************************************************************
//
// $Log: NSApplication.cs,v $
// Revision 1.6  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
