//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: MachOMethod.cs,v 1.3 2004/09/09 03:32:22 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	internal class MachOMethod {
		private string name;
		private MachOType[] types;

		internal MachOMethod(objc_method method, MachOFile file) {
			Utils.MakeBigEndian(ref method.name);
			Utils.MakeBigEndian(ref method.types);
			name = file.GetString(method.name);
			string typesStr = file.GetString(method.types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, typesStr);
			types = MachOType.ParseTypes(typesStr);
		}

		internal MachOMethod(string name,string types) {
			this.name = name;
			this.types = MachOType.ParseTypes(types);
			MachOFile.DebugOut(1,"\tmethod: {0} types={1}", name, types);
		}

		internal ICollection ToParameters(string nameSpace) {
			ArrayList ret = new ArrayList();
			for (int i = 2; i < types.Length; ++i)
				ret.Add(new ParameterInfo("p" + (i-2),types[i].ToTypeUsage(nameSpace)));
			return ret;
		}

		internal Method ToMethod(string nameSpace) {
			return new Method(name,this.types[0].ToTypeUsage(nameSpace),ToParameters(nameSpace));
		}

		static internal ICollection ToMethods(string nameSpace,ICollection methods) {
			ArrayList ret = new ArrayList();
			foreach (MachOMethod method in methods)
				ret.Add(method.ToMethod(nameSpace));
			return ret;
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
				ret.Add(new MachOMethod(method,file));
			}
			return ret;
		}
	}
}

//
// $Log: MachOMethod.cs,v $
// Revision 1.3  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.2  2004/09/09 02:33:04  urs
// Fix build
//
