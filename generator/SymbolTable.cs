//
// $Id: SymbolTable.cs,v 1.4 2004/09/03 20:32:17 urs Exp $
//

using System;
using System.Runtime.InteropServices;

namespace CocoaSharp 
{

	unsafe public class SymbolTable {
		private objc_symtab ocsymtab;
	
		/// <summary>
		/// Creates a new <see cref="SymbolTable"/> instance.
		/// </summary>
		/// <param name="offset">Offset.</param>
		public SymbolTable (byte *headptr, uint offset, SegmentCommand objcSegment) {
			byte *ptr = headptr+(int)(offset - objcSegment.VMAddr + objcSegment.FileOffset);
			ocsymtab = *((objc_symtab *)ptr);
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.sel_ref_cnt);
			Utils.MakeBigEndian(ref ocsymtab.cat_def_cnt);

			uint *defptr = (uint*)(ptr + Marshal.SizeOf(ocsymtab));
			int defIndex = 0;
			if (ocsymtab.cls_def_cnt > 0) 
			{
				for (int index = 0; index < ocsymtab.cls_def_cnt; ++index, ++defptr, ++defIndex) {
					Class cls = new Class (headptr, *defptr, objcSegment);
				}
			}
//			for (int i = 0; i < ocsymtab.cls_def_cnt; i++, ptr += (int)Marshal.SizeOf (ocsymtab)) { 
//				Class cls = new Class ((byte *)ocsymtab.defs.ToInt32 ());
//			}
		}
	}

	unsafe public struct objc_symtab {
		public uint sel_ref_cnt;
		public void *refs;
		public ushort cls_def_cnt;
		public ushort cat_def_cnt;
	}
}
