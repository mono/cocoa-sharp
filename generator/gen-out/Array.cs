//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Array.cs,v 1.1 2004/09/09 01:16:03 urs Exp $
//

using System;

namespace CocoaSharp {
	public class Array : Type {
		public Array(Type elementType,int dim) : base(null,null,typeof(IntPtr),OCType.array) {
			this.elementType = elementType;
			this.dim = dim;
		}

		// -- Public Properties --
		public Type ElementType { get { return elementType; } }
		public int Dim { get { return dim; } }

		// -- Members --
		private Type elementType;
		private int dim;
	}
}

//
// $Log: Array.cs,v $
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
