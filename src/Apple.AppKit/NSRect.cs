//
//  NSRect.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSRect.cs,v 1.3 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public enum NSRectEdge {
		NSMinXEdge = 0,
		NSMinYEdge = 1,
		NSMaxXEdge = 2,
		NSMaxYEdge = 3
	}

	public struct NSRect {
		public NSPoint origin;
		public NSSize size;

		public NSRect(float x, float y, float w, float h) {
			origin = new NSPoint(x, y);
			size = new NSSize(w, h);
		}
	}
}

//***************************************************************************
//
// $Log: NSRect.cs,v $
// Revision 1.3  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
