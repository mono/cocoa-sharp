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
		const int NSBorderlessWindowMask	= 0;
		const int NSTitledWindowMask		= 1 << 0;
		const int NSClosableWindowMask		= 1 << 1;
		const int NSMiniaturizableWindowMask	= 1 << 2;
		const int NSResizableWindowMask		= 1 << 3;
		
		const int NSBackingStoreBuffered	= 2;

		NSApplication.sharedApplication().setApplicationIconImage(NSImage.imageNamed("mono.icns"));

		NSRect contentRect = new NSRect(200, 180, 400, 300);

		NSWindow window = new NSWindow(new NSRect(200, 180, 400, 300),
					       NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
					       NSBackingStoreBuffered,
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

		NSApplication.sharedApplication().runModalForWindow(window);
	}

	new public void init() {
		Console.WriteLine("init from .Net!");
	}

	public void _stop() {
		Console.WriteLine("Cool ass SHIT!");
		NSApplication.sharedApplication().stopModal();
	}

	public void _swap() {
		swap1.Title = "I got tickled @ " + DateTime.Now;
	}
}
