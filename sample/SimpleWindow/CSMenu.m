//
//  CSMenu.m
//  SimpleWindow
//
//  Created by Adhamh Findlay on Sun Jun 13 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSMenu.h"

@implementation CSMenu

//probably should refactor this to take titles, and equivalents as args.

//method title to match the really poorly title method setAppleMenu
+ (NSMenu *)createAppleMenu
{
    // Since IB is slightly behind the current state of the art of menu capabilities, we need to create some of the fancier examples by hand.
    NSMenu *newMenu;
    NSMenuItem *newItem;
	
    // Add the submenu
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Mono Menu Item" action:NULL keyEquivalent:@""];
	[newItem setEnabled:YES]; 
    newMenu = [[NSMenu allocWithZone:[NSMenu menuZone]] initWithTitle:@"SimpleWindow"];

	//have to do this to get the menu to appear.
	[[NSApp mainMenu] addItem:newItem];
	[newMenu setAutoenablesItems: NO];
    [newItem setSubmenu:newMenu];
    [newMenu release];
    [newItem release];
	
	newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Quit" action:NULL keyEquivalent:@"q"];
	[newItem setKeyEquivalentModifierMask: NSCommandKeyMask];
	
    [newItem setTarget: [CSControl class]];
    [newItem setAction: @selector(quit:)];
	
    [newMenu addItem:newItem];
    [newItem release];
	
	return newMenu;
    // Add some cool items
    /*newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Images Used" action:NULL keyEquivalent:@""];
    [newItem setImage:[NSImage imageNamed:@"eomt_browsedata"]];
    [newItem setTarget:self];
    [newItem setAction:@selector(noopAction:)];
    [newMenu addItem:newItem];
    [newItem release];
	
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"In This Menu" action:NULL keyEquivalent:@""];
    [newItem setImage:[NSImage imageNamed:@"eomt_copy"]];
    [newItem setTarget:self];
    [newItem setAction:@selector(noopAction:)];
    [newMenu addItem:newItem];
    [newItem release];
	
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Were Stolen" action:NULL keyEquivalent:@""];
    [newItem setTarget:self];
    [newItem setAction:@selector(noopAction:)];
    [newMenu addItem:newItem];
    [newItem release];
	
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"From EOModeler" action:NULL keyEquivalent:@""];
    [newItem setImage:[NSImage imageNamed:@"eomt_cut"]];
    [newItem setTarget:self];
    [newItem setAction:@selector(noopAction:)];
    [newMenu addItem:newItem];
    [newItem release];*/
    
}

+ (NSMenu *) createWindowMenu 
{
    NSMenu *newMenu;
    NSMenuItem *newItem;
	newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Mono Menu Item" action:NULL keyEquivalent:@""];
	[newItem setEnabled:YES]; 
    newMenu = [[NSMenu allocWithZone:[NSMenu menuZone]] initWithTitle:@"Window"];
	[[NSApp mainMenu] addItem:newItem];
	[newMenu setAutoenablesItems: NO];
    [newItem setSubmenu:newMenu];
    [newMenu release];
    [newItem release];
	newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Close" action:NULL keyEquivalent:@"w"];
	[newItem setKeyEquivalentModifierMask: NSCommandKeyMask];
	[newItem setTarget: [NSApp mainWindow]];
    [newItem setAction: @selector(orderOut:)];
    [newMenu addItem:newItem];
    [newItem release];
	return newMenu;
}	

- (void)stop
{
	//makeMainWindow above set the application's main window.  now we can orderFront: nill
	//which makes it disappear.
	[[NSApp mainWindow] orderOut:nil];	
	//this quits the application.
	[NSApp terminate: NSApp];
}

@end
