/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSView.h>

NSBorderType NSBox_borderType (NSBox* THIS) {
	NSLog(@"NSBox_borderType");
	return [THIS borderType];
}
NSTitlePosition NSBox_titlePosition (NSBox* THIS) {
	NSLog(@"NSBox_titlePosition");
	return [THIS titlePosition];
}
void NSBox_setBorderType (NSBox* THIS, NSBorderType aType) {
	NSLog(@"NSBox_setBorderType");
	[THIS setBorderType:aType];
}

void NSBox_setBoxType (NSBox* THIS, NSBoxType boxType) {
	NSLog(@"NSBox_setBoxType");
	[THIS setBoxType:boxType];
}

NSBoxType NSBox_boxType (NSBox* THIS) {
	NSLog(@"NSBox_boxType");
	return [THIS boxType];
}
void NSBox_setTitlePosition (NSBox* THIS, NSTitlePosition aPosition) {
	NSLog(@"NSBox_setTitlePosition");
	[THIS setTitlePosition:aPosition];
}

NSString * NSBox_title (NSBox* THIS) {
	NSLog(@"NSBox_title");
	return [THIS title];
}
void NSBox_setTitle (NSBox* THIS, NSString * aString) {
	NSLog(@"NSBox_setTitle");
	[THIS setTitle:aString];
}

NSFont * NSBox_titleFont (NSBox* THIS) {
	NSLog(@"NSBox_titleFont");
	return [THIS titleFont];
}
void NSBox_setTitleFont (NSBox* THIS, NSFont * fontObj) {
	NSLog(@"NSBox_setTitleFont");
	[THIS setTitleFont:fontObj];
}

NSRect NSBox_borderRect (NSBox* THIS) {
	NSLog(@"NSBox_borderRect");
	return [THIS borderRect];
}
NSRect NSBox_titleRect (NSBox* THIS) {
	NSLog(@"NSBox_titleRect");
	return [THIS titleRect];
}
NSBox * NSBox_titleCell (NSBox* THIS) {
	NSLog(@"NSBox_titleCell");
	return [THIS titleCell];
}
void NSBox_sizeToFit (NSBox* THIS) {
	NSLog(@"NSBox_sizeToFit");
	[THIS sizeToFit];
}
NSSize NSBox_contentViewMargins (NSBox* THIS) {
	NSLog(@"NSBox_contentViewMargins");
	return [THIS contentViewMargins];
}
void NSBox_setContentViewMargins (NSBox* THIS, NSSize offsetSize) {
	NSLog(@"NSBox_setContentViewMargins");
	[THIS setContentViewMargins:offsetSize];
}

void NSBox_setFrameFromContentFrame (NSBox* THIS, NSRect contentFrame) {
	NSLog(@"NSBox_setFrameFromContentFrame");
	[THIS setFrameFromContentFrame:contentFrame];
}

NSBox * NSBox_contentView (NSBox* THIS) {
	NSLog(@"NSBox_contentView");
	return [THIS contentView];
}
void NSBox_setContentView (NSBox* THIS, NSView * aView) {
	NSLog(@"NSBox_setContentView");
	[THIS setContentView:aView];
}

void NSBox_setTitleWithMnemonic (NSBox* THIS, NSString * stringWithAmpersand) {
	NSLog(@"NSBox_setTitleWithMnemonic");
	[THIS setTitleWithMnemonic:stringWithAmpersand];
}

NSBox * NSBox_alloc() {
	NSLog(@"NSBox_alloc()");
	return [NSBox alloc];
}
