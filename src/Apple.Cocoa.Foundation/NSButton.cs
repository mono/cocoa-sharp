using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSButton : NSControl {
		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSButton_alloc();
		[DllImport("CoreFoundationGlue")]
		static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
		
		public NSButton() : this(NSButton_alloc()) {}
		protected internal NSButton(IntPtr raw) : base(raw) {}

		public static NSButton alloc() {
			return new NSButton();
		}
		
		public void setTitle(NSString aString) {
			NSButton_setTitle(Raw, aString.Raw);
		}
	}
}
