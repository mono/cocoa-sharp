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
using System.Collections;

namespace CocoaSharp {
	public class Type : OutputElement {
		static IDictionary mTypeByName = new Hashtable();

		public Type(string name, string nameSpace, string apiType, System.Type glueType, OCType ocType) 
			: base(name, nameSpace) {

			System.Diagnostics.Debug.Assert(!mTypeByName.Contains(name));
			mTypeByName[name] = this;

			this.apiType = apiType;
			this.glueType = glueType;
			this.ocType = ocType;
		}

		public static Type FromDecl(string objcDecl) {
			string name = string.Empty;
			string nameSpace = string.Empty;
			string apiType = string.Empty;
			System.Type glueType = null;
			OCType ocType = OCType.@void;
			switch (objcDecl) {
				case "BOOL": { apiType = "bool"; glueType = typeof(bool); ocType = OCType.@bool; break; }
				case "id": { apiType = "object"; glueType = typeof(IntPtr); ocType = OCType.id; break; }
				case "void": { apiType = "void"; glueType = typeof(void); ocType = OCType.@void; break; }
				case "short": { apiType = "short"; glueType = typeof(short); ocType = OCType.@short; break; }
				case "unsigned short": { apiType = "ushort"; glueType = typeof(ushort); ocType = OCType.unsigned_short; break; }
				case "int": { apiType = "int"; glueType = typeof(int); ocType = OCType.@int; break; }
				case "unsigned int": { apiType = "uint"; glueType = typeof(uint); ocType = OCType.unsigned_int; break; }
				default:
					if (objcDecl.EndsWith("*")) {
						apiType = objcDecl.Trim('*', ' ', '\t');
						Type found = (Type)mTypeByName[apiType];
						if (found != null) {
							if (found.OCType == OCType.pointer || found.OCType == OCType.id)
								return found;
							glueType = typeof(IntPtr);
							ocType = OCType.pointer;
						}
						else {
							glueType = typeof(IntPtr);
							ocType = OCType.id;
						}
					}
					else {
						ocType = OCType.@void;
					}
					break;
			}
			return new Type(name, nameSpace, apiType, glueType, ocType);
		}

		public static System.Type OCTypeToGlueType(OCType ocType) {
			switch (ocType) {
				case OCType.array: return typeof(Array);
				case OCType.bit_field: return typeof(int);
				case OCType.@bool: return typeof(bool);
				case OCType.@char: return typeof(sbyte);
				case OCType.char_ptr: return typeof(string);
				case OCType.Class: return typeof(IntPtr);
				case OCType.@double: return typeof(double);
				case OCType.@float: return typeof(float);
				case OCType.id: return typeof(IntPtr);
				case OCType.@int: return typeof(int);
				case OCType.@long: return typeof(int);
				case OCType.long_long: return typeof(long);
				case OCType.pointer: return typeof(IntPtr);
				case OCType.SEL: return typeof(IntPtr);
				case OCType.@short: return typeof(short);
				case OCType.structure: return typeof(ValueType);
				case OCType.undefined_type: return typeof(IntPtr);
				case OCType.union: return typeof(IntPtr);
				case OCType.unsigned_char: return typeof(byte);
				case OCType.unsigned_int: return typeof(uint);
				case OCType.unsigned_long: return typeof(uint);
				case OCType.unsigned_long_long: return typeof(ulong);
				case OCType.unsigned_short: return typeof(ushort);
				case OCType.@void: return typeof(void);
				default: return null;
			}
		}

		public static string OCTypeToApiType(OCType ocType) {
			switch (ocType) {
				case OCType.array: return null;
				case OCType.bit_field: return "int /*ERROR: bitfield*/";
				case OCType.@bool: return "bool";
				case OCType.@char: return "char";
				case OCType.char_ptr: return "string";
				case OCType.Class: return "Class";
				case OCType.@double: return "double";
				case OCType.@float: return "float";
				case OCType.id: return "object";
				case OCType.@int: return "int";
				case OCType.@long: return "int";
				case OCType.long_long: return "long";
				case OCType.pointer: return "IntPtr /*FIXME:)*/";
				case OCType.SEL: return "string";
				case OCType.@short: return "short";
				case OCType.structure: return "/*FIXME full name needed*/ object";
				case OCType.undefined_type: return "IntPtr";
				case OCType.union: return "IntPtr/*ERROR: Union not handled*/";
				case OCType.unsigned_char: return "byte";
				case OCType.unsigned_int: return "uint";
				case OCType.unsigned_long: return "uint";
				case OCType.unsigned_long_long: return "ulong";
				case OCType.unsigned_short: return "ushort";
				case OCType.@void: return "void";
				default: return null;
			}
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
