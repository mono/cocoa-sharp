//
//  NSNotification.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSNotification.cs,v 1.1 2004/06/19 17:19:27 gnorton Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSNotification : NSObject {
		protected internal static IntPtr NSNotification_class = Class.Get("NSNotification");
		
		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr/*(SEL)*/ NSNotification_object(IntPtr /*(NSNotification*)*/ THIS);
		#endregion

		public NSNotification() : this(NSObject__alloc(NSNotification_class),true) {}
		protected internal NSNotification(IntPtr raw,bool release) : base(raw,release) {}

		public Object Object {
			get { return NS2Net(NSNotification_object(Raw)); }
		}
	}
}

//***************************************************************************
//
// $Log: NSNotification.cs,v $
// Revision 1.1  2004/06/19 17:19:27  gnorton
// Broken API fixes.
// Delegates and methods with multi-argument support working.
// Argument parsing and casting working for all our known classes.
//
// Revision 1.4  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.3  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.2  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
