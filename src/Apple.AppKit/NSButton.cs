using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSButton : NSControl {
		[DllImport("AppKitGlue")]
		static extern IntPtr NSButton__alloc();
		[DllImport("AppKitGlue")]
		static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
		
		public NSButton() : this(NSButton__alloc()) {}
		protected internal NSButton(IntPtr raw) : base(raw) {}

		public static NSButton alloc() {
			return new NSButton();
		}
		
		public void setTitle(NSString aString) {
			NSButton_setTitle(Raw, aString.Raw);
		}
	}
}
