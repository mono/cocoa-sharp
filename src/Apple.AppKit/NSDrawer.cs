using System;
using Apple.Foundation;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSDrawer : NSObject {
		static IntPtr NSDrawer_class = Apple.Foundation.NSString.NSClass("NSDrawer");

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_initWithContentSize_preferredEdge(IntPtr THIS, NSSize size, NSRectEdge edge);

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_toggle(IntPtr THIS, IntPtr SENDER);

		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_setParentWindow(IntPtr THIS, IntPtr WINDOW);
		
		[DllImport("AppKitGlue")]
		static extern IntPtr NSDrawer_setContentView(IntPtr THIS, IntPtr VIEW);

		public NSDrawer() : this(NSObject__alloc(NSDrawer_class)) {}
		protected internal NSDrawer(IntPtr raw) : base (raw) {}

		public IntPtr initWithContentSize_preferredEdge(NSSize size, NSRectEdge edge) {
			return NSDrawer_initWithContentSize_preferredEdge(Raw, size, edge);
		}

		public void setParentWindow(NSWindow window) {
			NSDrawer_setParentWindow(Raw, window.Raw);
		}
		
		public void setContentView(NSView view) {
			NSDrawer_setContentView(Raw, view.Raw);
		}

		public void toggle(IntPtr sender) {
			NSDrawer_toggle(Raw, sender);
		}
	}
}
