//
// $Id: SymbolTable.cs,v 1.3 2004/09/03 20:05:30 gnorton Exp $
//

using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
	
		/// <summary>
		/// Creates a new <see cref="SymbolTable"/> instance.
		/// </summary>
		/// <param name="offset">Offset.</param>
		public SymbolTable (byte *headptr, uint offset, SegmentCommand objcSegment) {
			ocsymtab = *((objc_symtab *)(headptr+(int)(offset - objcSegment.VMAddr + objcSegment.FileOffset)));
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cls_def_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cat_def_cnt);

			uint *defptr = ocsymtab.defs;
			//for (int i = 0; i < ocsymtab.cls_def_cnt; i++, defptr++) {
				Class cls = new Class (headptr, *defptr, objcSegment);
			//}
		}
	}

	unsafe public struct objc_symtab {
		public uint sel_ref_cnt;
		public void *refs;
		public ushort cls_def_cnt;
		public ushort cat_def_cnt;
		public uint *defs;
	}
}
