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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSInvocation.cs,v 1.7 2004/06/20 02:07:25 urs Exp $
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
			IntPtr argPtr = IntPtr.Zero;
			NSInvocation_getArgument_atIndex(Raw, ref argPtr, i+2);
			// this can be compared to:
			// id argPtr = nil;
			// [invocation getArgument: &argPtr atIndex: i+2];
			// &argPtr is the pointer to the memory and is allocated on the stack
			// same is true for the argPtr in C#, the 'ref' will pass the pointer to argPtr on the stack
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
// Revision 1.7  2004/06/20 02:07:25  urs
// Clean up, move Apple.Tools into Foundation since it will need it
// No need to allocate memory for getArgumentAtIndex of NSInvocation
//
// Revision 1.6  2004/06/19 20:42:59  gnorton
// Code cleanup (remove some old methods/clean some console.writelines)
// Modify NS2Net and NSObject destructor to be able to FreeCoTaskMem that we allocate in our argument parser.
//
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
