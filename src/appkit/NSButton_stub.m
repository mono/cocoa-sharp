/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSControl.h>

#include <AppKit/NSButtonCell.h>

NSString * NSButton_title (NSButton* THIS) {
	NSLog(@"NSButton_title");
	return [THIS title];
}
void NSButton_setTitle (NSButton* THIS, NSString * aString) {
	NSLog(@"NSButton_setTitle");
	[THIS setTitle:aString];
}

NSString * NSButton_alternateTitle (NSButton* THIS) {
	NSLog(@"NSButton_alternateTitle");
	return [THIS alternateTitle];
}
void NSButton_setAlternateTitle (NSButton* THIS, NSString * aString) {
	NSLog(@"NSButton_setAlternateTitle");
	[THIS setAlternateTitle:aString];
}

NSImage * NSButton_image (NSButton* THIS) {
	NSLog(@"NSButton_image");
	return [THIS image];
}
void NSButton_setImage (NSButton* THIS, NSImage * image) {
	NSLog(@"NSButton_setImage");
	[THIS setImage:image];
}

NSImage * NSButton_alternateImage (NSButton* THIS) {
	NSLog(@"NSButton_alternateImage");
	return [THIS alternateImage];
}
void NSButton_setAlternateImage (NSButton* THIS, NSImage * image) {
	NSLog(@"NSButton_setAlternateImage");
	[THIS setAlternateImage:image];
}

NSCellImagePosition NSButton_imagePosition (NSButton* THIS) {
	NSLog(@"NSButton_imagePosition");
	return [THIS imagePosition];
}
void NSButton_setImagePosition (NSButton* THIS, NSCellImagePosition aPosition) {
	NSLog(@"NSButton_setImagePosition");
	[THIS setImagePosition:aPosition];
}

void NSButton_setButtonType (NSButton* THIS, NSButtonType aType) {
	NSLog(@"NSButton_setButtonType");
	[THIS setButtonType:aType];
}

int NSButton_state (NSButton* THIS) {
	NSLog(@"NSButton_state");
	return [THIS state];
}
void NSButton_setState (NSButton* THIS, int value) {
	NSLog(@"NSButton_setState");
	[THIS setState:value];
}

BOOL NSButton_isBordered (NSButton* THIS) {
	NSLog(@"NSButton_isBordered");
	return [THIS isBordered];
}
void NSButton_setBordered (NSButton* THIS, BOOL flag) {
	NSLog(@"NSButton_setBordered");
	[THIS setBordered:flag];
}

BOOL NSButton_isTransparent (NSButton* THIS) {
	NSLog(@"NSButton_isTransparent");
	return [THIS isTransparent];
}
void NSButton_setTransparent (NSButton* THIS, BOOL flag) {
	NSLog(@"NSButton_setTransparent");
	[THIS setTransparent:flag];
}

void NSButton_setPeriodicDelay_interval (NSButton* THIS, float delay, float interval) {
	NSLog(@"NSButton_setPeriodicDelay_interval");
	[THIS setPeriodicDelay:delay interval:interval];
}

void NSButton_getPeriodicDelay_interval (NSButton* THIS, float * delay, float * interval) {
	NSLog(@"NSButton_getPeriodicDelay_interval");
	[THIS getPeriodicDelay:delay interval:interval];
}

NSString * NSButton_keyEquivalent (NSButton* THIS) {
	NSLog(@"NSButton_keyEquivalent");
	return [THIS keyEquivalent];
}
void NSButton_setKeyEquivalent (NSButton* THIS, NSString * charCode) {
	NSLog(@"NSButton_setKeyEquivalent");
	[THIS setKeyEquivalent:charCode];
}

unsigned int NSButton_keyEquivalentModifierMask (NSButton* THIS) {
	NSLog(@"NSButton_keyEquivalentModifierMask");
	return [THIS keyEquivalentModifierMask];
}
void NSButton_setKeyEquivalentModifierMask (NSButton* THIS, unsigned int mask) {
	NSLog(@"NSButton_setKeyEquivalentModifierMask");
	[THIS setKeyEquivalentModifierMask:mask];
}

void NSButton_highlight (NSButton* THIS, BOOL flag) {
	NSLog(@"NSButton_highlight");
	[THIS highlight:flag];
}

BOOL NSButton_performKeyEquivalent (NSButton* THIS, NSEvent * key) {
	NSLog(@"NSButton_performKeyEquivalent");
	return [THIS performKeyEquivalent:key];
}

void NSButton_setTitleWithMnemonic (NSButton* THIS, NSString * stringWithAmpersand) {
	NSLog(@"NSButton_setTitleWithMnemonic");
	[THIS setTitleWithMnemonic:stringWithAmpersand];
}

NSAttributedString * NSButton_attributedTitle (NSButton* THIS) {
	NSLog(@"NSButton_attributedTitle");
	return [THIS attributedTitle];
}
void NSButton_setAttributedTitle (NSButton* THIS, NSAttributedString * aString) {
	NSLog(@"NSButton_setAttributedTitle");
	[THIS setAttributedTitle:aString];
}

NSAttributedString * NSButton_attributedAlternateTitle (NSButton* THIS) {
	NSLog(@"NSButton_attributedAlternateTitle");
	return [THIS attributedAlternateTitle];
}
void NSButton_setAttributedAlternateTitle (NSButton* THIS, NSAttributedString * obj) {
	NSLog(@"NSButton_setAttributedAlternateTitle");
	[THIS setAttributedAlternateTitle:obj];
}

void NSButton_setBezelStyle (NSButton* THIS, NSBezelStyle bezelStyle) {
	NSLog(@"NSButton_setBezelStyle");
	[THIS setBezelStyle:bezelStyle];
}

NSBezelStyle NSButton_bezelStyle (NSButton* THIS) {
	NSLog(@"NSButton_bezelStyle");
	return [THIS bezelStyle];
}
void NSButton_setAllowsMixedState (NSButton* THIS, BOOL flag) {
	NSLog(@"NSButton_setAllowsMixedState");
	[THIS setAllowsMixedState:flag];
}

BOOL NSButton_allowsMixedState (NSButton* THIS) {
	NSLog(@"NSButton_allowsMixedState");
	return [THIS allowsMixedState];
}
void NSButton_setNextState (NSButton* THIS) {
	NSLog(@"NSButton_setNextState");
	[THIS setNextState];
}
void NSButton_setShowsBorderOnlyWhileMouseInside (NSButton* THIS, BOOL show) {
	NSLog(@"NSButton_setShowsBorderOnlyWhileMouseInside");
	[THIS setShowsBorderOnlyWhileMouseInside:show];
}

BOOL NSButton_showsBorderOnlyWhileMouseInside (NSButton* THIS) {
	NSLog(@"NSButton_showsBorderOnlyWhileMouseInside");
	return [THIS showsBorderOnlyWhileMouseInside];
}
void NSButton_setSound (NSButton* THIS, NSSound * aSound) {
	NSLog(@"NSButton_setSound");
	[THIS setSound:aSound];
}

NSSound * NSButton_sound (NSButton* THIS) {
	NSLog(@"NSButton_sound");
	return [THIS sound];
}
NSButton * NSButton_alloc() {
	NSLog(@"NSButton_alloc()");
	return [NSButton alloc];
}
