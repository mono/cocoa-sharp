using System;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSWindow : NSResponder {

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSWindow_alloc();

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSWindow_initWithContentRect_styleMask_backing_defer(IntPtr THIS, NSRect contentRec, uint aStyle, int bufferingType, bool flag);

		[DllImport("CoreFoundationGlue")]
		static extern void NSWindow_setTitle(IntPtr THIS, IntPtr aString);

		[DllImport("CoreFoundationGlue")]
		static extern void NSWindow_center(IntPtr THIS);

		[DllImport("CoreFoundationGlue")]
		static extern void NSWindow_makeKeyAndOrderFront(IntPtr THIS, IntPtr sender);

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSWindow_contentView(IntPtr THIS);

		public NSWindow() : this(NSWindow_alloc()) {}
		protected internal NSWindow(IntPtr raw) : base (raw) {}

		public static NSWindow alloc()
		{
			return new NSWindow();
		}

		public IntPtr initWithContentRect_styleMask_backing_defer(NSRect contentRect, uint aStyle, int bufferingType, bool flag)
		{
			return NSWindow_initWithContentRect_styleMask_backing_defer(Raw, contentRect, aStyle, bufferingType, flag);
		}

		public void setTitle(NSString aString)
		{
			NSWindow_setTitle(Raw, aString.Raw);
		}

		public void center()
		{
			NSWindow_center(Raw);
		}
		
		public void makeKeyAndOrderFront(IntPtr sender)
		{
			NSWindow_makeKeyAndOrderFront(Raw, sender);
		}
		
		public NSObject contentView()
		{
			return new NSView(NSWindow_contentView(Raw));
		}
	}
}
