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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSDrawer.cs,v 1.5 2004/06/17 17:41:20 gnorton Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSDrawer : NSObject {
		protected internal static IntPtr NSDrawer_class = Class.Get("NSDrawer");

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSDrawer_initWithContentSize_preferredEdge(IntPtr THIS, NSSize size, NSRectEdge edge);

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSDrawer_toggle(IntPtr THIS, IntPtr SENDER);

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSDrawer_setParentWindow(IntPtr THIS, IntPtr WINDOW);
		
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSDrawer_setContentView(IntPtr THIS, IntPtr VIEW);

		public NSDrawer() : this(NSObject__alloc(NSDrawer_class),true) {}
		protected internal NSDrawer(IntPtr raw,bool release) : base (raw,release) {}

		public NSDrawer(NSSize size, NSRectEdge edge) {
			SetRaw(NSObject__alloc(NSDrawer_class), true);
			initWithContentSize_preferredEdge(size, edge);
		}

		public NSDrawer initWithContentSize_preferredEdge(NSSize size, NSRectEdge edge) {
			SetRaw(NSDrawer_initWithContentSize_preferredEdge(Raw, size, edge),_release);
			return this;
		}

		public NSWindow ParentWindow {
			set { NSDrawer_setParentWindow(Raw, value.Raw); }
		}
		
		public NSView ContentView {
			set { NSDrawer_setContentView(Raw, value.Raw); }
		}

		public void toggle(object sender) {
			NSDrawer_toggle(Raw, Net2NS(sender));
		}
	}
}

//***************************************************************************
//
// $Log: NSDrawer.cs,v $
// Revision 1.5  2004/06/17 17:41:20  gnorton
// API modification.
//
// Allow our inits to be called with crafted constructors.
//
// Revision 1.4  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.3  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
