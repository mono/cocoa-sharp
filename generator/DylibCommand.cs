//
// $Id: DylibCommand.cs,v 1.2 2004/09/03 17:30:24 urs Exp $
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
				name = Marshal.PtrToStringAuto (new IntPtr (mfile.Pointer + dld.offset - Marshal.SizeOf (lcmd)));
				mfile.Pointer += (int)(lcmd.cmdsize - Marshal.SizeOf (lcmd));
			}
		}
	}


	[StructLayout(LayoutKind.Explicit)]
	public struct dylib {
		[FieldOffset(0)] public uint offset;
		[FieldOffset(4)] public uint timestamp;
		[FieldOffset(8)] public uint current_version;
		[FieldOffset(10)] public uint compatability_version;
	}
}
