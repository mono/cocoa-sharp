//
//  NSDrawer.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSDrawer.cs,v 1.2 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSDrawer : NSObject {
		static IntPtr NSDrawer_class = Apple.Foundation.NSString.NSClass("NSDrawer");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_initWithContentSize_preferredEdge(IntPtr THIS, NSSize size, NSRectEdge edge);

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_toggle(IntPtr THIS, IntPtr SENDER);

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_setParentWindow(IntPtr THIS, IntPtr WINDOW);
		
		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_setContentView(IntPtr THIS, IntPtr VIEW);

		public NSDrawer() : this(NSObject__alloc(NSDrawer_class)) {}
		protected internal NSDrawer(IntPtr raw) : base (raw) {}

		public IntPtr initWithContentSize_preferredEdge(NSSize size, NSRectEdge edge) {
			return NSDrawer_initWithContentSize_preferredEdge(Raw, size, edge);
		}

		public void setParentWindow(NSWindow window) {
			NSDrawer_setParentWindow(Raw, window.Raw);
		}
		
		public void setContentView(NSView view) {
			NSDrawer_setContentView(Raw, view.Raw);
		}

		public void toggle(IntPtr sender) {
			NSDrawer_toggle(Raw, sender);
		}
	}
}

//***************************************************************************
//
// $Log: NSDrawer.cs,v $
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
