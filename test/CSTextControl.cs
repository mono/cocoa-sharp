using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSTextControl : NSControl {
	NSButton swap1;

	[Export("controlTextDidEndEditing:")]
	public void TextDidEndEditing(NSConcreteNotification aNotification) {
		Console.WriteLine("controlTextDidEndEditing(): Received delegate method: {0}", ((Apple.AppKit.NSTextField)aNotification.object_).stringValue);
	}

}
