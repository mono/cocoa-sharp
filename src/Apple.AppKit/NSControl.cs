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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSControl.cs,v 1.10 2004/06/23 17:55:46 urs Exp $
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
		protected internal static extern IntPtr NSControl_initWithFrame1(IntPtr THIS, NSRect frameRect);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_target0(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setTarget1(IntPtr THIS, IntPtr anObject);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_action0(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setAction1(IntPtr THIS, IntPtr aSelector);
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_stringValue0(IntPtr THIS);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setStringValue1(IntPtr THIS, IntPtr aString);
		
		protected NSControl(IntPtr raw,bool release) : base(raw,release) {}

		public NSControl() : this(NSObject__alloc0(NSControl_class),true) {}
		public NSControl(NSRect frameRect) : this(NSObject__alloc0(NSControl_class),true) {
		    initWithFrame(frameRect);
		}

		new public NSControl initWithFrame(NSRect frameRect)
		{
			SetRaw(NSControl_initWithFrame1(Raw,frameRect),_release);
			return this;
		}
	
		public object Target
		{
			get { return NS2Net(NSControl_target0(Raw)); } set { NSControl_setTarget1(Raw, Net2NS(value)); }
		}

		public string Action
		{
			get { return NSString.FromSEL(NSControl_action0(Raw)).ToString(); } set { NSControl_setAction1(Raw, NSString.NSSelector(value)); }
		}

		public string StringValue
		{
			get { 
				return NS2Net(NSControl_stringValue0(Raw)).ToString();
			} set { 
				NSControl_setStringValue1(Raw, Net2NS(value)); 
			}
		}
	}
}

//***************************************************************************
//
// $Log: NSControl.cs,v $
// Revision 1.10  2004/06/23 17:55:46  urs
// Make test compile with the lasted glue API name change
//
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
