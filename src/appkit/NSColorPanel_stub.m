/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSPanel.h>

#include <AppKit/NSApplication.h>

NSColorPanel * NSColorPanel_sharedColorPanel (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_sharedColorPanel");
	return [THIS sharedColorPanel];
}
BOOL NSColorPanel_sharedColorPanelExists (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_sharedColorPanelExists");
	return [THIS sharedColorPanelExists];
}
BOOL NSColorPanel_dragColor_withEvent_fromView (NSColorPanel* THIS, NSColor * color, NSEvent * theEvent, NSView * sourceView) {
	NSLog(@"NSColorPanel_dragColor_withEvent_fromView");
	return [THIS dragColor:color withEvent:theEvent fromView:sourceView];
}

void NSColorPanel_setPickerMask (NSColorPanel* THIS, int mask) {
	NSLog(@"NSColorPanel_setPickerMask");
	[THIS setPickerMask:mask];
}

void NSColorPanel_setPickerMode (NSColorPanel* THIS, int mode) {
	NSLog(@"NSColorPanel_setPickerMode");
	[THIS setPickerMode:mode];
}

void NSColorPanel_setAccessoryView (NSColorPanel* THIS, NSView * aView) {
	NSLog(@"NSColorPanel_setAccessoryView");
	[THIS setAccessoryView:aView];
}

NSView * NSColorPanel_accessoryView (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_accessoryView");
	return [THIS accessoryView];
}
void NSColorPanel_setContinuous (NSColorPanel* THIS, BOOL flag) {
	NSLog(@"NSColorPanel_setContinuous");
	[THIS setContinuous:flag];
}

BOOL NSColorPanel_isContinuous (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_isContinuous");
	return [THIS isContinuous];
}
void NSColorPanel_setShowsAlpha (NSColorPanel* THIS, BOOL flag) {
	NSLog(@"NSColorPanel_setShowsAlpha");
	[THIS setShowsAlpha:flag];
}

BOOL NSColorPanel_showsAlpha (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_showsAlpha");
	return [THIS showsAlpha];
}
void NSColorPanel_setMode (NSColorPanel* THIS, int mode) {
	NSLog(@"NSColorPanel_setMode");
	[THIS setMode:mode];
}

int NSColorPanel_mode (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_mode");
	return [THIS mode];
}
void NSColorPanel_setColor (NSColorPanel* THIS, NSColor * color) {
	NSLog(@"NSColorPanel_setColor");
	[THIS setColor:color];
}

NSColor * NSColorPanel_color (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_color");
	return [THIS color];
}
float NSColorPanel_alpha (NSColorPanel* THIS) {
	NSLog(@"NSColorPanel_alpha");
	return [THIS alpha];
}
void NSColorPanel_setAction (NSColorPanel* THIS, SEL aSelector) {
	NSLog(@"NSColorPanel_setAction");
	[THIS setAction:aSelector];
}

void NSColorPanel_setTarget (NSColorPanel* THIS, NSColorPanel * anObject) {
	NSLog(@"NSColorPanel_setTarget");
	[THIS setTarget:anObject];
}

void NSColorPanel_attachColorList (NSColorPanel* THIS, NSColorList * colorList) {
	NSLog(@"NSColorPanel_attachColorList");
	[THIS attachColorList:colorList];
}

void NSColorPanel_detachColorList (NSColorPanel* THIS, NSColorList * colorList) {
	NSLog(@"NSColorPanel_detachColorList");
	[THIS detachColorList:colorList];
}

void NSColorPanel_orderFrontColorPanel (NSColorPanel* THIS, NSColorPanel * sender) {
	NSLog(@"NSColorPanel_orderFrontColorPanel");
	[THIS orderFrontColorPanel:sender];
}

void NSColorPanel_changeColor (NSColorPanel* THIS, NSColorPanel * sender) {
	NSLog(@"NSColorPanel_changeColor");
	[THIS changeColor:sender];
}

NSColorPanel * NSColorPanel_alloc() {
	NSLog(@"NSColorPanel_alloc()");
	return [NSColorPanel alloc];
}
