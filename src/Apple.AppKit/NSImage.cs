using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSImage : NSObject {
		static IntPtr NSImage_class = Apple.Foundation.NSString.NSClass("NSImage");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSImage__imageNamed(IntPtr CLASS, IntPtr name);

		public NSImage() : this(NSObject__alloc(NSImage_class)) {}
		protected internal NSImage(IntPtr raw) : base (raw) {}

		public static NSImage imageNamed(NSString name) {
			return new NSImage(NSImage__imageNamed(NSImage_class, name.Raw));
		}
	}
}
