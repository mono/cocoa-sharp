//
//  NSWindow.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSWindow.cs,v 1.9 2004/06/23 17:55:46 urs Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSWindow : NSResponder {
		protected internal static IntPtr NSWindow_class = Class.Get("NSWindow");

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSWindow_initWithContentRect_styleMask_backing_defer4(IntPtr THIS, NSRect contentRec, uint aStyle, int bufferingType, bool flag);

		[DllImport("AppKitGlue")]
		protected internal static extern void NSWindow_setTitle1(IntPtr THIS, IntPtr aString);

		[DllImport("AppKitGlue")]
		protected internal static extern void NSWindow_center0(IntPtr THIS);

		[DllImport("AppKitGlue")]
		protected internal static extern void NSWindow_makeKeyAndOrderFront1(IntPtr THIS, IntPtr sender);

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSWindow_contentView0(IntPtr THIS);

		public NSWindow() : this(NSObject__alloc0(NSWindow_class),true) {}
		protected internal NSWindow(IntPtr raw,bool release) : base (raw,release) {}
		public NSWindow(NSRect contentRect, uint aStyle, int bufferingType, bool flag) {
			SetRaw(NSObject__alloc0(NSWindow_class), true);
			initWithContentRect_styleMask_backing_defer(contentRect, aStyle, bufferingType, flag);
		}
		public NSWindow initWithContentRect_styleMask_backing_defer(NSRect contentRect, uint aStyle, int bufferingType, bool flag)
		{
			SetRaw(NSWindow_initWithContentRect_styleMask_backing_defer4(Raw, contentRect, aStyle, bufferingType, flag),_release);
			return this;
		}

		public string Title
		{
			set { NSWindow_setTitle1(Raw, Net2NS(value)); }
		}

		public void center()
		{
			NSWindow_center0(Raw);
		}
		
		public void makeKeyAndOrderFront(object sender)
		{
			NSWindow_makeKeyAndOrderFront1(Raw, Net2NS(sender));
		}
		
		public NSObject contentView()
		{
			return (NSObject)NS2Net(NSWindow_contentView0(Raw));
		}
	}
}

//***************************************************************************
//
// $Log: NSWindow.cs,v $
// Revision 1.9  2004/06/23 17:55:46  urs
// Make test compile with the lasted glue API name change
//
// Revision 1.8  2004/06/17 17:41:20  gnorton
// API modification.
//
// Allow our inits to be called with crafted constructors.
//
// Revision 1.7  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.6  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.5  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
