//
// $Id: Ivar.cs,v 1.2 2004/09/07 20:07:40 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	public class Ivar {
		private string name;
		private int offset;
		private OCType type;

		public Ivar(objc_ivar ivar, MachOFile file) {
			Utils.MakeBigEndian(ref ivar.name);
			Utils.MakeBigEndian(ref ivar.type);
			Utils.MakeBigEndian(ref ivar.offset);
			name = file.GetString(ivar.name);
			string typeName = file.GetString(ivar.type);
			type = OCType.ParseType(typeName);
			offset = ivar.offset;
			MachOFile.DebugOut(1,"\tvar: {0} type=[{3}]->[{1}] offset={2}", name, type, offset, typeName);
		}
	}

	public struct objc_ivar_list {
		public int ivar_count;
	};

	public struct objc_ivar {
		public uint name;
		public uint type;
		public int offset;
	};

}
