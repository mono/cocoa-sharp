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
using System.Text.RegularExpressions;

namespace CocoaSharp {
	public class Type : OutputElement {
		static IDictionary mTypeByName = new Hashtable();
		static IDictionary mTypeByOcType = new Hashtable();

		public static string FullName(string name, string nameSpace) {
			return nameSpace == null || nameSpace.Length == 0 ? name : nameSpace + "." + name;
		}

		public static Type RegisterType(string name, string nameSpace, System.Type newType) {
			string fullName = FullName(name, nameSpace);
			Type ret = (Type)mTypeByName[fullName];
			if (ret != null) {
				System.Diagnostics.Debug.Assert(newType.IsInstanceOfType(ret));
				return ret;
			}
			ret = (Type)newType.GetConstructor(new System.Type[]{typeof(string), typeof(string)}).Invoke(new object[]{name, nameSpace});
			return ret;
		}

		public Type(string name, string nameSpace, string apiType, System.Type glueType, OCType ocType) 
			: base(name, nameSpace) {
			switch (ocType) {
				case OCType.bit_field:
				case OCType.array:
				case OCType.pointer:
					break;
				default:
					if (name != "?") {
						System.Diagnostics.Debug.Assert(!mTypeByName.Contains(FullName(name, nameSpace)));
						System.Diagnostics.Debug.Assert(!mTypeByName.Contains(name));
//Console.WriteLine("Register type: " + FullName(name, nameSpace) + ": apiType=" + apiType + ", glueType=" + glueType.FullName + ", ocType=" + ocType);
						mTypeByName[FullName(name, nameSpace)] = this;
						mTypeByName[name] = this;
					}
					break;
			}

			IList tmp = (IList)mTypeByOcType[ocType];
			if (tmp == null) {
				tmp = new ArrayList();
				mTypeByOcType[ocType] = tmp;
			}
			tmp.Add(this);

			this.apiType = apiType;
			this.glueType = glueType;
			this.ocType = ocType;
		}

		public static Type FromOcType(OCType ocType, string name) {
			IList tmp = (IList)mTypeByOcType[ocType];
			if (tmp == null)
				return null;
			if (tmp.Count == 1)
				return (Type)tmp[0];
			if (ocType == OCType.@int)
				return FromDecl("int");
			if (name != null && name.Length > 0) {
				foreach (Type type in tmp)
					if (type.Name == name)
						return type;
				if (mTypeByName.Contains(name))
					return (Type)mTypeByName[name];
				if (ocType == OCType.structure && name.StartsWith("_") && mTypeByName.Contains(name.Substring(1)))
					return (Type)mTypeByName[name.Substring(1)];
			}
			if (ocType == OCType.id)
				return FromDecl("id");
			if (ocType == OCType.structure)
				return new Struct(name, null);
			if (ocType == OCType.union)
				return new Type(name, null, "object", typeof(IntPtr), OCType.union);
			return null;
		}

		public static Type FromDecl(string objcDecl) {
			Type found = (Type)mTypeByName[objcDecl];

			if (found != null)
				return found;

			string name = objcDecl;
			string nameSpace = string.Empty;
			string apiType = string.Empty;
			System.Type glueType = null;
			OCType ocType = OCType.@void;
			switch (objcDecl) {
				case "Boolean":
				case "BOOL": { apiType = "bool"; glueType = typeof(bool); ocType = OCType.@bool; break; }
				case "SEL": { apiType = "string"; glueType = typeof(IntPtr); ocType = OCType.SEL; break; }
				case "IMP": { apiType = "IntPtr"; glueType = typeof(IntPtr); ocType = OCType.pointer; break; }
				case "Class": { apiType = "Foundation.Class"; glueType = typeof(IntPtr); ocType = OCType.Class; break; }
				case "Protocol": { apiType = "object"; glueType = typeof(IntPtr); ocType = OCType.id; break; }
				case "id": { apiType = "object"; glueType = typeof(IntPtr); ocType = OCType.id; break; }
				case "void": { apiType = "void"; glueType = typeof(void); ocType = OCType.@void; break; }
				case "char *": { apiType = "string"; glueType = typeof(IntPtr); ocType = OCType.char_ptr; break; }

				case "uint8_t":
				case "unsigned char":
					{ apiType = "byte"; glueType = typeof(byte); ocType = OCType.unsigned_char; break; }
				case "char": { apiType = "char"; glueType = typeof(byte); ocType = OCType.@char; break; }
				case "float": { apiType = "float"; glueType = typeof(float); ocType = OCType.@float; break; }
				case "double": { apiType = "double"; glueType = typeof(double); ocType = OCType.@double; break; }
				case "OSErr":
				case "short": { apiType = "short"; glueType = typeof(short); ocType = OCType.@short; break; }
				case "unichar":
				case "unsigned short": { apiType = "ushort"; glueType = typeof(ushort); ocType = OCType.unsigned_short; break; }
				case "int32_t":
				case "SInt32":
				case "int": { apiType = "int"; glueType = typeof(int); ocType = OCType.@int; break; }
				case "UInt32":
				case "UTF32Char":
				case "unsigned int":
				case "unsigned":
					{ apiType = "uint"; glueType = typeof(uint); ocType = OCType.unsigned_int; break; }
				case "long int":
				case "long": { apiType = "int"; glueType = typeof(int); ocType = OCType.@long; break; }
				case "unsigned long": { apiType = "uint"; glueType = typeof(uint); ocType = OCType.unsigned_long; break; }
				case "int64_t":
				case "long long": { apiType = "long"; glueType = typeof(long); ocType = OCType.long_long; break; }
				case "unsigned long long": { apiType = "ulong"; glueType = typeof(ulong); ocType = OCType.unsigned_long_long; break; }

				// -- hack starts here --
				// typedef unsigned int NSDragOperation;
				case "NSDragOperation": { apiType = "uint"; glueType = typeof(uint); ocType = OCType.unsigned_int; break; }
				case "AEDesc":
				case "AEEventClass":
				case "AEEventID":
				case "AEKeyword":
				case "AEReturnID":
				case "AETransactionID":
				case "AppleEvent":
				case "CFRunLoopRef":
				case "DescType":
				case "NSAppleEventManagerSuspensionID":
				case "NSApplicationDelegateReply":
				case "NSComparisonResult":
				case "NSFontTraitMask":
				case "NSGlyph":
				case "NSInsertionPosition":
				case "NSKeyValueObservingOptions":
				case "NSModalSession":
				case "NSPointArray":
				case "NSPointPointer":
				case "NSRangePointer":
				case "NSRectArray":
				case "NSRectPointer":
				case "NSRequestUserAttentionType":
				case "NSRoundingMode":
				case "NSSizePointer":
				case "NSSocketNativeHandle":
				case "NSStringEncoding":
				case "NSTableViewDropOptions":
				case "NSTableViewDropOperation":
				case "NSTimeInterval":
				case "NSToolTipTag":
				case "NSToolbarDisplayMode":
				case "NSToolbarSizeMode":
				case "NSTrackingRectTag":
				case "NSTypesetterGlyphInfo":
				case "NSWindowDepth":
				case "NSWorkspaceLaunchOptions":
				case "_NSZone": case "NSZone":						// typedef struct _NSZone NSZone;
				{ apiType = "IntPtr"; glueType = typeof(System.ValueType); ocType = OCType.structure; break; }

				case "OSType": { apiType = "uint"; glueType = typeof(uint); ocType = OCType.unsigned_int; break; }
				case "va_list": { apiType = "IntPtr"; glueType = typeof(IntPtr); ocType = OCType.unsigned_int; break; }
				// -- hack ends here --

				default:
					if (objcDecl.EndsWith("*")) {
						apiType = objcDecl.Trim('*', ' ', '\t');
						found = FromDecl(apiType);
						return new Pointer(found);
					}
					else if (objcDecl.EndsWith("]")) {
						Regex arrayRegex = new Regex(@"^\s*(?<type>(\s*.+?\s*)+)(\s*\[(?<size>\w+|\d+)\])");
						Match m = arrayRegex.Match(objcDecl);
						string type = m.Groups["type"].Value;
						string dim = m.Groups["size"].Value;
						return new Array(Type.FromDecl(type), int.Parse(dim));
					} else if (objcDecl.IndexOf(":") > 0) {
						string[] splt = objcDecl.Split(new char[]{':'}, 2);
						return new BitField(int.Parse(splt[1]));
					} else if (objcDecl.StartsWith("struct ")) {
						return new Struct(objcDecl.Substring("struct ".Length), null);
					} else if (objcDecl.StartsWith("union ")) {
						return new Type(objcDecl.Substring("struct ".Length), null, "object", typeof(IntPtr), OCType.union);
					} else {
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

		public static string OCTypeToDeclType(OCType ocType) {
			switch (ocType) {
				case OCType.@bool: return "BOOL";
				case OCType.@char: return "char";
				case OCType.char_ptr: return "char *";
				case OCType.Class: return "Class";
				case OCType.@double: return "double";
				case OCType.@float: return "float";
				case OCType.@int: return "int";
				case OCType.@long: return "long";
				case OCType.long_long: return "long long";
				case OCType.SEL: return "SEL";
				case OCType.@short: return "short";
				case OCType.unsigned_char: return "unsigned char";
				case OCType.unsigned_int: return "unsigned";
				case OCType.unsigned_long: return "unsigned long";
				case OCType.unsigned_long_long: return "unsigned long long";
				case OCType.unsigned_short: return "unsigned short";
				case OCType.@void: return "void";

				case OCType.id:
				case OCType.array:
				case OCType.bit_field:
				case OCType.pointer:
				case OCType.structure:
				case OCType.undefined_type:
				case OCType.union:
				default:
					return null;
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
