//
//  NSSize.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSSize.cs,v 1.2 2004/06/16 12:20:26 urs Exp $
//

using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public struct NSSize {
		private float _width; /* should never be negative */
		private float _height; /* should never be negative */

		public float width {
			get {
				return _width;
			}
			set {
				if(value < 0)
					throw new InvalidOperationException("width < 0");
				_width = value;
			}
		}
		public float height {
			get {
				return _height;
			}
			set {
				if(value < 0)
					throw new InvalidOperationException("height < 0");
				_height = value;
			}
		}

		public NSSize(float w, float h) { _width = w; _height = h; }
	}
}

//***************************************************************************
//
// $Log: NSSize.cs,v $
// Revision 1.2  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
