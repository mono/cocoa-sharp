using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSAutoreleasePool : NSObject {
		static IntPtr NSAutoreleasePool_class = NSString.NSClass("NSAutoreleasePool");

		public NSAutoreleasePool() : this(NSObject__alloc(NSAutoreleasePool_class)) {}
		protected internal NSAutoreleasePool(IntPtr raw) : base(raw) {}

		public static NSAutoreleasePool alloc() {
			return new NSAutoreleasePool();
		}
	}
}
