//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: MachOIvar.cs,v 1.1 2004/09/09 01:18:47 urs Exp $
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
	}

	internal struct objc_ivar_list {
		internal int ivar_count;
	};

	internal struct objc_ivar {
		internal uint name;
		internal uint type;
		internal int offset;
	};

}
