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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSImage.cs,v 1.3 2004/06/17 13:06:27 urs Exp $
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

		public NSImage() : this(NSObject__alloc(NSImage_class),true) {}
		protected internal NSImage(IntPtr raw,bool release) : base (raw,release) {}

		public static NSImage imageNamed(NSString name) {
			return new NSImage(NSImage__imageNamed(NSImage_class, name.Raw),false);
		}
	}
}

//***************************************************************************
//
// $Log: NSImage.cs,v $
// Revision 1.3  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
