using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSTextControl : NSControl {
	static IntPtr CSTextControl_class = NSRegisterClass(typeof(CSTextControl), "NSControl");
	
	NSButton swap1;
	BridgeDelegate CSTextControlDelegate;
	public CSTextControl() : this(NSObject__alloc(CSTextControl_class),true) {}

	protected internal CSTextControl(IntPtr raw,bool release) : base(raw,release) {
		CSTextControlDelegate = new BridgeDelegate(this.MethodInvoker);
		SetRaw(DotNetForwarding_initWithManagedDelegate(Raw,CSTextControlDelegate),release);
	}

	public void controlTextDidEndEditing(NSConcreteNotification aNotification) {
		Console.WriteLine("controlTextDidEndEditing(): Received delegate method: {0}", ((Apple.AppKit.NSTextField)aNotification.Object).StringValue);
	}

}
