//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: SymbolTable.cs,v 1.1 2004/09/09 01:18:47 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
		private ArrayList classes = new ArrayList();
		private ArrayList categories = new ArrayList();

		public SymbolTable (byte *ptr, MachOFile file) {
			ocsymtab = *((objc_symtab *)ptr);
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cls_def_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cat_def_cnt);

			MachOFile.DebugOut(1,"SymbolTable #sel={0}, #cls={1}, #cat={2}",ocsymtab.sel_ref_cnt,ocsymtab.cls_def_cnt,ocsymtab.cat_def_cnt);
			uint *defptr = (uint*)(ptr + Marshal.SizeOf(ocsymtab));
			for (int i = 0; i < ocsymtab.cls_def_cnt; ++i, ++defptr) {
				Utils.MakeBigEndian(ref *defptr);
				ptr = file.GetPtr(*defptr);
				if (ptr != null) {
					MachOClass cls = new MachOClass(ptr, file);
					classes.Add(cls);
				}
			}
			for (int i = 0; i < ocsymtab.cat_def_cnt; ++i, ++defptr) {
				Utils.MakeBigEndian(ref *defptr);
				ptr = file.GetPtr(*defptr);
				if (ptr != null) {
					MachOCategory cat = new MachOCategory(ptr, file);
					categories.Add(cat);
				}
			}
		}
	}

	internal struct objc_symtab {
		internal uint sel_ref_cnt;
		internal uint refs;
		internal ushort cls_def_cnt;
		internal ushort cat_def_cnt;
	}
}
