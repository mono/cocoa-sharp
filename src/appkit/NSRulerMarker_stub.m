/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

#include <Foundation/NSGeometry.h>

NSRulerMarker * NSRulerMarker_initWithRulerView_markerLocation_image_imageOrigin (NSRulerMarker* THIS, NSRulerView * ruler, float location, NSImage * image, NSPoint imageOrigin) {
	NSLog(@"NSRulerMarker_initWithRulerView_markerLocation_image_imageOrigin");
	return [THIS initWithRulerView:ruler markerLocation:location image:image imageOrigin:imageOrigin];
}

NSRulerView * NSRulerMarker_ruler (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_ruler");
	return [THIS ruler];
}
void NSRulerMarker_setMarkerLocation (NSRulerMarker* THIS, float location) {
	NSLog(@"NSRulerMarker_setMarkerLocation");
	[THIS setMarkerLocation:location];
}

float NSRulerMarker_markerLocation (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_markerLocation");
	return [THIS markerLocation];
}
void NSRulerMarker_setImage (NSRulerMarker* THIS, NSImage * image) {
	NSLog(@"NSRulerMarker_setImage");
	[THIS setImage:image];
}

NSImage * NSRulerMarker_image (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_image");
	return [THIS image];
}
void NSRulerMarker_setImageOrigin (NSRulerMarker* THIS, NSPoint imageOrigin) {
	NSLog(@"NSRulerMarker_setImageOrigin");
	[THIS setImageOrigin:imageOrigin];
}

NSPoint NSRulerMarker_imageOrigin (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_imageOrigin");
	return [THIS imageOrigin];
}
void NSRulerMarker_setMovable (NSRulerMarker* THIS, BOOL flag) {
	NSLog(@"NSRulerMarker_setMovable");
	[THIS setMovable:flag];
}

void NSRulerMarker_setRemovable (NSRulerMarker* THIS, BOOL flag) {
	NSLog(@"NSRulerMarker_setRemovable");
	[THIS setRemovable:flag];
}

BOOL NSRulerMarker_isMovable (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_isMovable");
	return [THIS isMovable];
}
BOOL NSRulerMarker_isRemovable (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_isRemovable");
	return [THIS isRemovable];
}
BOOL NSRulerMarker_isDragging (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_isDragging");
	return [THIS isDragging];
}
/* UNSUPPORTED: 
- (void)setRepresentedObject:(id <NSCopying>)representedObject;
 */

/* UNSUPPORTED: 
- (id <NSCopying>)representedObject;
 */

NSRect NSRulerMarker_imageRectInRuler (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_imageRectInRuler");
	return [THIS imageRectInRuler];
}
float NSRulerMarker_thicknessRequiredInRuler (NSRulerMarker* THIS) {
	NSLog(@"NSRulerMarker_thicknessRequiredInRuler");
	return [THIS thicknessRequiredInRuler];
}
void NSRulerMarker_drawRect (NSRulerMarker* THIS, NSRect rect) {
	NSLog(@"NSRulerMarker_drawRect");
	[THIS drawRect:rect];
}

BOOL NSRulerMarker_trackMouse_adding (NSRulerMarker* THIS, NSEvent * mouseDownEvent, BOOL isAdding) {
	NSLog(@"NSRulerMarker_trackMouse_adding");
	return [THIS trackMouse:mouseDownEvent adding:isAdding];
}

NSRulerMarker * NSRulerMarker_alloc() {
	NSLog(@"NSRulerMarker_alloc()");
	return [NSRulerMarker alloc];
}
