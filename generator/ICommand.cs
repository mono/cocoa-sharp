//
// $Id: ICommand.cs,v 1.3 2004/09/03 17:30:24 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	public interface ICommand {
		void ProcessCommand ();
	}
}
