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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSWindow.cs,v 1.6 2004/06/17 13:06:27 urs Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSWindow : NSResponder {
		static IntPtr NSWindow_class = Apple.Foundation.NSString.NSClass("NSWindow");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSWindow_initWithContentRect_styleMask_backing_defer(IntPtr THIS, NSRect contentRec, uint aStyle, int bufferingType, bool flag);

		[DllImport("AppKitGlue")]
		static extern void NSWindow_setTitle(IntPtr THIS, IntPtr aString);

		[DllImport("AppKitGlue")]
		static extern void NSWindow_center(IntPtr THIS);

		[DllImport("AppKitGlue")]
		static extern void NSWindow_makeKeyAndOrderFront(IntPtr THIS, IntPtr sender);

		[DllImport("AppKitGlue")]
		static extern IntPtr NSWindow_contentView(IntPtr THIS);

		public NSWindow() : this(NSObject__alloc(NSWindow_class),true) {}
		protected internal NSWindow(IntPtr raw,bool release) : base (raw,release) {}

		public IntPtr initWithContentRect_styleMask_backing_defer(NSRect contentRect, uint aStyle, int bufferingType, bool flag)
		{
			return NSWindow_initWithContentRect_styleMask_backing_defer(Raw, contentRect, aStyle, bufferingType, flag);
		}

		public void setTitle(NSString aString)
		{
			NSWindow_setTitle(Raw, aString.Raw);
		}

		public void center()
		{
			NSWindow_center(Raw);
		}
		
		public void makeKeyAndOrderFront(NSObject sender)
		{
			NSWindow_makeKeyAndOrderFront(Raw, sender == null ? IntPtr.Zero : sender.Raw);
		}
		
		public NSObject contentView()
		{
			return new NSView(NSWindow_contentView(Raw),false);
		}
	}
}

//***************************************************************************
//
// $Log: NSWindow.cs,v $
// Revision 1.6  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.5  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
