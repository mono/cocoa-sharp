/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSImageRep.h>

NSPICTImageRep * NSPICTImageRep_imageRepWithData (NSPICTImageRep* THIS, NSData* pictData) {
	NSLog(@"NSPICTImageRep_imageRepWithData");
	return [THIS imageRepWithData:pictData];
}

NSPICTImageRep * NSPICTImageRep_initWithData (NSPICTImageRep* THIS, NSData* pictData) {
	NSLog(@"NSPICTImageRep_initWithData");
	return [THIS initWithData:pictData];
}

NSData* NSPICTImageRep_PICTRepresentation (NSPICTImageRep* THIS) {
	NSLog(@"NSPICTImageRep_PICTRepresentation");
	return [THIS PICTRepresentation];
}
NSRect NSPICTImageRep_boundingBox (NSPICTImageRep* THIS) {
	NSLog(@"NSPICTImageRep_boundingBox");
	return [THIS boundingBox];
}
NSPICTImageRep * NSPICTImageRep_alloc() {
	NSLog(@"NSPICTImageRep_alloc()");
	return [NSPICTImageRep alloc];
}
