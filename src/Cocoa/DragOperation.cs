using System;

namespace Cocoa {
	public enum DragOperation {
		None = 0,
		Copy = 1,
		Link = 2,
		Generic = 4,
		Private = 8,
		All_Obsolete = 15,
		Move = 16,
		Delete = 32,
		Every = Int32.MaxValue
	}
}
