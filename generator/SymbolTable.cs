using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
	
		public SymbolTable (byte *ptr) {
			ocsymtab = *((objc_symtab *)ptr);
//			for (int i = 0; i < ocsymtab.cls_def_cnt; i++, ptr += (int)Marshal.SizeOf (ocsymtab)) { 
				Class cls = new Class ((byte *)ocsymtab.defs.ToInt32 ());
//			}
			
		}
	}

	public struct objc_symtab {
		public uint sel_ref_cnt;
		public IntPtr refs;
		public ushort cls_def_cnt;
		public ushort cat_def_cnt;
		public IntPtr defs;
	}
}
