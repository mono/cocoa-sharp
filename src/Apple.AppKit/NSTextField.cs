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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.AppKit/Attic/NSTextField.cs,v 1.9 2004/06/18 20:13:00 gnorton Exp $
//

using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSTextField : NSControl {
		protected internal static IntPtr NSTextField_class = Class.Get("NSTextField");
		private NSObject mDelegate;

		[DllImport("AppKitGlue")]
		protected internal static extern void NSTextField_setEditable(IntPtr THIS, bool flag);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSTextField_setBezeled(IntPtr THIS, bool flag);
		[DllImport("AppKitGlue")]
		protected internal static extern void NSTextField_setDelegate(IntPtr THIS, IntPtr aDelegate);

		public NSTextField() : this(NSObject__alloc(NSTextField_class),true) {}
		protected NSTextField(IntPtr raw,bool release) : base(raw,release) {}
		public NSTextField(NSRect rect) {
			SetRaw(NSObject__alloc(NSTextField_class), true);
			initWithFrame(rect);
		}

		public bool Editable {
			set { NSTextField_setEditable(Raw, value); }
		}
		
		public bool Bezeled {
			set { NSTextField_setBezeled(Raw, value); }
		}
		
		public NSObject Delegate {
			get { return mDelegate; }
			set { mDelegate = value; NSTextField_setDelegate(Raw, value.Raw); }
		}
	}
}

//***************************************************************************
//
// $Log: NSTextField.cs,v $
// Revision 1.9  2004/06/18 20:13:00  gnorton
// Support for multi-argument method signatures/calling in .Net
//
// Revision 1.8  2004/06/17 17:41:20  gnorton
// API modification.
//
// Allow our inits to be called with crafted constructors.
//
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
