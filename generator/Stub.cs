using System;

namespace CocoaSharp {

	public class Test {

		static void Main (string [] args) {
			MachOFile mfile = new MachOFile(args[0]);
		}
	}
}
