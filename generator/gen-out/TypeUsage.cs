//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: TypeUsage.cs,v 1.1 2004/09/09 01:16:03 urs Exp $
//

using System;

namespace CocoaSharp {
	public class TypeUsage {
		public TypeUsage(Type type, TypeModifiers typeModifiers) {
			this.type = type;
			this.typeModifiers = typeModifiers;
		}

		// -- Public Properties --
		public Type Type { get { return type; } }
		public TypeModifiers TypeModifiers { get { return typeModifiers; } }

		// -- Members --
		private Type type;
		private TypeModifiers typeModifiers;
	}

	[Flags]
	public enum TypeModifiers {
		@const 	= 1 << 1,
		@in 	= 1 << 2,
		inout 	= 1 << 3,
		@out 	= 1 << 4,
		bycopy 	= 1 << 5,
		oneway 	= 1 << 6,
	}
}

//
// $Log: TypeUsage.cs,v $
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
