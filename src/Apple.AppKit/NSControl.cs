//
//  NSControl.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSControl.cs,v 1.9 2004/06/19 20:42:59 gnorton Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSControl : NSView {
		protected internal static IntPtr NSControl_class = Class.Get("NSControl");

		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_initWithFrame(IntPtr THIS, NSRect frameRect);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_target(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setTarget(IntPtr THIS, IntPtr anObject);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_action(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setAction(IntPtr THIS, IntPtr aSelector);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_stringValue(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setStringValue(IntPtr THIS, IntPtr aString);
		
		protected NSControl(IntPtr raw,bool release) : base(raw,release) {}

		public NSControl() : this(NSObject__alloc(NSControl_class),true) {}
		public NSControl(NSRect frameRect) : this(NSObject__alloc(NSControl_class),true) {
		    initWithFrame(frameRect);
		}

		new public NSControl initWithFrame(NSRect frameRect)
		{
			SetRaw(NSControl_initWithFrame(Raw,frameRect),_release);
			return this;
		}
	
		public object Target
		{
			get { return NS2Net(NSControl_target(Raw)); } set { NSControl_setTarget(Raw, Net2NS(value)); }
		}

		public string Action
		{
			get { return NSString.FromSEL(NSControl_action(Raw)).ToString(); } set { NSControl_setAction(Raw, NSString.NSSelector(value)); }
		}

		public string StringValue
		{
			get { 
				return NS2Net(NSControl_stringValue(Raw)).ToString();
			} set { 
				NSControl_setStringValue(Raw, Net2NS(value)); 
			}
		}
	}
}

//***************************************************************************
//
// $Log: NSControl.cs,v $
// Revision 1.9  2004/06/19 20:42:59  gnorton
// Code cleanup (remove some old methods/clean some console.writelines)
// Modify NS2Net and NSObject destructor to be able to FreeCoTaskMem that we allocate in our argument parser.
//
// Revision 1.8  2004/06/19 17:19:27  gnorton
// Broken API fixes.
// Delegates and methods with multi-argument support working.
// Argument parsing and casting working for all our known classes.
//
// Revision 1.7  2004/06/19 02:34:32  urs
// some cleanup
//
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
