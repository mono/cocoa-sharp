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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSInvocation.cs,v 1.4 2004/06/17 15:58:07 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSInvocation : NSObject {
		protected internal static IntPtr NSInvocation_class = Class.Get("NSInvocation");
		
		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr/*(SEL)*/ NSInvocation_selector(IntPtr /*(NSInvocation*)*/ THIS);
		#endregion

		public NSInvocation() : this(NSObject__alloc(NSInvocation_class),true) {}
		protected internal NSInvocation(IntPtr raw,bool release) : base(raw,release) {}

		public string Selector {
			get { return NSString.FromSEL(NSInvocation_selector(Raw)).ToString(); }
		}
	}
}

//***************************************************************************
//
// $Log: NSInvocation.cs,v $
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
