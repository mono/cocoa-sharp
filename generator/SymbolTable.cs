//
// $Id: SymbolTable.cs,v 1.5 2004/09/03 21:46:29 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
		private ArrayList classes = new ArrayList();

		public SymbolTable (uint offset, MachOFile file) {
			byte *ptr = file.GetPtr(offset);
			ocsymtab = *((objc_symtab *)ptr);
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cls_def_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cat_def_cnt);

			uint *defptr = (uint*)(ptr + Marshal.SizeOf(ocsymtab));
			for (int i = 0; i < ocsymtab.cls_def_cnt; ++i, ++defptr) {
				Utils.MakeBigEndian(ref *defptr);
				Class cls = new Class(*defptr, file);
				classes.Add(cls);
			}
		}
	}

	unsafe public struct objc_symtab {
		public uint sel_ref_cnt;
		public void *refs;
		public ushort cls_def_cnt;
		public ushort cat_def_cnt;
	}
}
