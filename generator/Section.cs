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

		public void ProcessSection () {
			unsafe {
				sec = *((section *)mfile.Pointer);
				sectname = Marshal.PtrToStringAuto (new IntPtr (mfile.Pointer), 16);
				segname = Marshal.PtrToStringAuto (new IntPtr (mfile.Pointer+16), 16);
				mfile.Pointer += (int)Marshal.SizeOf (sec);
			}

			Console.WriteLine ("\t\tSectName: {0}", sectname);
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct section {
		[FieldOffset(0)] public IntPtr sectname;
		[FieldOffset(16)] public IntPtr segname;
		[FieldOffset(32)] public uint addr;
		[FieldOffset(36)] public uint size;
		[FieldOffset(40)] public uint offset;
		[FieldOffset(44)] public uint align;
		[FieldOffset(48)] public uint reloff;
		[FieldOffset(52)] public uint nreloc;
		[FieldOffset(56)] public uint flags;
		[FieldOffset(60)] public uint reserved1;
		[FieldOffset(64)] public uint reserved2;
	}
}
