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
		NSApplication.sharedApplication().setApplicationIconImage(NSImage.imageNamed(new NSString("mono.icns")));

		NSRect contentRect = new NSRect(200, 180, 300, 300);

		NSWindow window = new NSWindow();
		window.initWithContentRect_styleMask_backing_defer(contentRect, 
			NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
			NSBackingStoreBuffered, false);
		window.setTitle(new NSString("Hi Mono"));

// Drawer 1
		NSView drawer1View = new NSView();
		drawer1View.initWithFrame(new NSRect(50,50,50,50));

		NSDrawer monoDrawer1 = new NSDrawer();
		monoDrawer1.initWithContentSize_preferredEdge(new NSSize(200, 100) , NSRectEdge.NSMinXEdge);
		monoDrawer1.init();
		monoDrawer1.setParentWindow(window);
		monoDrawer1.setContentView(drawer1View);
		
		
		NSButton closedrawer1Button = new NSButton();
		closedrawer1Button.initWithFrame(new NSRect(100, 20, 84, 25));
		closedrawer1Button.setTitle(new NSString("Close D"));
		closedrawer1Button.setTarget(monoDrawer1);
		closedrawer1Button.setAction(NSString.NSSelector("close"));
		closedrawer1Button.setBezelStyle(NSBezelStyle.NSRoundedBezelStyle);
		
		drawer1View.addSubview(closedrawer1Button);
// End Drawer 1
// Drawer 2
		NSView drawer2View = new NSView();
		drawer2View.initWithFrame(new NSRect(50,50,50,50));

		NSDrawer monoDrawer2 = new NSDrawer();
		monoDrawer2.initWithContentSize_preferredEdge(new NSSize(200, 100) , NSRectEdge.NSMaxXEdge);
		monoDrawer2.init();
		monoDrawer2.setParentWindow(window);
		monoDrawer2.setContentView(drawer2View);
		
		
		NSButton closedrawer2Button = new NSButton();
		closedrawer2Button.initWithFrame(new NSRect(100, 20, 84, 25));
		closedrawer2Button.setTitle(new NSString("Close D"));
		closedrawer2Button.setTarget(monoDrawer2);
		closedrawer2Button.setAction(NSString.NSSelector("close"));
		closedrawer2Button.setBezelStyle(NSBezelStyle.NSRoundedBezelStyle);
		
		drawer2View.addSubview(closedrawer2Button);
// End Drawer 2

		NSButton monoButton = new NSButton();
		monoButton.initWithFrame(new NSRect(20, 20, 74, 25));
		monoButton.setTitle(new NSString("Dismiss"));
		monoButton.setTarget(NSApplication.sharedApplication());
		monoButton.setAction(NSString.NSSelector("stopModal"));
		monoButton.setBezelStyle(NSBezelStyle.NSRoundedBezelStyle);

		NSButton drawer1Button = new NSButton();
		drawer1Button.initWithFrame(new NSRect(100, 20, 84, 25));
		drawer1Button.setTitle(new NSString("Open D 1"));
		drawer1Button.setTarget(monoDrawer1);
		drawer1Button.setAction(NSString.NSSelector("open"));
		drawer1Button.setBezelStyle(NSBezelStyle.NSRoundedBezelStyle);

		NSButton drawer2Button = new NSButton();
		drawer2Button.initWithFrame(new NSRect(190, 20, 84, 25));
		drawer2Button.setTitle(new NSString("Open D 2"));
		drawer2Button.setTarget(monoDrawer2);
		drawer2Button.setAction(NSString.NSSelector("open"));
		drawer2Button.setBezelStyle(NSBezelStyle.NSRoundedBezelStyle);

		NSTextField text = new NSTextField();
		text.initWithFrame(new NSRect(100, 200, 78, 20));
		text.setEditable(true);
		text.setBezeled(true);
		text.setStringValue(new NSString("Hello, Mono"));

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(drawer1Button);
		((NSView)window.contentView()).addSubview(drawer2Button);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.sharedApplication().runModalForWindow(window);

		monoButton.release();
	}
}
