//
// $Id: Class.cs,v 1.13 2004/09/07 20:07:40 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Class {
		
		private objc_class occlass;
		private string superClass, name;
		private bool isClass;
		private ArrayList ivars = new ArrayList();
		private ArrayList methods, classMethods;
		private IDictionary protocols = new Hashtable();
	
		public Class (byte *ptr, MachOFile file) {
			occlass = *(objc_class *)ptr;
			Utils.MakeBigEndian(ref occlass.isa);
			Utils.MakeBigEndian(ref occlass.super_class);
			Utils.MakeBigEndian(ref occlass.name);
			Utils.MakeBigEndian(ref occlass.version);
			Utils.MakeBigEndian(ref occlass.info);
			Utils.MakeBigEndian(ref occlass.instance_size);
			Utils.MakeBigEndian(ref occlass.ivars);
			Utils.MakeBigEndian(ref occlass.methodLists);
			Utils.MakeBigEndian(ref occlass.protocols);
			superClass = file.GetString(occlass.super_class);
			name = file.GetString(occlass.name);
			isClass = (occlass.info & 1) != 0;
			MachOFile.DebugOut(0,"Class: {0} : {1} iSize={2} info={3,8:x} isClass={4}",name,superClass,occlass.instance_size,occlass.info,isClass);

			// Process ivars
			if (isClass && occlass.ivars != 0) {
				byte* ivarsPtr = file.GetPtr(occlass.ivars);
				objc_ivar_list ocivars = *(objc_ivar_list*)ivarsPtr;
				Utils.MakeBigEndian(ref ocivars.ivar_count);
				byte* ivarPtr = ivarsPtr + Marshal.SizeOf(typeof(objc_ivar_list));

				for (int i = 0; i < ocivars.ivar_count; ++i, ivarPtr += Marshal.SizeOf(typeof(objc_ivar))) {
					objc_ivar ivar = *(objc_ivar*)ivarPtr;
					ivars.Add(new Ivar(ivar,file));
				}
			}

			methods = Method.ProcessMethods(occlass.methodLists,file);

			if (isClass) {
				// Process meta class
				objc_class metaClass = *(objc_class *)file.GetPtr(occlass.isa);
				Utils.MakeBigEndian(ref metaClass.methodLists);
				classMethods = Method.ProcessMethods(metaClass.methodLists,file);
			}

			AddProtocolsFromArray(ProcessProtocolList(occlass.protocols,file));
		}

		void AddProtocolsFromArray(IList protocols) {
			foreach (Protocol p in protocols)
				AddProtocol(p);
		}

		void AddProtocol(Protocol p) {
			if (protocols.Contains(p.Name))
				protocols[p.Name] = p;
		}

		static ArrayList ProcessProtocolList(uint protocolListAddr,MachOFile file) {
			ArrayList protocols = new ArrayList();
			if (protocolListAddr == 0)
				return protocols;

			byte *ptr = file.GetPtr(protocolListAddr);
			objc_protocol_list protocolList = *(objc_protocol_list*)ptr;
			Utils.MakeBigEndian(ref protocolList.count);
			uint *protocolPtrs = (uint*)(ptr + Marshal.SizeOf(typeof(objc_protocol_list)));
			for (int index = 0; index < protocolList.count; index++, protocolPtrs++) {
				Utils.MakeBigEndian(ref *protocolPtrs);
				ptr = file.GetPtr(*protocolPtrs);
				if (ptr != null)
					protocols.Add(ProcessProtocol(ptr,file));
			}

			return protocols;
		}

		static IDictionary mProtocolsByName = new Hashtable();
		static Protocol ProcessProtocol(byte *ptr,MachOFile file) {
			objc_protocol protocolPtr = *(objc_protocol*)ptr;
			Utils.MakeBigEndian(ref protocolPtr.isa);
			Utils.MakeBigEndian(ref protocolPtr.protocol_name);
			Utils.MakeBigEndian(ref protocolPtr.protocol_list);
			Utils.MakeBigEndian(ref protocolPtr.instance_methods);
			Utils.MakeBigEndian(ref protocolPtr.class_methods);
			string name = file.GetString(protocolPtr.protocol_name);
			ArrayList protocols = ProcessProtocolList(protocolPtr.protocol_list,file);

			Protocol protocol = (Protocol)mProtocolsByName[name];
			if (protocol == null) {
				protocol = new Protocol(name);
				mProtocolsByName[name] = protocol;
			}

			protocol.AddProtocolsFromArray(protocols);
			if (protocol.instanceMethods.Count == 0)
				protocol.instanceMethods = ProcessProtocolMethods(protocolPtr.instance_methods,file);

			if (protocol.classMethods.Count == 0)
				protocol.classMethods = ProcessProtocolMethods(protocolPtr.class_methods,file);

			// TODO (2003-12-09): Maybe we should add any missing methods.  But then we'd lose the original order.
			return protocol;
		}

		static ArrayList ProcessProtocolMethods(uint methodsAddr, MachOFile file) {
			ArrayList methods = new ArrayList();
			if (methodsAddr == 0)
				return methods;
			byte *ptr = file.GetPtr(methodsAddr);
			objc_protocol_method_list methodsPtr = *(objc_protocol_method_list*)ptr;
			ptr += Marshal.SizeOf(typeof(objc_protocol_method_list));

			Utils.MakeBigEndian(ref methodsPtr.method_count);
			for (int index = 0; index < methodsPtr.method_count; index++, ptr += Marshal.SizeOf(typeof(objc_protocol_method))) {
				objc_protocol_method methodPtr = *(objc_protocol_method*)ptr;
				Utils.MakeBigEndian(ref methodPtr.name);
				Utils.MakeBigEndian(ref methodPtr.types);
				Method method = new Method(file.GetString(methodPtr.name),
					file.GetString(methodPtr.types)
				);
				methods.Insert(0,method);
			}

			return methods;
		}
	}

	public class Protocol {
		public string Name;
		public ArrayList instanceMethods = new ArrayList();
		public ArrayList classMethods = new ArrayList();
		private IDictionary protocols = new Hashtable();

		public Protocol(string name) { Name = name; }

		public void AddProtocolsFromArray(IList protocols) {
			foreach (Protocol p in protocols)
				AddProtocol(p);
		}

		public void AddProtocol(Protocol p) {
			if (protocols.Contains(p.Name))
				protocols[p.Name] = p;
		}
	}

	public struct objc_protocol_list {
		public uint next;
		public uint count;
	}

	public struct objc_protocol {
		public uint isa;
		public uint protocol_name;
		public uint protocol_list;
		public uint instance_methods;
		public uint class_methods;
	};

	public struct objc_protocol_method_list {
		public uint method_count;
		// Followed by methods
	};

	public struct objc_protocol_method {
		public uint name;
		public uint types;
	};

	public class Method {
		private string name;
		private OCType[] types;

		public Method(objc_method method, MachOFile file) {
			Utils.MakeBigEndian(ref method.name);
			Utils.MakeBigEndian(ref method.types);
			name = file.GetString(method.name);
			string typesStr = file.GetString(method.types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, typesStr);
			types = OCType.ParseTypes(typesStr);
		}
		public Method(string name,string types) {
			this.name = name;
			this.types = OCType.ParseTypes(types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, types);
		}

		unsafe public static ArrayList ProcessMethods(uint methodLists,MachOFile file) {
			ArrayList ret = new ArrayList();
			if (methodLists == 0) 
				return ret;
			byte* methodsPtr = file.GetPtr(methodLists);
			if (methodsPtr == null)
				return ret;
			objc_method_list ocmethodlist = *(objc_method_list *)methodsPtr;
			byte* methodPtr = methodsPtr+Marshal.SizeOf(ocmethodlist);
			Utils.MakeBigEndian(ref ocmethodlist.method_count);
			for (int i = 0; i < ocmethodlist.method_count; ++i, methodPtr += Marshal.SizeOf(typeof(objc_method))) {
				objc_method method = *(objc_method*)methodPtr;
				ret.Add(new Method(method,file));
			}
			return ret;
		}
	}

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

	public enum OCTypeKind {
		id,
		Class,
		SEL,
		void_,
		char_,
		unsigned_char,
		short_,
		unsigned_short,
		int_,
		unsigned_int,
		long_,
		unsigned_long,
		long_long,
		unsigned_long_long,
		float_,
		double_,
		bool_,
		char_ptr,
		pointer,
		undefined_type,
		bit_field,
		array,
		union,
		structure,
	}
	[Flags]
	public enum OCTypeModifiers {
		const_ 	= 1 << 1,
		in_ 	= 1 << 2,
		inout 	= 1 << 3,
		out_ 	= 1 << 4,
		bycopy 	= 1 << 5,
		oneway 	= 1 << 6,
	}

	public class OCType {
		public OCTypeKind kind = OCTypeKind.id;
		public OCTypeModifiers modifiers;
		public int offset, size, arrayDim, bitCount;
		public string name, typeStr;
		public OCType reference;
		public OCType[] fields;

		public bool IsPrimitive {
			get { return kind != OCTypeKind.structure && kind != OCTypeKind.array && kind != OCTypeKind.pointer && kind != OCTypeKind.union; }
		}
		public override string ToString() {
			string detail = reference != null ? reference.ToString() : "";
			switch (kind) {
			case OCTypeKind.structure:
			case OCTypeKind.union:
				foreach (OCType field in fields) {
					if (detail != "") detail += ",";
					detail += field.ToString();
				}
				break;
			case OCTypeKind.array:
				detail = detail + "[" + arrayDim + "]";
				break;
			case OCTypeKind.bit_field:
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
		
		static public OCType[] ParseTypes(string types) {
			ArrayList ret = new ArrayList();
#if DEBUG
			bool hasNonPrimitive = false;
#endif
			int read = 0;
			string tmp = types;
			do {
				tmp = tmp.Substring(read);
				OCType t = ParseType(tmp,true,out read);
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
			return (OCType[])ret.ToArray(typeof(OCType));
		}

		static int ParseInt(string type,ref int read) {
			string intStr = "";
			while (read < type.Length && char.IsDigit(type[read]))
				intStr += type[read++];

			if (intStr != "")
				return int.Parse(intStr);
			return 0;
		}

		static public OCType ParseType(string type) {
			int tmpRead;
			return ParseType(type,false,out tmpRead);
		}
		
		static OCType ParseSubType(string type, ref int read) {
			int tmpRead;
			OCType ret = ParseType(type.Substring(read),false,out tmpRead);
			read += tmpRead;
			return ret;
		}

		static public OCType ParseType(string type,bool readOff,out int read) {
			OCType ret = new OCType();
			read = 0;
			MachOFile.DebugOut(1,"- Parsing '{0}'",type);
			bool cont;
			do {
				cont = false;
				switch (type[read]) {
				case '@': // id
					++read;
					ret.kind = OCTypeKind.id;
					cont = ret.name == null && read < type.Length && type[read] == '"';
					break;
				case '#': // Class
					++read;
					ret.kind = OCTypeKind.Class;
					break;
				case ':': // SEL
					++read;
					ret.kind = OCTypeKind.SEL;
					break;
				case 'v': // void
					++read;
					ret.kind = OCTypeKind.void_;
					break;
				case 'c': // char
					++read;
					ret.kind = OCTypeKind.char_;
					break;
				case 'C': // unsigned char
					++read;
					ret.kind = OCTypeKind.unsigned_char;
					break;
				case 's': // short
					++read;
					ret.kind = OCTypeKind.short_;
					break;
				case 'S': // unsigned short
					++read;
					ret.kind = OCTypeKind.unsigned_short;
					break;
				case 'i': // int
					++read;
					ret.kind = OCTypeKind.int_;
					break;
				case 'I': // unsigned int
					++read;
					ret.kind = OCTypeKind.unsigned_int;
					break;
				case 'l': // long
					++read;
					ret.kind = OCTypeKind.long_;
					break;
				case 'L': // unsigned long
					++read;
					ret.kind = OCTypeKind.unsigned_long;
					break;
				case 'q': // long long
					++read;
					ret.kind = OCTypeKind.long_long;
					break;
				case 'Q': // unsigned long long
					++read;
					ret.kind = OCTypeKind.unsigned_long_long;
					break;
				case 'f': // float
					++read;
					ret.kind = OCTypeKind.float_;
					break;
				case 'd': // double
					++read;
					ret.kind = OCTypeKind.double_;
					break;
				case 'B': // C++ bool or a C99 _Bool
 					++read;
					ret.kind = OCTypeKind.bool_;
					break;
				case '*': // char *
					++read;
					ret.kind = OCTypeKind.char_ptr;
					break;
				case '^': // any pointer
					++read;
					ret.kind = OCTypeKind.pointer;
					ret.reference = ParseSubType(type,ref read);
					break;
				case '?': // an undefined type
					++read;
					ret.kind = OCTypeKind.undefined_type;
					break;
				case 'b': // a bitfield
					++read;
					ret.kind = OCTypeKind.bit_field;
					ret.bitCount = ParseInt(type, ref read);
					break;
				case '[': // begin an array
					++read;
					ret.kind = OCTypeKind.array;
					ret.arrayDim = ParseInt(type,ref read);
					ret.reference = ParseSubType(type,ref read);
					if (type[read] != ']')
						MachOFile.DebugOut(0,"ERROR: array does not end with ']' ({0}) #{1}",type,read);
					else
						++read;
					break;
				case '"': // begin a name
					cont = read == 0;
					++read;
					{
						int nameOff = type.IndexOf('"',read);
						ret.name = type.Substring(read,nameOff-read);
						read = nameOff+1;
					}
					break;
				case '(': // begin a union
				case '{': // begin a structure
					ret.kind = type[read] == '(' ? OCTypeKind.union : OCTypeKind.structure;
					++read;
					{
						char close = ret.kind == OCTypeKind.union ? ')' : '}';
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
	
							ret.fields = (OCType[])fields.ToArray(typeof(OCType));
						}
						else
							MachOFile.DebugOut(0,"ERROR: structure/union does not end with '{1}' ({0})",type.Substring(read),close);

						OCType existing = (OCType)MachOFile.Types[ret.name];
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
					ret.modifiers |= OCTypeModifiers.const_;
					break;
				case 'n': // in
					cont = true;
					++read;
					ret.modifiers |= OCTypeModifiers.in_;
					break;
				case 'N': // inout
					cont = true;
					++read;
					ret.modifiers |= OCTypeModifiers.inout;
					break;
				case 'o': // out
					cont = true;
					++read;
					ret.modifiers |= OCTypeModifiers.out_;
					break;
				case 'O': // bycopy
					cont = true;
					++read;
					ret.modifiers |= OCTypeModifiers.bycopy;
					break;
				case 'V': // oneway
					cont = true;
					++read;
					ret.modifiers |= OCTypeModifiers.oneway;
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

	public struct objc_class {
		public uint isa;
		public uint super_class;
		public uint name;
		public uint version;
		public uint info;
		public uint instance_size;
		public uint ivars;
		public uint methodLists;
		public uint cache;
		public uint protocols;
	}

	public struct objc_method_list {
		public uint obsolete;
		public uint method_count;
	}

	public struct objc_method {
		public uint name;
		public uint types;
		public uint imp;
	}
}
