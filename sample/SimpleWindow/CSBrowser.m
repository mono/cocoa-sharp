//
//  CSBrowser.m
//  SimpleWindow
//
//  Created by Adhamh Findlay on Mon Jun 14 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSBrowser.h"


@implementation CSBrowser

/*- (void)createWindowMenu {
    // Since IB is slightly behind the current state of the art of menu capabilities, we need to create some of the fancier examples by hand.
    NSMenu *newMenu;
    NSMenuItem *newItem;
	
    // Add the submenu
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Mono Menu Item" action:NULL keyEquivalent:@"w"];
    newMenu = [[NSMenu allocWithZone:[NSMenu menuZone]] initWithTitle:@"Window"];
	[newItem setEnabled:YES]; 

	//have to do this to get the menu to appear.
	[[NSApp mainMenu] addItem:newItem];
	[newMenu setAutoenablesItems: NO];
    [newItem setSubmenu:newMenu];
    [newMenu release];
    [newItem release];
	
    newItem = [[NSMenuItem allocWithZone:[NSMenu menuZone]] initWithTitle:@"Close" action:NULL keyEquivalent:@"w"];
	[newItem setKeyEquivalentModifierMask: NSCommandKeyMask];
	
	//CSControl *control = [[CSControl alloc] init];
    [newItem setTarget: [NSApp mainMenu]];
    [newItem setAction:@selector(orderFront:)];
	//[control release];
	
    [newMenu addItem:newItem];
    [newItem release];
}*/

@end
