//
// $Id: DylibCommand.cs,v 1.4 2004/09/07 21:02:43 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class DylibCommand : ICommand {

		private load_command lcmd;
		private dylib dld;
		private string name;
		private MachOFile mfile;
	
		public DylibCommand (MachOFile mfile, load_command lcmd) {
			this.mfile = mfile;
			this.lcmd = lcmd;
		}

		public void ProcessCommand () {
			unsafe {
				dld = *((dylib *)mfile.Pointer);
				Utils.MakeBigEndian(ref dld.offset);
				Utils.MakeBigEndian(ref dld.timestamp);
				Utils.MakeBigEndian(ref dld.current_version);
				Utils.MakeBigEndian(ref dld.compatability_version);
				name = Marshal.PtrToStringAuto (new IntPtr (mfile.Pointer + dld.offset - Marshal.SizeOf (lcmd)));
				mfile.Pointer += (int)(lcmd.cmdsize - Marshal.SizeOf (lcmd));
			}
		}
	}


	// http://developer.apple.com/documentation/DeveloperTools/Conceptual/MachORuntime/FileStructure/chapter_4_section_12.html#//apple_ref/doc/c_ref/dylib
	//
	// Defines the data used by the dynamic linker to match a shared library against the files that have linked to it. Used exclusively in the dylib_command
	// data structure.
	public struct dylib {
		// A data structure of type lc_str. Specifies the name of the shared library.
		public uint offset;
		// The date and time when the shared library was built.
		public uint timestamp;
		// The current version of the shared library.
		public uint current_version;
		// The compatibility version of the shared library.
		public uint compatability_version;
	}
}
