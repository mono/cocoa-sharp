using System;

using Apple.Foundation;
using Apple.AppKit;

class Test
{
    const int NSBorderlessWindowMask		= 0;
    const int NSTitledWindowMask		= 1 << 0;
    const int NSClosableWindowMask		= 1 << 1;
    const int NSMiniaturizableWindowMask	= 1 << 2;
    const int NSResizableWindowMask		= 1 << 3;

    const int NSBackingStoreBuffered	 = 2;


	static void Main(string[] args)
	{
		NSApplication.sharedApplication().setApplicationIconImage(NSImage.imageNamed("mono.icns"));

		NSRect contentRect = new NSRect(200, 180, 300, 300);

		NSWindow window = new NSWindow();
		window.initWithContentRect_styleMask_backing_defer(contentRect, 
			NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
			NSBackingStoreBuffered, false);
		window.Title = "Hi Mono";

// Drawer 1
		NSView drawer1View = new NSView();
		drawer1View.initWithFrame(new NSRect(50,50,50,50));

		NSDrawer monoDrawer1 = new NSDrawer();
		monoDrawer1.initWithContentSize_preferredEdge(new NSSize(200, 100) , NSRectEdge.NSMinXEdge);
		monoDrawer1.ParentWindow = window;
		monoDrawer1.ContentView = drawer1View;
		
		
		NSButton closedrawer1Button = new NSButton();
		closedrawer1Button.initWithFrame(new NSRect(100, 20, 84, 25));
		closedrawer1Button.Title = "Close D";
		closedrawer1Button.Target = monoDrawer1;
		closedrawer1Button.Action = "close";
		closedrawer1Button.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		
		drawer1View.addSubview(closedrawer1Button);
// End Drawer 1
// Drawer 2
		NSView drawer2View = new NSView();
		drawer2View.initWithFrame(new NSRect(50,50,50,50));

		NSDrawer monoDrawer2 = new NSDrawer();
		monoDrawer2.initWithContentSize_preferredEdge(new NSSize(200, 100) , NSRectEdge.NSMaxXEdge);
		monoDrawer2.ParentWindow = window;
		monoDrawer2.ContentView = drawer2View;
		
		NSButton closedrawer2Button = new NSButton();
		closedrawer2Button.initWithFrame(new NSRect(100, 20, 84, 25));
		closedrawer2Button.Title = "Close D";
		closedrawer2Button.Target = monoDrawer2;
		closedrawer2Button.Action = "close";
		closedrawer2Button.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;
		
		drawer2View.addSubview(closedrawer2Button);
// End Drawer 2

		NSButton monoButton = new NSButton();
		monoButton.initWithFrame(new NSRect(20, 20, 74, 25));
		monoButton.Title = "Dismiss";
		monoButton.Target = NSApplication.sharedApplication();
		monoButton.Action = "stopModal";
		monoButton.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSButton drawer1Button = new NSButton();
		drawer1Button.initWithFrame(new NSRect(100, 20, 84, 25));
		drawer1Button.Title = "Open D 1";
		drawer1Button.Target = monoDrawer1;
		drawer1Button.Action = "open";
		drawer1Button.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSButton drawer2Button = new NSButton();
		drawer2Button.initWithFrame(new NSRect(190, 20, 84, 25));
		drawer2Button.Title = "Open D 2";
		drawer2Button.Target = monoDrawer2;
		drawer2Button.Action = "open";
		drawer2Button.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;

		NSTextField text = new NSTextField();
		text.initWithFrame(new NSRect(100, 200, 78, 20));
		text.Editable = true;
		text.Bezeled = true;
		text.StringValue = "Hello, Mono";

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(drawer1Button);
		((NSView)window.contentView()).addSubview(drawer2Button);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.sharedApplication().runModalForWindow(window);
	}
}
