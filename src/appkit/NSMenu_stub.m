/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

#include <Foundation/NSGeometry.h>

#include <AppKit/AppKitDefines.h>

#include <AppKit/NSMenuItem.h>

void NSMenu_setMenuZone (NSMenu* THIS, NSZone * aZone) {
	NSLog(@"NSMenu_setMenuZone");
	[THIS setMenuZone:aZone];
}

NSZone * NSMenu_menuZone (NSMenu* THIS) {
	NSLog(@"NSMenu_menuZone");
	return [THIS menuZone];
}
void NSMenu_popUpContextMenu_withEvent_forView (NSMenu* THIS, NSMenu* menu, NSEvent* event, NSView* view) {
	NSLog(@"NSMenu_popUpContextMenu_withEvent_forView");
	[THIS popUpContextMenu:menu withEvent:event forView:view];
}

void NSMenu_popUpContextMenu_withEvent_forView_withFont (NSMenu* THIS, NSMenu* menu, NSEvent* event, NSView* view, NSFont* font) {
	NSLog(@"NSMenu_popUpContextMenu_withEvent_forView_withFont");
	[THIS popUpContextMenu:menu withEvent:event forView:view withFont:font];
}

void NSMenu_setMenuBarVisible (NSMenu* THIS, BOOL visible) {
	NSLog(@"NSMenu_setMenuBarVisible");
	[THIS setMenuBarVisible:visible];
}

BOOL NSMenu_menuBarVisible (NSMenu* THIS) {
	NSLog(@"NSMenu_menuBarVisible");
	return [THIS menuBarVisible];
}
NSMenu * NSMenu_initWithTitle (NSMenu* THIS, NSString * aTitle) {
	NSLog(@"NSMenu_initWithTitle");
	return [THIS initWithTitle:aTitle];
}

void NSMenu_setTitle (NSMenu* THIS, NSString * aString) {
	NSLog(@"NSMenu_setTitle");
	[THIS setTitle:aString];
}

NSString * NSMenu_title (NSMenu* THIS) {
	NSLog(@"NSMenu_title");
	return [THIS title];
}
void NSMenu_setSupermenu (NSMenu* THIS, NSMenu * supermenu) {
	NSLog(@"NSMenu_setSupermenu");
	[THIS setSupermenu:supermenu];
}

NSMenu * NSMenu_supermenu (NSMenu* THIS) {
	NSLog(@"NSMenu_supermenu");
	return [THIS supermenu];
}
/* UNSUPPORTED: 
- (void)insertItem:(id <NSMenuItem>)newItem atIndex:(int)index;
 */

/* UNSUPPORTED: 
- (void)addItem:(id <NSMenuItem>)newItem;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)insertItemWithTitle:(NSString *)aString action:(SEL)aSelector keyEquivalent:(NSString *)charCode atIndex:(int)index;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)addItemWithTitle:(NSString *)aString action:(SEL)aSelector keyEquivalent:(NSString *)charCode;
 */

void NSMenu_removeItemAtIndex (NSMenu* THIS, int index) {
	NSLog(@"NSMenu_removeItemAtIndex");
	[THIS removeItemAtIndex:index];
}

/* UNSUPPORTED: 
- (void)removeItem:(id <NSMenuItem>)item;
 */

/* UNSUPPORTED: 
- (void)setSubmenu:(NSMenu *)aMenu forItem:(id <NSMenuItem>)anItem;
 */

NSArray * NSMenu_itemArray (NSMenu* THIS) {
	NSLog(@"NSMenu_itemArray");
	return [THIS itemArray];
}
int NSMenu_numberOfItems (NSMenu* THIS) {
	NSLog(@"NSMenu_numberOfItems");
	return [THIS numberOfItems];
}
/* UNSUPPORTED: 
- (int)indexOfItem:(id <NSMenuItem>)index;
 */

int NSMenu_indexOfItemWithTitle (NSMenu* THIS, NSString * aTitle) {
	NSLog(@"NSMenu_indexOfItemWithTitle");
	return [THIS indexOfItemWithTitle:aTitle];
}

int NSMenu_indexOfItemWithTag (NSMenu* THIS, int aTag) {
	NSLog(@"NSMenu_indexOfItemWithTag");
	return [THIS indexOfItemWithTag:aTag];
}

int NSMenu_indexOfItemWithRepresentedObject (NSMenu* THIS, NSMenu * object) {
	NSLog(@"NSMenu_indexOfItemWithRepresentedObject");
	return [THIS indexOfItemWithRepresentedObject:object];
}

int NSMenu_indexOfItemWithSubmenu (NSMenu* THIS, NSMenu * submenu) {
	NSLog(@"NSMenu_indexOfItemWithSubmenu");
	return [THIS indexOfItemWithSubmenu:submenu];
}

int NSMenu_indexOfItemWithTarget_andAction (NSMenu* THIS, NSMenu * target, SEL actionSelector) {
	NSLog(@"NSMenu_indexOfItemWithTarget_andAction");
	return [THIS indexOfItemWithTarget:target andAction:actionSelector];
}

/* UNSUPPORTED: 
- (id <NSMenuItem>)itemAtIndex:(int)index;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)itemWithTitle:(NSString *)aTitle;
 */

/* UNSUPPORTED: 
- (id <NSMenuItem>)itemWithTag:(int)tag;
 */

void NSMenu_setAutoenablesItems (NSMenu* THIS, BOOL flag) {
	NSLog(@"NSMenu_setAutoenablesItems");
	[THIS setAutoenablesItems:flag];
}

BOOL NSMenu_autoenablesItems (NSMenu* THIS) {
	NSLog(@"NSMenu_autoenablesItems");
	return [THIS autoenablesItems];
}
BOOL NSMenu_performKeyEquivalent (NSMenu* THIS, NSEvent * theEvent) {
	NSLog(@"NSMenu_performKeyEquivalent");
	return [THIS performKeyEquivalent:theEvent];
}

void NSMenu_update (NSMenu* THIS) {
	NSLog(@"NSMenu_update");
	[THIS update];
}
void NSMenu_setMenuChangedMessagesEnabled (NSMenu* THIS, BOOL flag) {
	NSLog(@"NSMenu_setMenuChangedMessagesEnabled");
	[THIS setMenuChangedMessagesEnabled:flag];
}

BOOL NSMenu_menuChangedMessagesEnabled (NSMenu* THIS) {
	NSLog(@"NSMenu_menuChangedMessagesEnabled");
	return [THIS menuChangedMessagesEnabled];
}
/* UNSUPPORTED: 
- (void)itemChanged:(id <NSMenuItem>)item;
 */

void NSMenu_helpRequested (NSMenu* THIS, NSEvent * eventPtr) {
	NSLog(@"NSMenu_helpRequested");
	[THIS helpRequested:eventPtr];
}

void NSMenu_setMenuRepresentation (NSMenu* THIS, NSMenu * menuRep) {
	NSLog(@"NSMenu_setMenuRepresentation");
	[THIS setMenuRepresentation:menuRep];
}

NSMenu * NSMenu_menuRepresentation (NSMenu* THIS) {
	NSLog(@"NSMenu_menuRepresentation");
	return [THIS menuRepresentation];
}
void NSMenu_setContextMenuRepresentation (NSMenu* THIS, NSMenu * menuRep) {
	NSLog(@"NSMenu_setContextMenuRepresentation");
	[THIS setContextMenuRepresentation:menuRep];
}

NSMenu * NSMenu_contextMenuRepresentation (NSMenu* THIS) {
	NSLog(@"NSMenu_contextMenuRepresentation");
	return [THIS contextMenuRepresentation];
}
void NSMenu_setTearOffMenuRepresentation (NSMenu* THIS, NSMenu * menuRep) {
	NSLog(@"NSMenu_setTearOffMenuRepresentation");
	[THIS setTearOffMenuRepresentation:menuRep];
}

NSMenu * NSMenu_tearOffMenuRepresentation (NSMenu* THIS) {
	NSLog(@"NSMenu_tearOffMenuRepresentation");
	return [THIS tearOffMenuRepresentation];
}
BOOL NSMenu_isTornOff (NSMenu* THIS) {
	NSLog(@"NSMenu_isTornOff");
	return [THIS isTornOff];
}
NSMenu * NSMenu_attachedMenu (NSMenu* THIS) {
	NSLog(@"NSMenu_attachedMenu");
	return [THIS attachedMenu];
}
BOOL NSMenu_isAttached (NSMenu* THIS) {
	NSLog(@"NSMenu_isAttached");
	return [THIS isAttached];
}
void NSMenu_sizeToFit (NSMenu* THIS) {
	NSLog(@"NSMenu_sizeToFit");
	[THIS sizeToFit];
}
NSPoint NSMenu_locationForSubmenu (NSMenu* THIS, NSMenu * aSubmenu) {
	NSLog(@"NSMenu_locationForSubmenu");
	return [THIS locationForSubmenu:aSubmenu];
}

void NSMenu_performActionForItemAtIndex (NSMenu* THIS, int index) {
	NSLog(@"NSMenu_performActionForItemAtIndex");
	[THIS performActionForItemAtIndex:index];
}

void NSMenu_setDelegate (NSMenu* THIS, NSMenu * anObject) {
	NSLog(@"NSMenu_setDelegate");
	[THIS setDelegate:anObject];
}

NSMenu * NSMenu_delegate (NSMenu* THIS) {
	NSLog(@"NSMenu_delegate");
	return [THIS delegate];
}
void NSMenu_submenuAction (NSMenu* THIS, NSMenu * sender) {
	NSLog(@"NSMenu_submenuAction");
	[THIS submenuAction:sender];
}

/* UNSUPPORTED: 
- (BOOL)validateMenuItem:(id <NSMenuItem>)menuItem;
 */

void NSMenu_menuNeedsUpdate (NSMenu* THIS, NSMenu* menu) {
	NSLog(@"NSMenu_menuNeedsUpdate");
	[THIS menuNeedsUpdate:menu];
}

int NSMenu_numberOfItemsInMenu (NSMenu* THIS, NSMenu* menu) {
	NSLog(@"NSMenu_numberOfItemsInMenu");
	return [THIS numberOfItemsInMenu:menu];
}

BOOL NSMenu_menu_updateItem_atIndex_shouldCancel (NSMenu* THIS, NSMenu* menu, NSMenuItem* item, int index, BOOL shouldCancel) {
	NSLog(@"NSMenu_menu_updateItem_atIndex_shouldCancel");
	return [THIS menu:menu updateItem:item atIndex:index shouldCancel:shouldCancel];
}

BOOL NSMenu_menuHasKeyEquivalent_forEvent_target_action (NSMenu* THIS, NSMenu* menu, NSEvent* event, NSMenu ** target, SEL* action) {
	NSLog(@"NSMenu_menuHasKeyEquivalent_forEvent_target_action");
	return [THIS menuHasKeyEquivalent:menu forEvent:event target:target action:action];
}

NSMenu * NSMenu_alloc() {
	NSLog(@"NSMenu_alloc()");
	return [NSMenu alloc];
}
