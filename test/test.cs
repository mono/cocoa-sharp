using System;

using Apple.Foundation;
using Apple.AppKit;

class Test
{
	static void Main(string[] args)
	{
		NSApplication.SharedApplication.applicationIconImage = (NSImage)NSImage.ImageNamed("applemono.icns");

		NSWindow window = new NSWindow(new NSRect(200, 180, 300, 300),
					       (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					       NSBackingStoreType.NSBackingStoreBuffered,
					       false);
		window.title = "Hi Mono";

// Drawer 1
		NSView drawer1View = new NSView(new NSRect(50,50,50,50));

		NSDrawer monoDrawer1 = new NSDrawer(new NSSize(200,100), NSRectEdge.NSMinXEdge);
		monoDrawer1.parentWindow = window;
		monoDrawer1.contentView = drawer1View;
		
		
		NSButton closedrawer1Button = new NSButton(new NSRect(100, 20, 84, 25));
		closedrawer1Button.title = "Close D";
		closedrawer1Button.target = monoDrawer1;
		closedrawer1Button.action = "close";
		closedrawer1Button.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		
		drawer1View.addSubview(closedrawer1Button);
// End Drawer 1
// Drawer 2
		NSView drawer2View = new NSView(new NSRect(50, 50, 50, 50));

		NSDrawer monoDrawer2 = new NSDrawer(new NSSize(200, 100), NSRectEdge.NSMaxXEdge);
		monoDrawer2.parentWindow = window;
		monoDrawer2.contentView = drawer2View;
		
		NSButton closedrawer2Button = new NSButton(new NSRect(100, 20, 84, 25));
		closedrawer2Button.title = "Close D";
		closedrawer2Button.target = monoDrawer2;
		closedrawer2Button.action = "close";
		closedrawer2Button.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		
		drawer2View.addSubview(closedrawer2Button);
// End Drawer 2

		NSButton monoButton = new NSButton(new NSRect(20, 20, 74, 25));
		monoButton.title = "Dismiss";
		monoButton.target = NSApplication.SharedApplication;
		monoButton.action = "stopModal";
		monoButton.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSButton drawer1Button = new NSButton(new NSRect(100, 20, 84, 25));
		drawer1Button.title = "Open D 1";
		drawer1Button.target = monoDrawer1;
		drawer1Button.action = "open";
		drawer1Button.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSButton drawer2Button = new NSButton(new NSRect(190, 20, 84, 25));
		drawer2Button.title = "Open D 2";
		drawer2Button.target = monoDrawer2;
		drawer2Button.action = "open";
		drawer2Button.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSTextField text = new NSTextField(new NSRect(100, 200, 78, 20));
		text.editable = true;
		text.bezeled = true;
		text.stringValue = "Hello, Mono";

		((NSView)window.contentView).addSubview(monoButton);
		((NSView)window.contentView).addSubview(drawer1Button);
		((NSView)window.contentView).addSubview(drawer2Button);
		((NSView)window.contentView).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.SharedApplication.runModalForWindow(window);
	}
}
