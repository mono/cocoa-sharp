//
// $Id: Ivar.cs,v 1.1 2004/09/04 04:49:30 gnorton Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	public class Ivar {
		private string name, type;
		private int offset;

		public Ivar(objc_ivar ivar, MachOFile file) {
			Utils.MakeBigEndian(ref ivar.name);
			Utils.MakeBigEndian(ref ivar.type);
			Utils.MakeBigEndian(ref ivar.offset);
			name = file.GetString(ivar.name);
			type = file.GetString(ivar.type);
			offset = ivar.offset;
			MachOFile.DebugOut(1,"\tvar: {0} type={1} offset={2}", name, type, offset);
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
