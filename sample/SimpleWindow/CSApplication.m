//
//  Cocoa#.m
//  SimpleWindow
//
//  Created by adhamh on Thu Jun 10 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//
#import <Cocoa/Cocoa.h>
#import "CSApplication.h"
#import "CSControl.h"

@implementation CSApplication

+(void) csApplicationMain
{
	//This is basically an implementation of NSApplicationMain.  The main difference
	//is that it does not call loadNibNamed because when creating the bridge, we don't
	//want to hide behind NIBs and NSApplicationMain.
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc]init];
	[NSApplication sharedApplication];
	[NSApp setApplicationIconImage: [NSImage imageNamed: @"iTools.tiff"]];

	[NSApp setMainMenu: [[NSMenu alloc] initWithTitle:@"MainMenu"]];
	
	//create an NSRect and use it to create an CSWindow
	NSRect contentRect = NSMakeRect(200, 180, 300, 300);
	//CSWindow is just a subclass of NSWindow.  subclasses of NSWindow are normal.
	CSWindow *window = [[CSWindow alloc] initWithContentRect: contentRect styleMask: NSWindowDocumentIconButton | NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask backing:NSBackingStoreBuffered defer:NO];
	//make window the main window for the application
	[window makeMainWindow];
	
	[NSMenu setMenuBarVisible:YES];

	
	//Here is where custom code is implemented to draw the first window, or whatever
	CSControl *control = [[CSControl alloc]init];
	[control displayWindow];

	[NSApp run];
	[pool release];	
}
@end
