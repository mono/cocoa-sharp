#import <Cocoa/Cocoa.h>

BOOL _runLoopDone;

int main (int argc, const char * argv[]) {
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	[NSApplication sharedApplication];
	NSRect contentRect = NSMakeRect(200, 180, 300, 300);
	
	NSWindow *window = [[NSWindow alloc] initWithContentRect:contentRect styleMask: NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask backing:NSBackingStoreBuffered defer:NO];
	[window setTitle: @"Hi Mono"];
	
	NSButton *monoButton = [[NSButton alloc] initWithFrame: NSMakeRect(20.0, 20.0, 74.0, 17.0)];
	[monoButton setTitle: @"Dismiss"];
	[monoButton setTarget: [NSApplication sharedApplication]];
	[monoButton setAction: @selector(stopModalWithCode:)];
	
	NSTextField *text = [[NSTextField alloc] initWithFrame: NSMakeRect(100.0, 200.0, 78.0, 20)];
	[text setEditable: NO];
	[text setBezeled: YES];
	[text setStringValue: @"Hello, Mono"];
	
	[[window contentView] addSubview: monoButton];
	[[window contentView] addSubview: text];
	[window center];
	[window makeKeyAndOrderFront:nil];
	
	
	[[NSApplication sharedApplication] runModalForWindow: window];
	[monoButton release];
	[pool release];
	exit(0);
}
