//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id$
//

using System;

namespace CocoaSharp {
	public class TypeUsage {
		public TypeUsage(Type type, TypeModifiers typeModifiers) {
			this.type = type;
			this.typeModifiers = typeModifiers;
		}

		public static TypeUsage FromDecl(string objcDecl) {
			return new TypeUsage(Type.FromDecl(objcDecl), TypeModifiers.none);
		}

		// -- Public Properties --
		public Type Type { get { return type; } }
		public TypeModifiers TypeModifiers { get { return typeModifiers; } }
		public string GlueType { get { return Type.GlueType; } }
		public string ApiType { get { return Type.ApiType; } }

		// -- Members --
		private Type type;
		private TypeModifiers typeModifiers;
	}

	[Flags]
	public enum TypeModifiers {
		none = 0,
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
// Revision 1.3  2004/09/11 00:41:22  urs
// Move Output to gen-out
//
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
