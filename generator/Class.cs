//
// $Id: Class.cs,v 1.10 2004/09/04 04:49:30 gnorton Exp $
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
	
		public Class (uint offset, MachOFile file) {
			byte *ptr = file.GetPtr(offset);
			occlass = *(objc_class *)ptr;
			Utils.MakeBigEndian(ref occlass.isa);
			Utils.MakeBigEndian(ref occlass.super_class);
			Utils.MakeBigEndian(ref occlass.name);
			Utils.MakeBigEndian(ref occlass.version);
			Utils.MakeBigEndian(ref occlass.info);
			Utils.MakeBigEndian(ref occlass.instance_size);
			Utils.MakeBigEndian(ref occlass.ivars);
			Utils.MakeBigEndian(ref occlass.methodLists);
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
#if false
			// Process protocols
			[aClass addProtocolsFromArray:[self processProtocolList:classPtr->protocols]];
#endif
		}
	}

	public class Method {
		private string name, types;

		public Method(objc_method method, MachOFile file) {
			Utils.MakeBigEndian(ref method.name);
			Utils.MakeBigEndian(ref method.types);
			name = file.GetString(method.name);
			types = file.GetString(method.types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, types);
		}

		unsafe public static ArrayList ProcessMethods(uint methodLists,MachOFile file) {
			ArrayList ret = new ArrayList();
			if (methodLists != 0) {
				byte* methodsPtr = file.GetPtr(methodLists);
				objc_method_list ocmethodlist = *(objc_method_list *)methodsPtr;
				byte* methodPtr = methodsPtr+Marshal.SizeOf(ocmethodlist);
				Utils.MakeBigEndian(ref ocmethodlist.method_count);
				for (int i = 0; i < ocmethodlist.method_count; ++i, methodPtr += Marshal.SizeOf(typeof(objc_method))) {
					objc_method method = *(objc_method*)methodPtr;
					ret.Add(new Method(method,file));
				}
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
