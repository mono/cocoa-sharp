/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSResponder.h>

#include <AppKit/AppKitDefines.h>

NSInterfaceStyle NSInterfaceStyle_interfaceStyle (NSInterfaceStyle* THIS) {
	NSLog(@"NSInterfaceStyle_interfaceStyle");
	return [THIS interfaceStyle];
}
void NSInterfaceStyle_setInterfaceStyle (NSInterfaceStyle* THIS, NSInterfaceStyle interfaceStyle) {
	NSLog(@"NSInterfaceStyle_setInterfaceStyle");
	[THIS setInterfaceStyle:interfaceStyle];
}

NSInterfaceStyle * NSInterfaceStyle_alloc() {
	NSLog(@"NSInterfaceStyle_alloc()");
	return [NSInterfaceStyle alloc];
}
