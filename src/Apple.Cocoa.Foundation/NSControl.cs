using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSControl : NSView {
		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSControl_initWithFrame(IntPtr THIS, NSRect frameRect);
		[DllImport("CoreFoundationGlue")]
		static extern void NSControl_setTarget(IntPtr THIS, IntPtr anObject);
		[DllImport("CoreFoundationGlue")]
		static extern void NSControl_setAction(IntPtr THIS, IntPtr aSelector);
		[DllImport("CoreFoundationGlue")]
		static extern void NSControl_setStringValue(IntPtr THIS, IntPtr aString);
		
		private NSControl() {}

		protected NSControl(IntPtr raw) : base(raw) {}

		public NSControl initWithFrame(NSRect frameRect)
		{
			Raw = NSControl_initWithFrame(Raw,frameRect);
			return this;
		}
	
		public void setTarget(IntPtr anObject)
		{
			NSControl_setTarget(Raw, anObject);
		}

		public void setAction(IntPtr aSelector)
		{
			NSControl_setAction(Raw, aSelector);
		}

		public void setStringValue(NSString aString)
		{
			NSControl_setStringValue(Raw, aString.Raw);
		}
	}
}
