/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSButton.h>

#include <AppKit/NSMenuItemCell.h>

#include <AppKit/NSMenuItem.h>

NSPopUpButton * NSPopUpButton_initWithFrame_pullsDown (NSPopUpButton* THIS, NSRect buttonFrame, BOOL flag) {
	NSLog(@"NSPopUpButton_initWithFrame_pullsDown");
	return [THIS initWithFrame:buttonFrame pullsDown:flag];
}

void NSPopUpButton_setMenu (NSPopUpButton* THIS, NSMenu * menu) {
	NSLog(@"NSPopUpButton_setMenu");
	[THIS setMenu:menu];
}

NSMenu * NSPopUpButton_menu (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_menu");
	return [THIS menu];
}
void NSPopUpButton_setPullsDown (NSPopUpButton* THIS, BOOL flag) {
	NSLog(@"NSPopUpButton_setPullsDown");
	[THIS setPullsDown:flag];
}

BOOL NSPopUpButton_pullsDown (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_pullsDown");
	return [THIS pullsDown];
}
void NSPopUpButton_setAutoenablesItems (NSPopUpButton* THIS, BOOL flag) {
	NSLog(@"NSPopUpButton_setAutoenablesItems");
	[THIS setAutoenablesItems:flag];
}

BOOL NSPopUpButton_autoenablesItems (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_autoenablesItems");
	return [THIS autoenablesItems];
}
void NSPopUpButton_setPreferredEdge (NSPopUpButton* THIS, NSRectEdge edge) {
	NSLog(@"NSPopUpButton_setPreferredEdge");
	[THIS setPreferredEdge:edge];
}

NSRectEdge NSPopUpButton_preferredEdge (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_preferredEdge");
	return [THIS preferredEdge];
}
void NSPopUpButton_addItemWithTitle (NSPopUpButton* THIS, NSString * title) {
	NSLog(@"NSPopUpButton_addItemWithTitle");
	[THIS addItemWithTitle:title];
}

void NSPopUpButton_addItemsWithTitles (NSPopUpButton* THIS, NSArray * itemTitles) {
	NSLog(@"NSPopUpButton_addItemsWithTitles");
	[THIS addItemsWithTitles:itemTitles];
}

void NSPopUpButton_insertItemWithTitle_atIndex (NSPopUpButton* THIS, NSString * title, int index) {
	NSLog(@"NSPopUpButton_insertItemWithTitle_atIndex");
	[THIS insertItemWithTitle:title atIndex:index];
}

void NSPopUpButton_removeItemWithTitle (NSPopUpButton* THIS, NSString * title) {
	NSLog(@"NSPopUpButton_removeItemWithTitle");
	[THIS removeItemWithTitle:title];
}

void NSPopUpButton_removeItemAtIndex (NSPopUpButton* THIS, int index) {
	NSLog(@"NSPopUpButton_removeItemAtIndex");
	[THIS removeItemAtIndex:index];
}

void NSPopUpButton_removeAllItems (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_removeAllItems");
	[THIS removeAllItems];
}
NSArray * NSPopUpButton_itemArray (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_itemArray");
	return [THIS itemArray];
}
int NSPopUpButton_numberOfItems (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_numberOfItems");
	return [THIS numberOfItems];
}
/* UNSUPPORTED: 
- (int)indexOfItem:(id <NSMenuItem>)item;
 */

int NSPopUpButton_indexOfItemWithTitle (NSPopUpButton* THIS, NSString * title) {
	NSLog(@"NSPopUpButton_indexOfItemWithTitle");
	return [THIS indexOfItemWithTitle:title];
}

int NSPopUpButton_indexOfItemWithTag (NSPopUpButton* THIS, int tag) {
	NSLog(@"NSPopUpButton_indexOfItemWithTag");
	return [THIS indexOfItemWithTag:tag];
}

int NSPopUpButton_indexOfItemWithRepresentedObject (NSPopUpButton* THIS, NSPopUpButton * obj) {
	NSLog(@"NSPopUpButton_indexOfItemWithRepresentedObject");
	return [THIS indexOfItemWithRepresentedObject:obj];
}

int NSPopUpButton_indexOfItemWithTarget_andAction (NSPopUpButton* THIS, NSPopUpButton * target, SEL actionSelector) {
	NSLog(@"NSPopUpButton_indexOfItemWithTarget_andAction");
	return [THIS indexOfItemWithTarget:target andAction:actionSelector];
}

/* UNSUPPORTED: 
- (id <NSMenuItem>)itemAtIndex:(int)index;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)itemWithTitle:(NSString *)title;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)lastItem;
 */

/* UNSUPPORTED: 
- (void)selectItem:(id <NSMenuItem>)item;
 */

void NSPopUpButton_selectItemAtIndex (NSPopUpButton* THIS, int index) {
	NSLog(@"NSPopUpButton_selectItemAtIndex");
	[THIS selectItemAtIndex:index];
}

void NSPopUpButton_selectItemWithTitle (NSPopUpButton* THIS, NSString * title) {
	NSLog(@"NSPopUpButton_selectItemWithTitle");
	[THIS selectItemWithTitle:title];
}

void NSPopUpButton_setTitle (NSPopUpButton* THIS, NSString * aString) {
	NSLog(@"NSPopUpButton_setTitle");
	[THIS setTitle:aString];
}

/* UNSUPPORTED: 
- (id <NSMenuItem>)selectedItem;
 */

int NSPopUpButton_indexOfSelectedItem (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_indexOfSelectedItem");
	return [THIS indexOfSelectedItem];
}
void NSPopUpButton_synchronizeTitleAndSelectedItem (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_synchronizeTitleAndSelectedItem");
	[THIS synchronizeTitleAndSelectedItem];
}
NSString * NSPopUpButton_itemTitleAtIndex (NSPopUpButton* THIS, int index) {
	NSLog(@"NSPopUpButton_itemTitleAtIndex");
	return [THIS itemTitleAtIndex:index];
}

NSArray * NSPopUpButton_itemTitles (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_itemTitles");
	return [THIS itemTitles];
}
NSString * NSPopUpButton_titleOfSelectedItem (NSPopUpButton* THIS) {
	NSLog(@"NSPopUpButton_titleOfSelectedItem");
	return [THIS titleOfSelectedItem];
}
NSPopUpButton * NSPopUpButton_alloc() {
	NSLog(@"NSPopUpButton_alloc()");
	return [NSPopUpButton alloc];
}
