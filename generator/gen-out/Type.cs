//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Type.cs,v 1.5 2004/09/20 22:31:18 gnorton Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Type : OutputElement {
		public Type(string name, string nameSpace, string apiType, System.Type glueType, OCType ocType) 
			: base(name, nameSpace) {
			this.apiType = apiType;
			this.glueType = glueType;
			this.ocType = ocType;
		}

        public bool NeedConversion {
            get {
				switch (this.ocType) {
					case OCType.bit_field:
					case OCType.@bool: 
					case OCType.@double:
					case OCType.@float: 
					case OCType.@int:
					case OCType.@long:
					case OCType.long_long:
					case OCType.@short:
					case OCType.unsigned_int:
					case OCType.unsigned_long:
					case OCType.unsigned_long_long:
					case OCType.unsigned_short:
					case OCType.@void:
					case OCType.@char:
					case OCType.unsigned_char:
					case OCType.pointer: // FIXME
					   return false;
					case OCType.array:
					case OCType.char_ptr:
					case OCType.Class:
					case OCType.id:
					case OCType.SEL:
					case OCType.structure:
					case OCType.undefined_type:
					case OCType.union:
					default: 
					   return true;
				}
            }
        }
		// -- Public Properties --
        public string GlueType { get { return ocType == OCType.@void ? "void" : glueType.FullName; } }
		public string ApiType { get { return apiType; } }
		public OCType OCType { get { return ocType; } }

		// -- Members --
		private string apiType;
		private System.Type glueType;
		private OCType ocType;
	}

	public enum OCType {
		id,
		Class,
		SEL,
		@void,
		@char,
		unsigned_char,
		@short,
		unsigned_short,
		@int,
		unsigned_int,
		@long,
		unsigned_long,
		long_long,
		unsigned_long_long,
		@float,
		@double,
		@bool,
		char_ptr,
		pointer,
		undefined_type,
		bit_field,
		array,
		union,
		structure,
	}
}

//
// $Log: Type.cs,v $
// Revision 1.5  2004/09/20 22:31:18  gnorton
// Generator v3 now generators Foundation in a compilable glueless state.
//
// Revision 1.4  2004/09/20 20:18:23  gnorton
// More refactoring; Foundation almost gens properly now.
//
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
