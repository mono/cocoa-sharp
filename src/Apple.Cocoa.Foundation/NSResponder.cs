using System;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSResponder : NSObject, NSCoding {
		private NSResponder() : this(IntPtr.Zero) {}
		protected internal NSResponder(IntPtr raw) : base (raw) {}
	}
}
