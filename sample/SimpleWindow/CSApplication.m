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
	[NSApp setApplicationIconImage: [NSImage imageNamed: @"mono.icns"]];

	//setup our menus.
	[NSApp setMainMenu: [[NSMenu alloc] initWithTitle:@"MainMenu"]];
	//setAppleMenu is a bad title as it sets the menu to the right of the apple menu..
	[NSApp setAppleMenu: [CSMenu createAppleMenu]];	
	//set some of the buildin behavior this way.
	[NSApp setWindowsMenu: [CSMenu createWindowMenu]];
	
	//create an NSRect and use it to create an CSWindow
	NSRect contentRect = NSMakeRect(200, 180, 300, 300);
	//CSWindow is just a subclass of NSWindow.  subclasses of NSWindow are normal.
	CSWindow *window = [[CSWindow alloc] initWithContentRect: contentRect styleMask: NSWindowDocumentIconButton | NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask backing:NSBackingStoreBuffered defer:NO];
	[window setTitle: @"Mono Window"];
	//make window the main window for the application
	[window center];
	[window makeMainWindow];
	[window makeKeyAndOrderFront: window];
	
	
	//subviews were added in [control displayWindow], but this resulted
	//in the window not drawing properly when launched from terminal
	//moving the calls here fixed that issue.  not understood why.
	CSControl *control = [[CSControl alloc]init];
	[[window contentView] addSubview: [control displayQuitButton]];
	[[window contentView] addSubview: [control displayTextField]];
	[[window contentView] addSubview: [control displayBrowserButton]];
	//[control displayApplicationMenu];

	//run the application 
	[NSApp run];
	[pool release];	
}

/*
 - (void)applicationWillFinishLaunching:(NSNotification *)notification {
	 // The creation of new menus for the menu bar should be done in applicationWillFinishLaunching.
	 [self createFlashyMenu];
	 //[self createTrickyMenu];
	 //[self createPopUps];
	 //currentRadioSetting = Radio1Tag;
	 //currentSwitch1Setting = NO;
	 //currentSwitch2Setting = NO;
 } */

@end
