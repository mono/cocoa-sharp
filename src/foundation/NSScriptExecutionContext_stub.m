/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSScriptExecutionContext * NSScriptExecutionContext_sharedScriptExecutionContext (NSScriptExecutionContext* THIS) {
	NSLog(@"NSScriptExecutionContext_sharedScriptExecutionContext");
	return [THIS sharedScriptExecutionContext];
}
NSScriptExecutionContext * NSScriptExecutionContext_topLevelObject (NSScriptExecutionContext* THIS) {
	NSLog(@"NSScriptExecutionContext_topLevelObject");
	return [THIS topLevelObject];
}
void NSScriptExecutionContext_setTopLevelObject (NSScriptExecutionContext* THIS, NSScriptExecutionContext * obj) {
	NSLog(@"NSScriptExecutionContext_setTopLevelObject");
	[THIS setTopLevelObject:obj];
}

NSScriptExecutionContext * NSScriptExecutionContext_objectBeingTested (NSScriptExecutionContext* THIS) {
	NSLog(@"NSScriptExecutionContext_objectBeingTested");
	return [THIS objectBeingTested];
}
void NSScriptExecutionContext_setObjectBeingTested (NSScriptExecutionContext* THIS, NSScriptExecutionContext * obj) {
	NSLog(@"NSScriptExecutionContext_setObjectBeingTested");
	[THIS setObjectBeingTested:obj];
}

NSScriptExecutionContext * NSScriptExecutionContext_rangeContainerObject (NSScriptExecutionContext* THIS) {
	NSLog(@"NSScriptExecutionContext_rangeContainerObject");
	return [THIS rangeContainerObject];
}
void NSScriptExecutionContext_setRangeContainerObject (NSScriptExecutionContext* THIS, NSScriptExecutionContext * obj) {
	NSLog(@"NSScriptExecutionContext_setRangeContainerObject");
	[THIS setRangeContainerObject:obj];
}

NSScriptExecutionContext * NSScriptExecutionContext_alloc() {
	NSLog(@"NSScriptExecutionContext_alloc()");
	return [NSScriptExecutionContext alloc];
}
