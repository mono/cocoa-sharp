using System;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSAutoreleasePool : NSObject {

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSAutoreleasePool_alloc();

		public NSAutoreleasePool() : this(NSAutoreleasePool_alloc()) {}
		protected internal NSAutoreleasePool(IntPtr raw) : base(raw) {}

		public static NSAutoreleasePool alloc() {
			return new NSAutoreleasePool();
		}
	}
}
