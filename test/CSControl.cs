using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSControl : NSObject {
	static IntPtr CSControl_class = NSRegisterClass(typeof(CSControl));
	
	NSButton swap1;
	BridgeDelegate CSControlDelegate;
	public CSControl() : this(NSObject__alloc(CSControl_class),true) {}

	protected internal CSControl(IntPtr raw,bool release) : base(raw,release) {
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

		NSRect contentRect = new NSRect(200, 180, 300, 300);

		NSWindow window = new NSWindow();
		window.initWithContentRect_styleMask_backing_defer(contentRect, 
			NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
			NSBackingStoreBuffered, false);
		window.Title = "Hi Mono";

		NSButton monoButton = new NSButton();
		monoButton.initWithFrame(new NSRect(20, 20, 74, 17));
		monoButton.Title = "Dismiss";
		monoButton.Target = this;
		monoButton.Action = "_stop";
		
		swap1 = new NSButton();
		swap1.initWithFrame(new NSRect(40, 40, 104, 17));
		swap1.Title = "Tickle me";
		swap1.Target = this;
		swap1.Action = "_swap";

		NSTextField text = new NSTextField();
		text.initWithFrame(new NSRect(100, 200, 78, 20));
		text.Editable = false;
		text.Bezeled = true;
		text.StringValue = "Hello, Mono";

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(swap1);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.sharedApplication().runModalForWindow(window);
	}

	public void init() {
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
