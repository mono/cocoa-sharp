using System;

using Apple.Cocoa.Foundation;

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
		NSAutoreleasePool pool = new NSAutoreleasePool();
		pool.init();

		NSApplication.sharedApplication();
		NSRect contentRect = new NSRect(200, 180, 300, 300);

		NSWindow window = new NSWindow();
		window.initWithContentRect_styleMask_backing_defer(contentRect, 
			NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
			NSBackingStoreBuffered, false);
		window.setTitle(new NSString("Hi Mono"));

		NSButton monoButton = new NSButton();
		monoButton.initWithFrame(new NSRect(20, 20, 74, 17));
		monoButton.setTitle(new NSString("Dismiss"));
		monoButton.setTarget(NSApplication.sharedApplication());
		//monoButton.setAction(@selector(stopModalWithCode:));

		NSTextField text = new NSTextField();
		text.initWithFrame(new NSRect(100, 200, 78, 20));
		text.setEditable(false);
		text.setBezeled(true);
		text.setStringValue(new NSString("Hello, Mono"));

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.sharedApplication().runModalForWindow(window);
		monoButton.release();
		pool.release();
	}
}
