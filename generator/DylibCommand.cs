//
// $Id: DylibCommand.cs,v 1.3 2004/09/03 19:10:05 urs Exp $
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


	[StructLayout(LayoutKind.Explicit)]
	public struct dylib {
		[FieldOffset(0)] public uint offset;
		[FieldOffset(4)] public uint timestamp;
		[FieldOffset(8)] public uint current_version;
		[FieldOffset(10)] public uint compatability_version;
	}
}
