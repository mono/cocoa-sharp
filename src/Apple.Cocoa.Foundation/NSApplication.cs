using System;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{

	enum NSBackingStoretype {
		NSBackingStoreRetained = 0,
		NSBackingStoreNonretained = 1,
		NSBackingStoreBuffered = 2,
	};

	public class NSApplication : NSResponder {
		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSApplication_alloc();
		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSApplication_sharedApplication();

		[DllImport("CoreFoundationGlue")]
		static extern int NSApplication_runModalForWindow(IntPtr THIS, IntPtr theWindow);

		private NSApplication() : this(IntPtr.Zero) {}
		protected internal NSApplication(IntPtr raw) : base(raw) {}

		public static NSApplication sharedApplication()
		{
			return new NSApplication(NSApplication_sharedApplication());
		}

		public int runModalForWindow(NSWindow theWindow)
		{
			return NSApplication_runModalForWindow(Raw, theWindow.Raw);
		}
	}
}
