//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Pointer.cs,v 1.2 2004/09/09 03:32:22 urs Exp $
//

using System;

namespace CocoaSharp {
	public class Pointer : Type {
		public Pointer(Type reference) : base(null,null,typeof(IntPtr),OCType.pointer) {
			this.reference = reference;
		}

		// -- Public Properties --
		public Type Reference { get { return reference; } }

		// -- Members --
		private Type reference;
	}
}

//
// $Log: Pointer.cs,v $
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
