/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSStatusBar* NSStatusBar_systemStatusBar (NSStatusBar* THIS) {
	NSLog(@"NSStatusBar_systemStatusBar");
	return [THIS systemStatusBar];
}
NSStatusItem* NSStatusBar_statusItemWithLength (NSStatusBar* THIS, float length) {
	NSLog(@"NSStatusBar_statusItemWithLength");
	return [THIS statusItemWithLength:length];
}

void NSStatusBar_removeStatusItem (NSStatusBar* THIS, NSStatusItem* item) {
	NSLog(@"NSStatusBar_removeStatusItem");
	[THIS removeStatusItem:item];
}

BOOL NSStatusBar_isVertical (NSStatusBar* THIS) {
	NSLog(@"NSStatusBar_isVertical");
	return [THIS isVertical];
}
float NSStatusBar_thickness (NSStatusBar* THIS) {
	NSLog(@"NSStatusBar_thickness");
	return [THIS thickness];
}
NSStatusBar * NSStatusBar_alloc() {
	NSLog(@"NSStatusBar_alloc()");
	return [NSStatusBar alloc];
}
