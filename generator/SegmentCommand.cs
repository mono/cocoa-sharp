using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class SegmentCommand : ICommand {
	
		private MachOFile mfile;
		private load_command lcmd;
		private segment_command scmd;
		private string name;
		private ArrayList sections;

		public SegmentCommand (MachOFile mfile, load_command lcmd) {
			this.mfile = mfile;
			this.lcmd = lcmd;
			sections = new ArrayList ();
		}

		public void ProcessCommand () {
			unsafe {
				scmd = *((segment_command *)mfile.Pointer);
				name = Marshal.PtrToStringAuto (new IntPtr(mfile.Pointer), 16);
				mfile.Pointer += (int)Marshal.SizeOf (scmd);
			}
			Console.WriteLine ("\tSegment Name: {0}", name);

			ProcessSections ();
		}

		private void ProcessSections () {
			for (int i = 0; i < scmd.nsects; i++) {
				Section sec = new Section(mfile, scmd);
				sec.ProcessSection ();
				sections.Add (sec);
			}
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct segment_command {
		[FieldOffset(0)] public char[] segname;
		[FieldOffset(16)] public uint vmaddr;
		[FieldOffset(20)] public uint vmsize;
		[FieldOffset(24)] public uint fileoff;
		[FieldOffset(28)] public uint filesize;
		[FieldOffset(32)] public uint maxprot;
		[FieldOffset(36)] public uint initprot;
		[FieldOffset(40)] public uint nsects;
		[FieldOffset(44)] public uint flags;
	}
}
