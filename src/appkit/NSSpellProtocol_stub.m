/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

void NSSpellProtocol_changeSpelling (NSSpellProtocol* THIS, NSSpellProtocol * sender) {
	NSLog(@"NSSpellProtocol_changeSpelling");
	[THIS changeSpelling:sender];
}

void NSSpellProtocol_ignoreSpelling (NSSpellProtocol* THIS, NSSpellProtocol * sender) {
	NSLog(@"NSSpellProtocol_ignoreSpelling");
	[THIS ignoreSpelling:sender];
}

NSSpellProtocol * NSSpellProtocol_alloc() {
	NSLog(@"NSSpellProtocol_alloc()");
	return [NSSpellProtocol alloc];
}
