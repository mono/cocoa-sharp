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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSInvocation.cs,v 1.5 2004/06/19 17:19:27 gnorton Exp $
//

using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSInvocation : NSObject {
		protected internal static IntPtr NSInvocation_class = Class.Get("NSInvocation");
		
		#region -- Glue --
		[DllImport("Glue")]
		protected internal static extern int GetInvocationArgumentSize(IntPtr /*(NSInvocation*)*/ THIS, int index);
		#endregion

		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr/*(SEL)*/ NSInvocation_selector(IntPtr /*(NSInvocation*)*/ THIS);
		[DllImport("FoundationGlue")]
		protected internal static extern void NSInvocation_getArgument_atIndex(IntPtr /*(NSInvocation*)*/ THIS, ref IntPtr buffer, int index);
		#endregion

		public NSInvocation() : this(NSObject__alloc(NSInvocation_class),true) {}
		protected internal NSInvocation(IntPtr raw,bool release) : base(raw,release) {}

		public object getArgument(int i) {
			Console.WriteLine("Getting argument: {0} of size {1}", i, GetInvocationArgumentSize(Raw, i+2));
			// This is static for now till we reflect sizes
			IntPtr argPtr = Marshal.AllocCoTaskMem(GetInvocationArgumentSize(Raw, i+2));
			NSInvocation_getArgument_atIndex(Raw, ref argPtr, i+2);
			return NS2Net(argPtr);
		}

		public string Selector {
			get { return NSString.FromSEL(NSInvocation_selector(Raw)).ToString(); }
		}
	}
}

//***************************************************************************
//
// $Log: NSInvocation.cs,v $
// Revision 1.5  2004/06/19 17:19:27  gnorton
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
