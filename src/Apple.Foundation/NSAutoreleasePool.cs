//
//  NSAutoreleasePool.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSAutoreleasePool.cs,v 1.5 2004/06/17 02:55:38 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSAutoreleasePool : NSObject {
		static IntPtr NSAutoreleasePool_class = NSString.NSClass("NSAutoreleasePool");

		private NSAutoreleasePool() : this(NSObject__alloc(NSAutoreleasePool_class)) {}
		protected internal NSAutoreleasePool(IntPtr raw) : base(raw) {}

	}
}

//***************************************************************************
//
// $Log: NSAutoreleasePool.cs,v $
// Revision 1.5  2004/06/17 02:55:38  urs
// Some cleanup and POC of glue change
//
// Revision 1.4  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
