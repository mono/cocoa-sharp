/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

#include <AppKit/AppKitDefines.h>

SEL NSUserInterfaceValidation_action (NSUserInterfaceValidation* THIS) {
	NSLog(@"NSUserInterfaceValidation_action");
	return [THIS action];
}
int NSUserInterfaceValidation_tag (NSUserInterfaceValidation* THIS) {
	NSLog(@"NSUserInterfaceValidation_tag");
	return [THIS tag];
}
/* UNSUPPORTED: 
- (BOOL)validateUserInterfaceItem:(id <NSValidatedUserInterfaceItem>)anItem;
 */

NSUserInterfaceValidation * NSUserInterfaceValidation_alloc() {
	NSLog(@"NSUserInterfaceValidation_alloc()");
	return [NSUserInterfaceValidation alloc];
}
