using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSControl : NSObject {
	NSButton swap1;
	CSTextControl textController;
	public CSControl() {
		textController = new CSTextControl();
	}

	public void displayWindow() {
		NSApplication.SharedApplication.applicationIconImage = (NSImage)NSImage.ImageNamed("mono.icns");

		NSWindow window = new NSWindow(new NSRect(200, 180, 400, 300),
					       (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					       NSBackingStoreType.NSBackingStoreBuffered,
					       false);
		window.title = "Hi Mono";

		NSButton monoButton = new NSButton(new NSRect(20, 20, 74, 25));
		monoButton.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		monoButton.title = "Dismiss";
		monoButton.target = this;
		monoButton.action = "_stop";
		
		swap1 = new NSButton(new NSRect(20, 50, 350, 25));
		swap1.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		swap1.title = "Tickle me";
		swap1.target = this;
		swap1.action = "_swap";

		NSTextField text = new NSTextField(new NSRect(100, 200, 78, 25));
		text.editable = true;
		text.bezeled = true;
		text.stringValue = "Hello, Mono";
		text.delegate_ = textController;

		((NSView)window.contentView).addSubview(monoButton);
		((NSView)window.contentView).addSubview(swap1);
		((NSView)window.contentView).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.SharedApplication.runModalForWindow(window);
	}

	new public void init() {
		Console.WriteLine("init from .Net!");
	}

	[ObjCExport]
	public void _stop() {
		Console.WriteLine("Cool ass SHIT!");
		NSApplication.SharedApplication.stopModal();
	}

	[ObjCExport]
	public void _swap() {
		swap1.title = "I got tickled @ " + DateTime.Now;
	}
}
