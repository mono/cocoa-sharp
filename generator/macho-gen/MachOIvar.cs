//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: MachOIvar.cs,v 1.2 2004/09/09 02:33:04 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	internal class MachOIvar {
		private string name;
		private int offset;
		private MachOType type;

		internal MachOIvar(objc_ivar ivar, MachOFile file) {
			Utils.MakeBigEndian(ref ivar.name);
			Utils.MakeBigEndian(ref ivar.type);
			Utils.MakeBigEndian(ref ivar.offset);
			name = file.GetString(ivar.name);
			string typeName = file.GetString(ivar.type);
			type = MachOType.ParseType(typeName);
			offset = ivar.offset;
			MachOFile.DebugOut(1,"\tvar: {0} type=[{3}]->[{1}] offset={2}", name, type, offset, typeName);
		}

		internal Ivar ToIvar(string nameSpace) {
			return new Ivar(name,type.ToType(nameSpace),offset);
		}
	}

	internal struct objc_ivar_list {
		internal uint ivar_count;
	};

	internal struct objc_ivar {
		internal uint name;
		internal uint type;
		internal int offset;
	};
}

//
// $Log: MachOIvar.cs,v $
// Revision 1.2  2004/09/09 02:33:04  urs
// Fix build
//
