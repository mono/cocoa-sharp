//
// $Id: Stub.cs,v 1.5 2004/09/08 14:22:44 urs Exp $
//

using System;

namespace CocoaSharp {

	public class Test {

		static void Main (string [] args) {
			foreach (string arg in args)
				new MachOFile(arg);
			
			foreach (OCType t in MachOFile.Types.Values)
				if (t.fields.Length == 0)
					MachOFile.DebugOut(1,"undef {0}",t.ToString());
		}
	}
}
