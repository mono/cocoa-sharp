//
//  NSView.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSView.cs,v 1.6 2004/06/17 17:41:20 gnorton Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSView : NSResponder {
		protected internal static IntPtr NSView_class = Class.Get("NSView");

		[DllImport("AppKitGlue")]
		protected internal static extern void NSView_addSubview(IntPtr THIS, IntPtr aView);
		
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSView_initWithFrame(IntPtr THIS, NSRect frame);
		
		public NSView() : this(NSObject__alloc(NSView_class),true) {}
		protected internal NSView(IntPtr raw,bool release) : base(raw,release) {}
		public NSView(NSRect frame) {
			SetRaw(NSObject__alloc(NSView_class), true);
			initWithFrame(frame);
		}

		virtual public NSView initWithFrame(NSRect frame) {
			SetRaw(NSView_initWithFrame(Raw, frame),_release);
			return this;
		}

		public void addSubview(NSView aView) {
			NSView_addSubview(Raw, aView.Raw);
		}
	}
}

//***************************************************************************
//
// $Log: NSView.cs,v $
// Revision 1.6  2004/06/17 17:41:20  gnorton
// API modification.
//
// Allow our inits to be called with crafted constructors.
//
// Revision 1.5  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.4  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.3  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
