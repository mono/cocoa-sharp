using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSAutoreleasePool : NSObject {

		[DllImport("FoundationGlue")]
		static extern IntPtr NSAutoreleasePool__alloc();

		public NSAutoreleasePool() : this(NSAutoreleasePool__alloc()) {}
		protected internal NSAutoreleasePool(IntPtr raw) : base(raw) {}

		public static NSAutoreleasePool alloc() {
			return new NSAutoreleasePool();
		}
	}
}
