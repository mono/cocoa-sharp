using System;
using Apple.Foundation;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.AppKit
{
	public class NSControl : NSView {
		[DllImport("AppKitGlue")]
		static extern IntPtr NSControl_initWithFrame(IntPtr THIS, NSRect frameRect);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setTarget(IntPtr THIS, IntPtr anObject);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setAction(IntPtr THIS, IntPtr aSelector);
		[DllImport("AppKitGlue")]
		static extern void NSControl_setStringValue(IntPtr THIS, IntPtr aString);
		
		private NSControl() {}

		protected NSControl(IntPtr raw) : base(raw) {}

		override public IntPtr initWithFrame(NSRect frameRect)
		{
			return NSControl_initWithFrame(Raw,frameRect);
		}
	
		public void setTarget(NSObject anObject)
		{
			NSControl_setTarget(Raw, anObject.Raw);
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
