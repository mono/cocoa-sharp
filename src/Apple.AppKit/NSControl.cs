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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSControl.cs,v 1.6 2004/06/17 17:41:20 gnorton Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSControl : NSView {
		[DllImport("AppKitGlue")]
		protected internal static extern IntPtr NSControl_initWithFrame(IntPtr THIS, NSRect frameRect);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setTarget(IntPtr THIS, IntPtr anObject);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setAction(IntPtr THIS, IntPtr aSelector);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSControl_setStringValue(IntPtr THIS, IntPtr aString);
		
		public NSControl() {}

		protected NSControl(IntPtr raw,bool release) : base(raw,release) {}

		new public NSControl initWithFrame(NSRect frameRect)
		{
			SetRaw(NSControl_initWithFrame(Raw,frameRect),_release);
			return this;
		}
	
		public object Target
		{
			set { NSControl_setTarget(Raw, Net2NS(value)); }
		}

		public string Action
		{
			set { NSControl_setAction(Raw, NSString.NSSelector(value)); }
		}

		public string StringValue
		{
			set { NSControl_setStringValue(Raw, Net2NS(value)); }
		}
	}
}

//***************************************************************************
//
// $Log: NSControl.cs,v $
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
