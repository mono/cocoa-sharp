/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSTextFieldCell.h>

void NSTableHeaderCell_drawSortIndicatorWithFrame_inView_ascending_priority (NSTableHeaderCell* THIS, NSRect cellFrame, NSView * controlView, BOOL ascending, int priority) {
	NSLog(@"NSTableHeaderCell_drawSortIndicatorWithFrame_inView_ascending_priority");
	[THIS drawSortIndicatorWithFrame:cellFrame inView:controlView ascending:ascending priority:priority];
}

NSRect NSTableHeaderCell_sortIndicatorRectForBounds (NSTableHeaderCell* THIS, NSRect theRect) {
	NSLog(@"NSTableHeaderCell_sortIndicatorRectForBounds");
	return [THIS sortIndicatorRectForBounds:theRect];
}

NSTableHeaderCell * NSTableHeaderCell_alloc() {
	NSLog(@"NSTableHeaderCell_alloc()");
	return [NSTableHeaderCell alloc];
}
