//
// $Id: SymbolTable.cs,v 1.2 2004/09/03 19:10:05 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
	
		/// <summary>
		/// Creates a new <see cref="SymbolTable"/> instance.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public SymbolTable (byte *ptr) {
			ocsymtab = *((objc_symtab *)ptr);
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cls_def_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cat_def_cnt);

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
