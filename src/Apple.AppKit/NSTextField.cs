//
//  NSTextField.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSTextField.cs,v 1.7 2004/06/17 15:58:07 urs Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSTextField : NSControl {
		protected internal static IntPtr NSTextField_class = Class.Get("NSTextField");

		[DllImport("AppKitGlue")]
		protected internal static extern void NSTextField_setEditable(IntPtr THIS, bool flag);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSTextField_setBezeled(IntPtr THIS, bool flag);

		public NSTextField() : this(NSObject__alloc(NSTextField_class),true) {}
		protected NSTextField(IntPtr raw,bool release) : base(raw,release) {}

		public bool Editable {
			set { NSTextField_setEditable(Raw, value); }
		}
		
		public bool Bezeled {
			set { NSTextField_setBezeled(Raw, value); }
		}
	}
}

//***************************************************************************
//
// $Log: NSTextField.cs,v $
// Revision 1.7  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.6  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.5  2004/06/16 12:20:26  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
