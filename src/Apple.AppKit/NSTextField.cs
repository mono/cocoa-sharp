using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSTextField : NSControl {
		static IntPtr NSTextField_class = Apple.Foundation.NSString.NSClass("NSTextField");

		[DllImport("AppKitGlue")]
		static extern void NSTextField_setEditable(IntPtr THIS, bool flag);
		[DllImport("AppKitGlue")]
		static extern void NSTextField_setBezeled(IntPtr THIS, bool flag);

		public NSTextField() : this(NSObject__alloc(NSTextField_class)) {}
		protected NSTextField(IntPtr raw) : base(raw) {}

		public void setEditable(bool flag) {
			NSTextField_setEditable(Raw, flag);
		}
		
		public void setBezeled(bool flag) {
			NSTextField_setBezeled(Raw, flag);
		}
	}
}
