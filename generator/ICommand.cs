//
// $Id: ICommand.cs,v 1.4 2004/09/03 19:10:05 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	public interface ICommand {
		void ProcessCommand ();
	}
}
