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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSButton.cs,v 1.13 2004/06/23 17:55:46 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
    using Apple.Foundation;

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
		protected internal static extern IntPtr NSButton_title0(IntPtr THIS);

		[DllImport("AppKitGlue")]
		protected internal static extern void NSButton_setTitle1(IntPtr THIS, IntPtr aString);
		
		[DllImport("AppKitGlue")]
		protected internal static extern NSBezelStyle NSButton_bezelStyle0(IntPtr THIS);
		
		[DllImport("AppKitGlue")]
		protected internal static extern void NSButton_setBezelStyle1(IntPtr THIS, NSBezelStyle style);
		
		protected internal NSButton(IntPtr raw,bool release) : base(raw,release) {}

		public NSButton() : this(NSObject__alloc0(NSButton_class),true) {}
		public NSButton(NSRect rect) : this(NSObject__alloc0(NSButton_class), true) {
		    initWithFrame(rect);
		}

		public string Title {
			get { return (string)NS2Net(NSButton_title0(Raw)); } set { NSButton_setTitle1(Raw, Net2NS(value)); }
		}

		public NSBezelStyle BezelStyle {
			get { return NSButton_bezelStyle0(Raw); } set { NSButton_setBezelStyle1(Raw, value); }
		}
	}
}

//***************************************************************************
//
// $Log: NSButton.cs,v $
// Revision 1.13  2004/06/23 17:55:46  urs
// Make test compile with the lasted glue API name change
//
// Revision 1.12  2004/06/19 17:19:27  gnorton
// Broken API fixes.
// Delegates and methods with multi-argument support working.
// Argument parsing and casting working for all our known classes.
//
// Revision 1.11  2004/06/19 02:34:32  urs
// some cleanup
//
// Revision 1.10  2004/06/18 13:54:57  urs
// *** empty log message ***
//
// Revision 1.9  2004/06/17 17:41:20  gnorton
// API modification.
//
// Allow our inits to be called with crafted constructors.
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
