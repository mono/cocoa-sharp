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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSControl.cs,v 1.4 2004/06/17 13:06:27 urs Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSControl : NSView {
		[DllImport("AppKitGlue")]
		static extern IntPtr NSControl_initWithFrame(IntPtr THIS, NSRect frameRect);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setTarget(IntPtr THIS, IntPtr anObject);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setAction(IntPtr THIS, IntPtr aSelector);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setStringValue(IntPtr THIS, IntPtr aString);
		
		private NSControl() {}

		protected NSControl(IntPtr raw,bool release) : base(raw,release) {}

		override public IntPtr initWithFrame(NSRect frameRect)
		{
			return NSControl_initWithFrame(Raw,frameRect);
		}
	
		public void setTarget(NSObject anObject)
		{
			NSControl_setTarget(Raw, anObject.Raw);
		}

		public void setAction(IntPtr aSelector)
		{
			NSControl_setAction(Raw, aSelector);
		}

		public void setStringValue(NSString aString)
		{
			NSControl_setStringValue(Raw, aString.Raw);
		}
	}
}

//***************************************************************************
//
// $Log: NSControl.cs,v $
// Revision 1.4  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.3  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
