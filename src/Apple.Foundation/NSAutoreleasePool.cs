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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSAutoreleasePool.cs,v 1.8 2004/06/23 17:55:46 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSAutoreleasePool : NSObject {
		protected internal static IntPtr NSAutoreleasePool_class = Class.Get("NSAutoreleasePool");

		private NSAutoreleasePool() : this(NSObject__alloc0(NSAutoreleasePool_class),true) {}
		protected internal NSAutoreleasePool(IntPtr raw,bool release) : base(raw,release) {}

	}
}

//***************************************************************************
//
// $Log: NSAutoreleasePool.cs,v $
// Revision 1.8  2004/06/23 17:55:46  urs
// Make test compile with the lasted glue API name change
//
// Revision 1.7  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.6  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.5  2004/06/17 02:55:38  urs
// Some cleanup and POC of glue change
//
// Revision 1.4  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
