using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSResponder : NSObject, NSCoding {
		private NSResponder() : this(IntPtr.Zero) {}
		protected internal NSResponder(IntPtr raw) : base (raw) {}
	}
}
