//
//  NSInvocation.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSInvocation.cs,v 1.2 2004/06/16 12:20:27 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSInvocation : NSObject {
		static IntPtr NSInvocation_class = NSString.NSClass("NSInvocation");
		
		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		static extern IntPtr/*(SEL)*/ NSInvocation_selector(IntPtr /*(NSInvocation*)*/ THIS);
		#endregion

		public NSInvocation() : this(NSObject__alloc(NSInvocation_class)) {}
		protected internal NSInvocation(IntPtr raw) : base(raw) {}

		public string selector() {
			return NSString.FromSEL(NSInvocation_selector(Raw)).ToString();
		}
	}
}

//***************************************************************************
//
// $Log: NSInvocation.cs,v $
// Revision 1.2  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
