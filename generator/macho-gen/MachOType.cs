//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: MachOType.cs,v 1.5 2004/09/20 20:18:23 gnorton Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	// Meaning				Code
	//--------------------------
	// id					`@'
	// Class				`#'
	// SEL					`:'
	// void					`v'
	// char					`c'
	// unsigned char		`C'
	// short				`s'
	// unsigned short		`S'
	// int					`i'
	// unsigned int			`I'
	// long					`l'
	// unsigned long		`L'
	// long long			`q'
	// unsigned long long	`Q'
	// float				`f'
	// double				`d'
	// C++ bool or a C99 _Bool `B'
	// char *				`*'
	// any pointer			`^'
	// an undefined type	`?'
	// a bitfield			`b'
	// begin an array		`['
	// end an array			`]'
	// begin a union		`('
	// end a union			`)'
	// begin a structure	`{'
	// end a structure		`}'

	// The same codes are used for methods declared in a protocol, but with these additions for type modifiers:
	// const				`r'
	// in					`n'
	// inout				`N'
	// out					`o'
	// bycopy				`O'
	// oneway				`V'

	public class MachOType {
		public OCType kind = OCType.id;
		public TypeModifiers modifiers;
		public int offset, arrayDim, bitCount;
		public string name;
		public MachOType reference;
		public MachOType[] fields;

		public bool IsPrimitive {
			get { return kind != OCType.structure && kind != OCType.array && kind != OCType.pointer && kind != OCType.union; }
		}

		public override string ToString() {
			string detail = reference != null ? reference.ToString() : "";
			switch (kind) {
				case OCType.structure:
				case OCType.union:
					foreach (MachOType field in fields) {
						if (detail != "") detail += ",";
						detail += field.ToString();
					}
					break;
				case OCType.array:
					detail = detail + "[" + arrayDim + "]";
					break;
				case OCType.bit_field:
					detail = detail + ":" + bitCount;
					break;
			}
			string ret = kind.ToString();
			if (name != null)
				ret = name + "=" + ret;
			if (modifiers != 0)
				ret += " " + modifiers;
			if (detail != "")
				ret += " (" + detail + ")";
			if (offset != 0)
				ret += " offset=" + offset;
			return ret;
		}

		internal System.Type GlueType {
			get {
				switch (this.kind) {
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
		}
		internal string ApiType {
			get {
				switch (this.kind) {
					case OCType.array: return this.reference.ApiType + "[]";
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
		}

		internal TypeUsage ToTypeUsage(string nameSpace) {
			return new TypeUsage(ToType(nameSpace),modifiers);
		}
		internal Type ToType(string nameSpace) {
			return new Type(name,nameSpace,ApiType,GlueType,kind);
		}

		static public MachOType[] ParseTypes(string types) {
			ArrayList ret = new ArrayList();
#if DEBUG
			bool hasNonPrimitive = false;
#endif
			int read = 0;
			string tmp = types;
			do {
				tmp = tmp.Substring(read);
				MachOType t = ParseType(tmp,true,out read);
#if DEBUG
				if (!hasNonPrimitive)
					hasNonPrimitive = !t.IsPrimitive;
#endif
				ret.Add(t);
			} while (read < tmp.Length);
#if DEBUG
			if (hasNonPrimitive) {
				MachOFile.DebugOut(0,"Parsing '{0}'",types);
				MachOFile.DebugOut(0,"   ret={0}",ret[0]);
				for (int i = 3; i < ret.Count; ++i)
					MachOFile.DebugOut(0,"   #{0}={1}",i-3,ret[i]);
			}
#endif
			return (MachOType[])ret.ToArray(typeof(MachOType));
		}

		static int ParseInt(string type,ref int read) {
			string intStr = "";
			while (read < type.Length && char.IsDigit(type[read]))
				intStr += type[read++];

			if (intStr != "")
				return int.Parse(intStr);
			return 0;
		}

		static public MachOType ParseType(string type) {
			int tmpRead;
			return ParseType(type,false,out tmpRead);
		}
		
		static MachOType ParseSubType(string type, ref int read) {
			int tmpRead;
			MachOType ret = ParseType(type.Substring(read),false,out tmpRead);
			read += tmpRead;
			return ret;
		}

		static public MachOType ParseType(string type,bool readOff,out int read) {
			MachOType ret = new MachOType();
			read = 0;
			MachOFile.DebugOut(1,"- Parsing '{0}'",type);
			bool cont;
			do {
				cont = false;
				switch (type[read]) {
					case '@': // id
						++read;
						ret.kind = OCType.id;
						cont = ret.name == null && read < type.Length && type[read] == '"';
						break;
					case '#': // Class
						++read;
						ret.kind = OCType.Class;
						break;
					case ':': // SEL
						++read;
						ret.kind = OCType.SEL;
						break;
					case 'v': // void
						++read;
						ret.kind = OCType.@void;
						break;
					case 'c': // char
						++read;
						ret.kind = OCType.@char;
						break;
					case 'C': // unsigned char
						++read;
						ret.kind = OCType.unsigned_char;
						break;
					case 's': // short
						++read;
						ret.kind = OCType.@short;
						break;
					case 'S': // unsigned short
						++read;
						ret.kind = OCType.unsigned_short;
						break;
					case 'i': // int
						++read;
						ret.kind = OCType.@int;
						break;
					case 'I': // unsigned int
						++read;
						ret.kind = OCType.unsigned_int;
						break;
					case 'l': // long
						++read;
						ret.kind = OCType.@long;
						break;
					case 'L': // unsigned long
						++read;
						ret.kind = OCType.unsigned_long;
						break;
					case 'q': // long long
						++read;
						ret.kind = OCType.long_long;
						break;
					case 'Q': // unsigned long long
						++read;
						ret.kind = OCType.unsigned_long_long;
						break;
					case 'f': // float
						++read;
						ret.kind = OCType.@float;
						break;
					case 'd': // double
						++read;
						ret.kind = OCType.@double;
						break;
					case 'B': // C++ bool or a C99 _Bool
						++read;
						ret.kind = OCType.@bool;
						break;
					case '*': // char *
						++read;
						ret.kind = OCType.char_ptr;
						break;
					case '^': // any pointer
						++read;
						ret.kind = OCType.pointer;
						ret.reference = ParseSubType(type,ref read);
						break;
					case '?': // an undefined type
						++read;
						ret.kind = OCType.undefined_type;
						break;
					case 'b': // a bitfield
						++read;
						ret.kind = OCType.bit_field;
						ret.bitCount = ParseInt(type, ref read);
						break;
					case '[': // begin an array
						++read;
						ret.kind = OCType.array;
						ret.arrayDim = ParseInt(type,ref read);
						ret.reference = ParseSubType(type,ref read);
						if (type[read] != ']')
							MachOFile.DebugOut(0,"ERROR: array does not end with ']' ({0}) #{1}",type,read);
						else
							++read;
						break;
					case '"': // begin a name
						cont = read == 0;
						++read; {
						int nameOff = type.IndexOf('"',read);
						ret.name = type.Substring(read,nameOff-read);
						read = nameOff+1;
					}
						break;
					case '(': // begin a union
					case '{': // begin a structure
						ret.kind = type[read] == '(' ? OCType.union : OCType.structure;
						++read; {
						char close = ret.kind == OCType.union ? ')' : '}';
						int nameOff = type.IndexOfAny(new char[]{'=',close},read);
						if (nameOff >= 0) {
							ArrayList fields = new ArrayList();
							ret.name = type.Substring(read,nameOff-read);
							read = nameOff;
							if (type[read] == '=')
								++read;
							while (type[read] != close)
								fields.Add(ParseSubType(type,ref read));
							if (type[read] == close)
								++read;
							else
								MachOFile.DebugOut(0,"ERROR: structure/union does not end with '{1}' ({0})",type.Substring(read),close);

							ret.fields = (MachOType[])fields.ToArray(typeof(MachOType));
						}
						else
							MachOFile.DebugOut(0,"ERROR: structure/union does not end with '{1}' ({0})",type.Substring(read),close);

						MachOType existing = (MachOType)MachOFile.Types[ret.name];
						if (existing == null)
							MachOFile.Types[ret.name] = ret;
						else if (existing.fields.Length == 0 && ret.fields.Length != 0)
							MachOFile.Types[ret.name] = ret;
						else if (existing.fields.Length > 0 && existing.fields[0].name == null && ret.fields.Length > 0 && ret.fields[0].name != null)
							MachOFile.Types[ret.name] = ret;
					}
						break;
					case 'r': // const
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.@const;
						break;
					case 'n': // in
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.@in;
						break;
					case 'N': // inout
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.inout;
						break;
					case 'o': // out
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.@out;
						break;
					case 'O': // bycopy
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.bycopy;
						break;
					case 'V': // oneway
						cont = true;
						++read;
						ret.modifiers |= TypeModifiers.oneway;
						break;
					default:
						if (ret.modifiers == 0) {
							MachOFile.DebugOut(0,"ERROR: unknown type ({0}) #{1}",type,read);
							read = type.Length;
							return null;
						}
						break;
				}
			} while (cont);
			if (readOff && read < type.Length)
				ret.offset = ParseInt(type,ref read);

			return ret;
		}
	}
}

//
// $Log: MachOType.cs,v $
// Revision 1.5  2004/09/20 20:18:23  gnorton
// More refactoring; Foundation almost gens properly now.
//
// Revision 1.4  2004/09/11 00:41:22  urs
// Move Output to gen-out
//
// Revision 1.3  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.2  2004/09/09 02:33:04  urs
// Fix build
//
