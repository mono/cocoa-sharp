//
//  CSControl.m
//  SimpleWindow
//
//  Created by adhamh on Thu Jun 10 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSControl.h"


//  this is were you place methods that your interface will use, its the the C in MCV.

@implementation CSControl
- (void)displayWindow
{
	//
	CSWindow *window = [NSApp mainWindow];
	[window setTitle: @"Hi mono"];
	
	//create our button 
	NSButton *monoButton = [[[NSButton alloc] initWithFrame: NSMakeRect(100,20,100,30)] autorelease];
	[monoButton setButtonType: NSToggleButton];
	[monoButton setBezelStyle: NSRoundedBezelStyle];
	[monoButton setEnabled: YES ];
	[monoButton setState: NSOnState];
	[monoButton setFont: [NSFont fontWithName: @"Geneva" size: 10]];
	
	//make the button throb
	[monoButton setKeyEquivalent: @"\r"];
	
	//set the text of the button and its target, in this case self
	[monoButton setTitle: @"Dismiss"];
	[monoButton setTarget: self];
	//this will call the _stop method in self when the button is pressed.
	[monoButton setAction: @selector(_stop)];
	
	//set up a nice little text field that will display something.
	NSTextField *text = [[[NSTextField alloc] initWithFrame: NSMakeRect(100.0, 200.0, 100.0, 40)] autorelease];
	[text setBezelStyle: 1];
	[text setEditable: NO];
	[text setBezeled: YES];
	[text setStringValue: @"Hello, mono"];
		
	//NSButton and NSTextField are subclasses of NSView, use this to add them to the window
	[[window contentView] addSubview: monoButton];
	[[window contentView] addSubview: text];
	
	//make sure the window is centered on the screen.
	[window center];
	//no matter what, bring window to the front.
	[window orderFrontRegardless];
}

- (void)_stop
{
	//makeMainWindow above set the application's main window.  now we can orderFront: nill
	//which makes it disappear.
	[[NSApp mainWindow] orderFront:nil];
	
	//this quits the application.
	[NSApp terminate: NSApp];
}

@end
