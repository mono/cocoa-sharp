using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSView : NSResponder {
		[DllImport("AppKitGlue")]
		static extern void NSView_addSubview(IntPtr THIS, IntPtr aView);
		
		public NSView() : this(IntPtr.Zero) {}
		protected internal NSView(IntPtr raw) : base(raw) {}

		public void addSubview(NSView aView) {
			NSView_addSubview(Raw, aView.Raw);
		}
	}
}
