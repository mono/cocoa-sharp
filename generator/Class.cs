//
// $Id: Class.cs,v 1.12 2004/09/05 23:50:16 urs Exp $
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
		private string name, types;

		public Method(objc_method method, MachOFile file) {
			Utils.MakeBigEndian(ref method.name);
			Utils.MakeBigEndian(ref method.types);
			name = file.GetString(method.name);
			types = file.GetString(method.types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, types);
		}
		public Method(string name,string types) {
			this.name = name;
			this.types = types;
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
