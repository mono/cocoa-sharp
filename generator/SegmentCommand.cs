//
// $Id: SegmentCommand.cs,v 1.8 2004/09/04 04:18:32 urs Exp $
//

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

		public string Name {
			get { return name; }
		}

		public bool ContainsAddress(uint offset) {
			int off = (int)(offset-scmd.vmaddr);
			return (off >= 0) && (off < scmd.vmsize);
		}

		public Section SectionContainingVMAddr(uint offset) {
			foreach (Section section in this.sections)
				if (section.ContainsAddress(offset))
					return section;

			return null;
		}

		public uint SegmentOffsetForVMAddr(uint offset) {
			Section section = this.SectionContainingVMAddr(offset);
			return section.SegmentOffsetForVMAddr(offset);
		}

		public Section SectionWithName(string name) {
			foreach (Section section in this.sections)
				if (section.Name == name)
					return section;
			return null;
		}

		unsafe public byte* HeadPointer {
			get { return mfile.HeadPointer; }
		}

		unsafe public byte* GetPtr(uint offset) {
			return mfile.GetPtr(offset);
		}

		public string GetString(uint offset) {
			return mfile.GetString(offset);
		}

		public int VMAddr {
			get { return (int)scmd.vmaddr; }
		}

		public int FileOffset {
			get { return (int)scmd.fileoff; }
		}

		public ArrayList Sections {
			get { return sections; }
		}

		public void ProcessCommand () {
			unsafe {
				scmd = *((segment_command *)mfile.Pointer);
				Utils.MakeBigEndian(ref scmd.vmaddr);
				Utils.MakeBigEndian(ref scmd.vmsize);
				Utils.MakeBigEndian(ref scmd.fileoff);
				Utils.MakeBigEndian(ref scmd.filesize);
				Utils.MakeBigEndian(ref scmd.maxprot);
				Utils.MakeBigEndian(ref scmd.initprot);
				Utils.MakeBigEndian(ref scmd.nsects);
				Utils.MakeBigEndian(ref scmd.flags);
				name = Utils.GetString(mfile.Pointer, 16);
				mfile.Pointer += (int)Marshal.SizeOf(scmd);
			}
			MachOFile.DebugOut(0,"\tSegment Name: {0} addr={1,8:x} size={2}", name, scmd.vmaddr, scmd.vmsize);

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
		[FieldOffset(0)]
		public byte segname0,segname1,segname2,segname3,segname4,segname5,segname6,segname7,
					segname8,segname9,segname10,segname11,segname12,segname13,segname14,segname15;
		[FieldOffset(16)]
		public uint vmaddr;
		[FieldOffset(20)]
		public uint vmsize;
		[FieldOffset(24)]
		public uint fileoff;
		[FieldOffset(28)]
		public uint filesize;
		[FieldOffset(32)]
		public uint maxprot;
		[FieldOffset(36)]
		public uint initprot;
		[FieldOffset(40)]
		public uint nsects;
		[FieldOffset(44)]
		public uint flags;
	}
}
