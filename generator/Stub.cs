//
// $Id: Stub.cs,v 1.6 2004/09/09 02:33:04 urs Exp $
//

using System;

namespace CocoaSharp {

	public class Test {

		static void Main (string [] args) {
			foreach (string arg in args)
				new MachOFile(arg);
			
			foreach (MachOType t in MachOFile.Types.Values)
				if (t.fields.Length == 0)
					MachOFile.DebugOut(1,"undef {0}",t.ToString());
		}
	}
}
