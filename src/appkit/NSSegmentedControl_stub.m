/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSControl.h>

void NSSegmentedControl_setSegmentCount (NSSegmentedControl* THIS, int count) {
	NSLog(@"NSSegmentedControl_setSegmentCount");
	[THIS setSegmentCount:count];
}

int NSSegmentedControl_segmentCount (NSSegmentedControl* THIS) {
	NSLog(@"NSSegmentedControl_segmentCount");
	return [THIS segmentCount];
}
void NSSegmentedControl_setSelectedSegment (NSSegmentedControl* THIS, int selectedSegment) {
	NSLog(@"NSSegmentedControl_setSelectedSegment");
	[THIS setSelectedSegment:selectedSegment];
}

int NSSegmentedControl_selectedSegment (NSSegmentedControl* THIS) {
	NSLog(@"NSSegmentedControl_selectedSegment");
	return [THIS selectedSegment];
}
void NSSegmentedControl_setWidth_forSegment (NSSegmentedControl* THIS, float width, int segment) {
	NSLog(@"NSSegmentedControl_setWidth_forSegment");
	[THIS setWidth:width forSegment:segment];
}

float NSSegmentedControl_widthForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_widthForSegment");
	return [THIS widthForSegment:segment];
}

void NSSegmentedControl_setImage_forSegment (NSSegmentedControl* THIS, NSImage * image, int segment) {
	NSLog(@"NSSegmentedControl_setImage_forSegment");
	[THIS setImage:image forSegment:segment];
}

NSImage * NSSegmentedControl_imageForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_imageForSegment");
	return [THIS imageForSegment:segment];
}

void NSSegmentedControl_setLabel_forSegment (NSSegmentedControl* THIS, NSString * label, int segment) {
	NSLog(@"NSSegmentedControl_setLabel_forSegment");
	[THIS setLabel:label forSegment:segment];
}

NSString * NSSegmentedControl_labelForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_labelForSegment");
	return [THIS labelForSegment:segment];
}

void NSSegmentedControl_setMenu_forSegment (NSSegmentedControl* THIS, NSMenu * menu, int segment) {
	NSLog(@"NSSegmentedControl_setMenu_forSegment");
	[THIS setMenu:menu forSegment:segment];
}

NSMenu * NSSegmentedControl_menuForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_menuForSegment");
	return [THIS menuForSegment:segment];
}

void NSSegmentedControl_setSelected_forSegment (NSSegmentedControl* THIS, BOOL selected, int segment) {
	NSLog(@"NSSegmentedControl_setSelected_forSegment");
	[THIS setSelected:selected forSegment:segment];
}

BOOL NSSegmentedControl_isSelectedForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_isSelectedForSegment");
	return [THIS isSelectedForSegment:segment];
}

void NSSegmentedControl_setEnabled_forSegment (NSSegmentedControl* THIS, BOOL enabled, int segment) {
	NSLog(@"NSSegmentedControl_setEnabled_forSegment");
	[THIS setEnabled:enabled forSegment:segment];
}

BOOL NSSegmentedControl_isEnabledForSegment (NSSegmentedControl* THIS, int segment) {
	NSLog(@"NSSegmentedControl_isEnabledForSegment");
	return [THIS isEnabledForSegment:segment];
}

NSSegmentedControl * NSSegmentedControl_alloc() {
	NSLog(@"NSSegmentedControl_alloc()");
	return [NSSegmentedControl alloc];
}
