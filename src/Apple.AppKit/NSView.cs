using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSView : NSResponder {
                static IntPtr NSView_class = Apple.Foundation.NSString.NSClass("NSView");

		[DllImport("AppKitGlue")]
		static extern void NSView_addSubview(IntPtr THIS, IntPtr aView);
		
		[DllImport("AppKitGlue")]
		static extern IntPtr NSView_initWithFrame(IntPtr THIS, NSRect frame);
		
		public NSView() : this(NSObject__alloc(NSView_class)) {}
		protected internal NSView(IntPtr raw) : base(raw) {}

		virtual public IntPtr initWithFrame(NSRect frame) {
			return NSView_initWithFrame(Raw, frame);
		}

		public void addSubview(NSView aView) {
			NSView_addSubview(Raw, aView.Raw);
		}
	}
}
