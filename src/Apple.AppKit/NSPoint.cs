//
//  NSPoint.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSPoint.cs,v 1.2 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public struct NSPoint {
		public float x;
		public float y;

		public NSPoint(float x, float y) { this.x = x; this.y = y; }
	}
}

//***************************************************************************
//
// $Log: NSPoint.cs,v $
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
