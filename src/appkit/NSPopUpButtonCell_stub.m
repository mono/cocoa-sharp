/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSMenuItemCell.h>

#include <AppKit/NSMenuItem.h>

NSPopUpButtonCell * NSPopUpButtonCell_initTextCell_pullsDown (NSPopUpButtonCell* THIS, NSString * stringValue, BOOL pullDown) {
	NSLog(@"NSPopUpButtonCell_initTextCell_pullsDown");
	return [THIS initTextCell:stringValue pullsDown:pullDown];
}

void NSPopUpButtonCell_setMenu (NSPopUpButtonCell* THIS, NSMenu * menu) {
	NSLog(@"NSPopUpButtonCell_setMenu");
	[THIS setMenu:menu];
}

NSMenu * NSPopUpButtonCell_menu (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_menu");
	return [THIS menu];
}
void NSPopUpButtonCell_setPullsDown (NSPopUpButtonCell* THIS, BOOL flag) {
	NSLog(@"NSPopUpButtonCell_setPullsDown");
	[THIS setPullsDown:flag];
}

BOOL NSPopUpButtonCell_pullsDown (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_pullsDown");
	return [THIS pullsDown];
}
void NSPopUpButtonCell_setAutoenablesItems (NSPopUpButtonCell* THIS, BOOL flag) {
	NSLog(@"NSPopUpButtonCell_setAutoenablesItems");
	[THIS setAutoenablesItems:flag];
}

BOOL NSPopUpButtonCell_autoenablesItems (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_autoenablesItems");
	return [THIS autoenablesItems];
}
void NSPopUpButtonCell_setPreferredEdge (NSPopUpButtonCell* THIS, NSRectEdge edge) {
	NSLog(@"NSPopUpButtonCell_setPreferredEdge");
	[THIS setPreferredEdge:edge];
}

NSRectEdge NSPopUpButtonCell_preferredEdge (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_preferredEdge");
	return [THIS preferredEdge];
}
void NSPopUpButtonCell_setUsesItemFromMenu (NSPopUpButtonCell* THIS, BOOL flag) {
	NSLog(@"NSPopUpButtonCell_setUsesItemFromMenu");
	[THIS setUsesItemFromMenu:flag];
}

BOOL NSPopUpButtonCell_usesItemFromMenu (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_usesItemFromMenu");
	return [THIS usesItemFromMenu];
}
void NSPopUpButtonCell_setAltersStateOfSelectedItem (NSPopUpButtonCell* THIS, BOOL flag) {
	NSLog(@"NSPopUpButtonCell_setAltersStateOfSelectedItem");
	[THIS setAltersStateOfSelectedItem:flag];
}

BOOL NSPopUpButtonCell_altersStateOfSelectedItem (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_altersStateOfSelectedItem");
	return [THIS altersStateOfSelectedItem];
}
void NSPopUpButtonCell_addItemWithTitle (NSPopUpButtonCell* THIS, NSString * title) {
	NSLog(@"NSPopUpButtonCell_addItemWithTitle");
	[THIS addItemWithTitle:title];
}

void NSPopUpButtonCell_addItemsWithTitles (NSPopUpButtonCell* THIS, NSArray * itemTitles) {
	NSLog(@"NSPopUpButtonCell_addItemsWithTitles");
	[THIS addItemsWithTitles:itemTitles];
}

void NSPopUpButtonCell_insertItemWithTitle_atIndex (NSPopUpButtonCell* THIS, NSString * title, int index) {
	NSLog(@"NSPopUpButtonCell_insertItemWithTitle_atIndex");
	[THIS insertItemWithTitle:title atIndex:index];
}

void NSPopUpButtonCell_removeItemWithTitle (NSPopUpButtonCell* THIS, NSString * title) {
	NSLog(@"NSPopUpButtonCell_removeItemWithTitle");
	[THIS removeItemWithTitle:title];
}

void NSPopUpButtonCell_removeItemAtIndex (NSPopUpButtonCell* THIS, int index) {
	NSLog(@"NSPopUpButtonCell_removeItemAtIndex");
	[THIS removeItemAtIndex:index];
}

void NSPopUpButtonCell_removeAllItems (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_removeAllItems");
	[THIS removeAllItems];
}
NSArray * NSPopUpButtonCell_itemArray (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_itemArray");
	return [THIS itemArray];
}
int NSPopUpButtonCell_numberOfItems (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_numberOfItems");
	return [THIS numberOfItems];
}
/* UNSUPPORTED: 
- (int)indexOfItem:(id <NSMenuItem>)item;
 */

int NSPopUpButtonCell_indexOfItemWithTitle (NSPopUpButtonCell* THIS, NSString * title) {
	NSLog(@"NSPopUpButtonCell_indexOfItemWithTitle");
	return [THIS indexOfItemWithTitle:title];
}

int NSPopUpButtonCell_indexOfItemWithTag (NSPopUpButtonCell* THIS, int tag) {
	NSLog(@"NSPopUpButtonCell_indexOfItemWithTag");
	return [THIS indexOfItemWithTag:tag];
}

int NSPopUpButtonCell_indexOfItemWithRepresentedObject (NSPopUpButtonCell* THIS, NSPopUpButtonCell * obj) {
	NSLog(@"NSPopUpButtonCell_indexOfItemWithRepresentedObject");
	return [THIS indexOfItemWithRepresentedObject:obj];
}

int NSPopUpButtonCell_indexOfItemWithTarget_andAction (NSPopUpButtonCell* THIS, NSPopUpButtonCell * target, SEL actionSelector) {
	NSLog(@"NSPopUpButtonCell_indexOfItemWithTarget_andAction");
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

void NSPopUpButtonCell_selectItemAtIndex (NSPopUpButtonCell* THIS, int index) {
	NSLog(@"NSPopUpButtonCell_selectItemAtIndex");
	[THIS selectItemAtIndex:index];
}

void NSPopUpButtonCell_selectItemWithTitle (NSPopUpButtonCell* THIS, NSString * title) {
	NSLog(@"NSPopUpButtonCell_selectItemWithTitle");
	[THIS selectItemWithTitle:title];
}

void NSPopUpButtonCell_setTitle (NSPopUpButtonCell* THIS, NSString * aString) {
	NSLog(@"NSPopUpButtonCell_setTitle");
	[THIS setTitle:aString];
}

/* UNSUPPORTED: 
- (id <NSMenuItem>)selectedItem;
 */

int NSPopUpButtonCell_indexOfSelectedItem (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_indexOfSelectedItem");
	return [THIS indexOfSelectedItem];
}
void NSPopUpButtonCell_synchronizeTitleAndSelectedItem (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_synchronizeTitleAndSelectedItem");
	[THIS synchronizeTitleAndSelectedItem];
}
NSString * NSPopUpButtonCell_itemTitleAtIndex (NSPopUpButtonCell* THIS, int index) {
	NSLog(@"NSPopUpButtonCell_itemTitleAtIndex");
	return [THIS itemTitleAtIndex:index];
}

NSArray * NSPopUpButtonCell_itemTitles (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_itemTitles");
	return [THIS itemTitles];
}
NSString * NSPopUpButtonCell_titleOfSelectedItem (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_titleOfSelectedItem");
	return [THIS titleOfSelectedItem];
}
void NSPopUpButtonCell_attachPopUpWithFrame_inView (NSPopUpButtonCell* THIS, NSRect cellFrame, NSView * controlView) {
	NSLog(@"NSPopUpButtonCell_attachPopUpWithFrame_inView");
	[THIS attachPopUpWithFrame:cellFrame inView:controlView];
}

void NSPopUpButtonCell_dismissPopUp (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_dismissPopUp");
	[THIS dismissPopUp];
}
void NSPopUpButtonCell_performClickWithFrame_inView (NSPopUpButtonCell* THIS, NSRect frame, NSView * controlView) {
	NSLog(@"NSPopUpButtonCell_performClickWithFrame_inView");
	[THIS performClickWithFrame:frame inView:controlView];
}

NSPopUpArrowPosition NSPopUpButtonCell_arrowPosition (NSPopUpButtonCell* THIS) {
	NSLog(@"NSPopUpButtonCell_arrowPosition");
	return [THIS arrowPosition];
}
void NSPopUpButtonCell_setArrowPosition (NSPopUpButtonCell* THIS, NSPopUpArrowPosition position) {
	NSLog(@"NSPopUpButtonCell_setArrowPosition");
	[THIS setArrowPosition:position];
}

/* UNSUPPORTED: 
- (id <NSCopying>)objectValue;
 */

/* UNSUPPORTED: 
- (void)setObjectValue:(id <NSCopying>)obj;
 */

NSPopUpButtonCell * NSPopUpButtonCell_alloc() {
	NSLog(@"NSPopUpButtonCell_alloc()");
	return [NSPopUpButtonCell alloc];
}
