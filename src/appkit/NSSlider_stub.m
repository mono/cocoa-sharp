/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSControl.h>

#include <AppKit/NSSliderCell.h>

double NSSlider_minValue (NSSlider* THIS) {
	NSLog(@"NSSlider_minValue");
	return [THIS minValue];
}
void NSSlider_setMinValue (NSSlider* THIS, double aDouble) {
	NSLog(@"NSSlider_setMinValue");
	[THIS setMinValue:aDouble];
}

double NSSlider_maxValue (NSSlider* THIS) {
	NSLog(@"NSSlider_maxValue");
	return [THIS maxValue];
}
void NSSlider_setMaxValue (NSSlider* THIS, double aDouble) {
	NSLog(@"NSSlider_setMaxValue");
	[THIS setMaxValue:aDouble];
}

void NSSlider_setAltIncrementValue (NSSlider* THIS, double incValue) {
	NSLog(@"NSSlider_setAltIncrementValue");
	[THIS setAltIncrementValue:incValue];
}

double NSSlider_altIncrementValue (NSSlider* THIS) {
	NSLog(@"NSSlider_altIncrementValue");
	return [THIS altIncrementValue];
}
void NSSlider_setTitleCell (NSSlider* THIS, NSCell * aCell) {
	NSLog(@"NSSlider_setTitleCell");
	[THIS setTitleCell:aCell];
}

NSSlider * NSSlider_titleCell (NSSlider* THIS) {
	NSLog(@"NSSlider_titleCell");
	return [THIS titleCell];
}
void NSSlider_setTitleColor (NSSlider* THIS, NSColor * newColor) {
	NSLog(@"NSSlider_setTitleColor");
	[THIS setTitleColor:newColor];
}

NSColor * NSSlider_titleColor (NSSlider* THIS) {
	NSLog(@"NSSlider_titleColor");
	return [THIS titleColor];
}
void NSSlider_setTitleFont (NSSlider* THIS, NSFont * fontObj) {
	NSLog(@"NSSlider_setTitleFont");
	[THIS setTitleFont:fontObj];
}

NSFont * NSSlider_titleFont (NSSlider* THIS) {
	NSLog(@"NSSlider_titleFont");
	return [THIS titleFont];
}
NSString * NSSlider_title (NSSlider* THIS) {
	NSLog(@"NSSlider_title");
	return [THIS title];
}
void NSSlider_setTitle (NSSlider* THIS, NSString * aString) {
	NSLog(@"NSSlider_setTitle");
	[THIS setTitle:aString];
}

void NSSlider_setKnobThickness (NSSlider* THIS, float aFloat) {
	NSLog(@"NSSlider_setKnobThickness");
	[THIS setKnobThickness:aFloat];
}

float NSSlider_knobThickness (NSSlider* THIS) {
	NSLog(@"NSSlider_knobThickness");
	return [THIS knobThickness];
}
void NSSlider_setImage (NSSlider* THIS, NSImage * backgroundImage) {
	NSLog(@"NSSlider_setImage");
	[THIS setImage:backgroundImage];
}

NSImage * NSSlider_image (NSSlider* THIS) {
	NSLog(@"NSSlider_image");
	return [THIS image];
}
int NSSlider_isVertical (NSSlider* THIS) {
	NSLog(@"NSSlider_isVertical");
	return [THIS isVertical];
}
BOOL NSSlider_acceptsFirstMouse (NSSlider* THIS, NSEvent * theEvent) {
	NSLog(@"NSSlider_acceptsFirstMouse");
	return [THIS acceptsFirstMouse:theEvent];
}

void NSSlider_setNumberOfTickMarks (NSSlider* THIS, int count) {
	NSLog(@"NSSlider_setNumberOfTickMarks");
	[THIS setNumberOfTickMarks:count];
}

int NSSlider_numberOfTickMarks (NSSlider* THIS) {
	NSLog(@"NSSlider_numberOfTickMarks");
	return [THIS numberOfTickMarks];
}
void NSSlider_setTickMarkPosition (NSSlider* THIS, NSTickMarkPosition position) {
	NSLog(@"NSSlider_setTickMarkPosition");
	[THIS setTickMarkPosition:position];
}

NSTickMarkPosition NSSlider_tickMarkPosition (NSSlider* THIS) {
	NSLog(@"NSSlider_tickMarkPosition");
	return [THIS tickMarkPosition];
}
void NSSlider_setAllowsTickMarkValuesOnly (NSSlider* THIS, BOOL yorn) {
	NSLog(@"NSSlider_setAllowsTickMarkValuesOnly");
	[THIS setAllowsTickMarkValuesOnly:yorn];
}

BOOL NSSlider_allowsTickMarkValuesOnly (NSSlider* THIS) {
	NSLog(@"NSSlider_allowsTickMarkValuesOnly");
	return [THIS allowsTickMarkValuesOnly];
}
double NSSlider_tickMarkValueAtIndex (NSSlider* THIS, int index) {
	NSLog(@"NSSlider_tickMarkValueAtIndex");
	return [THIS tickMarkValueAtIndex:index];
}

NSRect NSSlider_rectOfTickMarkAtIndex (NSSlider* THIS, int index) {
	NSLog(@"NSSlider_rectOfTickMarkAtIndex");
	return [THIS rectOfTickMarkAtIndex:index];
}

int NSSlider_indexOfTickMarkAtPoint (NSSlider* THIS, NSPoint point) {
	NSLog(@"NSSlider_indexOfTickMarkAtPoint");
	return [THIS indexOfTickMarkAtPoint:point];
}

double NSSlider_closestTickMarkValueToValue (NSSlider* THIS, double value) {
	NSLog(@"NSSlider_closestTickMarkValueToValue");
	return [THIS closestTickMarkValueToValue:value];
}

NSSlider * NSSlider_alloc() {
	NSLog(@"NSSlider_alloc()");
	return [NSSlider alloc];
}
