//
// $Id: Section.cs,v 1.6 2004/09/03 21:46:29 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class Section {
	
		private MachOFile mfile;
		private segment_command scmd;
		private section sec;
		private string segname;
		private string sectname;

		public Section (MachOFile mfile, segment_command scmd) {
			this.mfile = mfile;
			this.scmd = scmd;
		}

		public string Name {
			get { return sectname; }
		}
		
		public uint Offset {
			get { return sec.offset; }
		}
		
		public uint Addr {
			get { return sec.addr; }
		}
		
		public uint Size {
			get { return sec.size; }
		}

		public bool ContainsAddress(uint offset) {
			return (offset >= Addr) && (offset < Addr + Size);
		}

		public uint SegmentOffsetForVMAddr(uint offset) {
			return offset - Addr;
		}

		public void ProcessSection () {
			unsafe {
				sec = *((section *)mfile.Pointer);
				Utils.MakeBigEndian(ref sec.addr);
				Utils.MakeBigEndian(ref sec.size);
				Utils.MakeBigEndian(ref sec.offset);
				Utils.MakeBigEndian(ref sec.align);
				Utils.MakeBigEndian(ref sec.reloff);
				Utils.MakeBigEndian(ref sec.nreloc);
				Utils.MakeBigEndian(ref sec.flags);
				Utils.MakeBigEndian(ref sec.reserved1);
				Utils.MakeBigEndian(ref sec.reserved2);
				sectname = Utils.GetString(mfile.Pointer, 16);
				segname = Utils.GetString(mfile.Pointer+16, 16);
				mfile.Pointer += (int)Marshal.SizeOf (sec);
			}

			Console.WriteLine ("\t\tSectName: {0}", sectname);
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct section {
		[FieldOffset(0)]
		public byte sectname0,sectname1,sectname2,sectname3,sectname4,sectname5,sectname6,sectname7,
			sectname8,sectname9,sectname10,sectname11,sectname12,sectname13,sectname14,sectname15;
		[FieldOffset(16)]
		public byte segname0,segname1,segname2,segname3,segname4,segname5,segname6,segname7,
			segname8,segname9,segname10,segname11,segname12,segname13,segname14,segname15;
		[FieldOffset(32)]
		public uint addr;
		[FieldOffset(36)]
		public uint size;
		[FieldOffset(40)]
		public uint offset;
		[FieldOffset(44)]
		public uint align;
		[FieldOffset(48)]
		public uint reloff;
		[FieldOffset(52)]
		public uint nreloc;
		[FieldOffset(56)]
		public uint flags;
		[FieldOffset(60)]
		public uint reserved1;
		[FieldOffset(64)]
		public uint reserved2;
	}
}
