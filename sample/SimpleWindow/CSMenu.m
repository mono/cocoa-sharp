//
//  CSMenu.m
//  SimpleWindow
//
//  Created by Adhamh Findlay on Sun Jun 13 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSMenu.h"

@implementation CSMenu

- (void)createFlashyMenu {
    // Since IB is slightly behind the current state of the art of menu capabilities, we need to create some of the fancier examples by hand.
    NSMenu *newMenu;
    NSMenuItem *newItem;
	
    // Add the submenu
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Mono Menu Item" action:NULL keyEquivalent:@""];
    newMenu = [[NSMenu allocWithZone:[NSMenu menuZone]] initWithTitle:@"Mono Menu"];
	[newMenu setAutoenablesItems: YES];
    [newItem setSubmenu:newMenu];
    [newMenu release];
    [[NSApp mainMenu] addItem:newItem];
    [newItem release];
	
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
    
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Quit" action:NULL keyEquivalent:@"q"];
	[newItem setKeyEquivalentModifierMask: NSCommandKeyMask];

	//CSControl *control = [[CSControl alloc] init];
    [newItem setTarget: [CSControl class]];
    [newItem setAction:@selector(stop)];
	//[control release];
	
    [newMenu addItem:newItem];
    [newItem release];
}

@end
