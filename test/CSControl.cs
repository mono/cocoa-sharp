using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSControl : NSObject {
	static IntPtr CSControl_class = NSRegisterClass(typeof(CSControl), "NSObject");
	
	NSButton swap1;
	CSTextControl textController;
	BridgeDelegate CSControlDelegate;
	public CSControl() : this(NSObject__alloc0(CSControl_class),true) {}

	protected internal CSControl(IntPtr raw,bool release) : base(raw,release) {
		textController = new CSTextControl();
		CSControlDelegate = new BridgeDelegate(this.MethodInvoker);
		SetRaw(DotNetForwarding_initWithManagedDelegate(Raw,CSControlDelegate),release);
	}

	public void displayWindow() {

		NSApplication.SharedApplication().setApplicationIconImage((NSImage)NSImage.ImageNamed("mono.icns"));

		NSRect contentRect = new NSRect(200, 180, 400, 300);

		NSWindow window = new NSWindow(new NSRect(200, 180, 400, 300),
					       (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					       NSBackingStoreType.NSBackingStoreBuffered,
					       false);
		window.Title = "Hi Mono";

		NSButton monoButton = new NSButton(new NSRect(20, 20, 74, 25));
		monoButton.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		monoButton.Title = "Dismiss";
		monoButton.Target = this;
		monoButton.Action = "_stop";
		
		swap1 = new NSButton(new NSRect(20, 50, 350, 25));
		swap1.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		swap1.Title = "Tickle me";
		swap1.Target = this;
		swap1.Action = "_swap";

		NSTextField text = new NSTextField(new NSRect(100, 200, 78, 25));
		text.Editable = true;
		text.Bezeled = true;
		text.StringValue = "Hello, Mono";
		text.Delegate = textController;

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(swap1);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.SharedApplication().runModalForWindow(window);
	}

	new public void init() {
		Console.WriteLine("init from .Net!");
	}

	public void _stop() {
		Console.WriteLine("Cool ass SHIT!");
		NSApplication.SharedApplication().stopModal();
	}

	public void _swap() {
		swap1.Title = "I got tickled @ " + DateTime.Now;
	}
}
