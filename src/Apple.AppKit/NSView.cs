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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSView.cs,v 1.3 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSView : NSResponder {
                static IntPtr NSView_class = Apple.Foundation.NSString.NSClass("NSView");

		[DllImport("AppKitGlue")]
		static extern void NSView_addSubview(IntPtr THIS, IntPtr aView);
		
		[DllImport("AppKitGlue")]
		static extern IntPtr NSView_initWithFrame(IntPtr THIS, NSRect frame);
		
		public NSView() : this(NSObject__alloc(NSView_class)) {}
		protected internal NSView(IntPtr raw) : base(raw) {}

		virtual public IntPtr initWithFrame(NSRect frame) {
			return NSView_initWithFrame(Raw, frame);
		}

		public void addSubview(NSView aView) {
			NSView_addSubview(Raw, aView.Raw);
		}
	}
}

//***************************************************************************
//
// $Log: NSView.cs,v $
// Revision 1.3  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
