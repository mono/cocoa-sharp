using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{

	enum NSBackingStoretype {
		NSBackingStoreRetained = 0,
		NSBackingStoreNonretained = 1,
		NSBackingStoreBuffered = 2,
	};

	public class NSApplication : NSResponder {
		static IntPtr NSApplication_class = Apple.Foundation.NSString.NSClass("NSApplication");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSApplication__sharedApplication(IntPtr CLASS);

		[DllImport("AppKitGlue")]
		static extern int NSApplication_runModalForWindow(IntPtr THIS, IntPtr theWindow);

		private NSApplication() : this(IntPtr.Zero) {}
		protected internal NSApplication(IntPtr raw) : base(raw) {}

		public static NSApplication sharedApplication()
		{
			return new NSApplication(NSApplication__sharedApplication(NSApplication_class));
		}

		public int runModalForWindow(NSWindow theWindow)
		{
			return NSApplication_runModalForWindow(Raw, theWindow.Raw);
		}
	}
}
