using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSView : NSResponder {
		[DllImport("CoreFoundationGlue")]
		static extern void NSView_addSubview(IntPtr THIS, IntPtr aView);
		
		public NSView() : this(IntPtr.Zero) {}
		protected internal NSView(IntPtr raw) : base(raw) {}

		public void addSubview(IntPtr aView) {
			NSView_addSubview(Raw, aView);
		}
	}
}
