/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSCell.h>

NSImageAlignment NSImageCell_imageAlignment (NSImageCell* THIS) {
	NSLog(@"NSImageCell_imageAlignment");
	return [THIS imageAlignment];
}
void NSImageCell_setImageAlignment (NSImageCell* THIS, NSImageAlignment newAlign) {
	NSLog(@"NSImageCell_setImageAlignment");
	[THIS setImageAlignment:newAlign];
}

NSImageScaling NSImageCell_imageScaling (NSImageCell* THIS) {
	NSLog(@"NSImageCell_imageScaling");
	return [THIS imageScaling];
}
void NSImageCell_setImageScaling (NSImageCell* THIS, NSImageScaling newScaling) {
	NSLog(@"NSImageCell_setImageScaling");
	[THIS setImageScaling:newScaling];
}

NSImageFrameStyle NSImageCell_imageFrameStyle (NSImageCell* THIS) {
	NSLog(@"NSImageCell_imageFrameStyle");
	return [THIS imageFrameStyle];
}
void NSImageCell_setImageFrameStyle (NSImageCell* THIS, NSImageFrameStyle newStyle) {
	NSLog(@"NSImageCell_setImageFrameStyle");
	[THIS setImageFrameStyle:newStyle];
}

NSImageCell * NSImageCell_alloc() {
	NSLog(@"NSImageCell_alloc()");
	return [NSImageCell alloc];
}
