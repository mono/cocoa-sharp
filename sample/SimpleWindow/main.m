#import <Cocoa/Cocoa.h>
#import "Utils.h"
#import "CSWindow.h"

void CSApplicationMain() {
	[NSApplication sharedApplication];
	[NSBundle allFrameworks];
	[NSApp run];
}

int main (int argc, const char * argv[]) {
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	Utils *utils = [[Utils alloc] init];

	[NSApplication sharedApplication];

	NSRect contentRect = NSMakeRect(200, 180, 300, 300);
	
	CSWindow *window = [[CSWindow alloc] initWithContentRect: contentRect styleMask: NSWindowDocumentIconButton | NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask backing:NSBackingStoreBuffered defer:NO];
	[window makeMainWindow];
	[window makeKeyAndOrderFront:window];
	//[window center];

	[window setTitle: @"Hi Mono"];
	
	NSButton *monoButton = [[[NSButton alloc] initWithFrame: NSMakeRect(100,20,100,30)] autorelease];
	[monoButton setButtonType: NSToggleButton];
	[monoButton setBezelStyle: NSRoundedBezelStyle];
	[monoButton setEnabled: YES ];
	[monoButton setState: NSOnState];
	[monoButton setFont: [NSFont fontWithName: @"Geneva" size: 10]];
	
	[monoButton setTitle: @"Dismiss"];
	[monoButton setTarget: window];

	[monoButton setAction: @selector(stop)];
	
	NSTextField *text = [[NSTextField alloc] initWithFrame: NSMakeRect(100.0, 200.0, 100.0, 40)];
	[text setBezelStyle: 1];
	[text setEditable: NO];
	[text setBezeled: YES];
	[text setStringValue: @"Hello, Mono"];
	
	[[window contentView] addSubview: monoButton];
	[[window contentView] addSubview: text];

	[monoButton release];

	[window orderFrontRegardless];
	[NSApp run];

//	[pool release];
}

void stopRunLoop(id sender)
{
	[[NSRunLoop currentRunLoop] runUntilDate: [NSDate distantPast]];
	return;
}


/*
int main (int argc, const char * argv[]) {
	NSApplication *app = [NSApplication sharedApplication];
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	NSRunLoop *run = [NSRunLoop currentRunLoop];
	
	NSRect contentRect = NSMakeRect(200, 180, 300, 300);
	
	NSWindow *window = [[NSWindow alloc] initWithContentRect:contentRect styleMask: NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask backing:NSBackingStoreBuffered defer:NO];
	[window setTitle: @"Hi Mono"];
	
	NSTextField *text = [[NSTextField alloc] initWithFrame: NSMakeRect(100.0, 200.0, 100.0, 40)];
	NSButton *monoButton = [[[NSButton alloc] initWithFrame: NSMakeRect(100,20,100,30)] autorelease];

	[monoButton setButtonType: NSMomentaryPushButton];
	[monoButton setTitle: @"Dismiss"];

	[monoButton setTarget: app];
//	[monoButton setAction: @selector(runUntilDate:)];
	[monoButton setAction: @selector(stopModal)];
	[monoButton setBezelStyle: 1];

	[text setEditable: NO];
	[text setBezelStyle: 1];
	[text setStringValue: @"Hello, Mono"];
	
	[[window contentView] addSubview: monoButton];
	[[window contentView] addSubview: text];
	[window center];
	//[window makeKeyAndOrderFront:nil];
	[window orderFront:nil];
	
	
	[[NSApplication sharedApplication] runModalForWindow: window];
	//[monoButton release];
	[pool release];
	[app run];
	return 0;
}
*/

/*
 int main(int argc, const char *argv[])
 {
	 NSApplication *NSApp = [NSApplication sharedApplication];
	 NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	 NSButton *button = [[[NSButton alloc] initWithFrame:NSMakeRect(100,20,100,30)] autorelease];
	 NSTextField *textField  = [[[NSTextField alloc] initWithFrame:NSMakeRect(50,100,200,20)] autorelease];
	 NSWindow *mainWindow = [[NSWindow alloc] 
       initWithContentRect:NSMakeRect(100,100,300,160) 
				 styleMask:NSClosableWindowMask | NSTitledWindowMask 
				   backing:NSBackingStoreBuffered defer:false];
	 
	 //Block *motherBlock = [[@"[:textField| [textField setStringValue:NSDate 
	//	 now printString]]" asBlock] retain];
	 //Block *printDate = [[motherBlock value:textField] retain];
	 NSString *printDate = @"foo";

	 [[mainWindow contentView] addSubview:button];
	 [[mainWindow contentView] addSubview:textField];
	 [button setBezelStyle:1];
	 
	 [button setTarget:printDate];
	 [button setAction:@selector(value:)];
	 
	 [mainWindow orderFront:nil];
	 [pool release];
	 [NSApp run];
	 return 0;
 }
 */
 