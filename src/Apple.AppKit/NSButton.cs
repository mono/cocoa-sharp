//
//  NSButton.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSButton.cs,v 1.8 2004/06/17 15:58:07 urs Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public enum NSBezelStyle {
		NSRoundedBezelStyle = 1,
		NSRegularSquareBezelStyle = 2,
		NSThickSquareBezelStyle = 3,
		NSThickerSquareBezelStyle = 4,
		NSDisclosureBezelStyle = 5,
		NSShadowlessSquareBezelStyle = 6,
		NSCircularBezelStyle = 7,
		NSTexturedSquareBezelStyle = 8,
		NSHelpButtonBezelStyle = 9
	} 

	public class NSButton : NSControl {
		protected internal static IntPtr NSButton_class = Class.Get("NSButton");

		[DllImport("AppKitGlue")]
		protected internal static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
		
		[DllImport("AppKitGlue")]
		protected internal static extern void NSButton_setBezelStyle(IntPtr THIS, NSBezelStyle style);
		
		public NSButton() : this(NSObject__alloc(NSButton_class),true) {}
		protected internal NSButton(IntPtr raw,bool release) : base(raw,release) {}

		public string Title {
			set { NSButton_setTitle(Raw, Net2NS(value)); }
		}

		public NSBezelStyle BezelStyle {
			set { NSButton_setBezelStyle(Raw, value); }
		}
	}
}

//***************************************************************************
//
// $Log: NSButton.cs,v $
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
