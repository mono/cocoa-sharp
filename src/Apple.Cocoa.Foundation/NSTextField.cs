using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSTextField : NSControl {
		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSTextField_alloc();
		[DllImport("CoreFoundationGlue")]
		static extern void NSTextField_setEditable(IntPtr THIS, bool flag);
		[DllImport("CoreFoundationGlue")]
		static extern void NSTextField_setBezeled(IntPtr THIS, bool flag);

		public NSTextField() : this(NSTextField_alloc()) {}
		protected NSTextField(IntPtr raw) : base(raw) {}

		public static NSTextField alloc() {
			return new NSTextField();
		}
		
		public void setEditable(bool flag) {
			NSTextField_setEditable(Raw, flag);
		}
		
		public void setBezeled(bool flag) {
			NSTextField_setBezeled(Raw, flag);
		}
	}
}
