//
// $Id: LoadCommand.cs,v 1.3 2004/09/03 19:10:05 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class LoadCommand : ICommand {

		private MachOFile mfile;
		private load_command lcmd;
	
		public LoadCommand (MachOFile mfile, load_command lcmd) {
			this.mfile = mfile;
			this.lcmd = lcmd;
		}

		public void ProcessCommand () {
			unsafe {
				mfile.Pointer += (int)(lcmd.cmdsize - Marshal.SizeOf (lcmd));
			}
		}
	}
}
