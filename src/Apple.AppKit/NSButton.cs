using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public enum NSBezelStyle {
		NSRoundedBezelStyle = 1,
		NSRegularSquareBezelStyle = 2,
		NSThickSquareBezelStyle = 3,
		NSThickerSquareBezelStyle = 4,
		NSDisclosureBezelStyle = 5,
		NSShadowlessSquareBezelStyle = 6,
		NSCircularBezelStyle = 7,
		NSTexturedSquareBezelStyle = 8,
		NSHelpButtonBezelStyle = 9
	} 

	public class NSButton : NSControl {
		static IntPtr NSButton_class = Apple.Foundation.NSString.NSClass("NSButton");

		[DllImport("AppKitGlue")]
		static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
		
		[DllImport("AppKitGlue")]
		static extern void NSButton_setBezelStyle(IntPtr THIS, NSBezelStyle style);
		
		public NSButton() : this(NSObject__alloc(NSButton_class)) {}
		protected internal NSButton(IntPtr raw) : base(raw) {}

		public void setTitle(NSString aString) {
			NSButton_setTitle(Raw, aString.Raw);
		}

		public void setBezelStyle(NSBezelStyle style) {
			NSButton_setBezelStyle(Raw, style);
		}
	}
}
