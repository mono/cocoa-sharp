//
//  NSImage.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSImage.cs,v 1.2 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSImage : NSObject {
		static IntPtr NSImage_class = Apple.Foundation.NSString.NSClass("NSImage");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSImage__imageNamed(IntPtr CLASS, IntPtr name);

		public NSImage() : this(NSObject__alloc(NSImage_class)) {}
		protected internal NSImage(IntPtr raw) : base (raw) {}

		public static NSImage imageNamed(NSString name) {
			return new NSImage(NSImage__imageNamed(NSImage_class, name.Raw));
		}
	}
}

//***************************************************************************
//
// $Log: NSImage.cs,v $
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
