/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSController.h>

NSUserDefaultsController * NSUserDefaultsController_sharedUserDefaultsController (NSUserDefaultsController* THIS) {
	NSLog(@"NSUserDefaultsController_sharedUserDefaultsController");
	return [THIS sharedUserDefaultsController];
}
NSUserDefaultsController * NSUserDefaultsController_initWithDefaults_initialValues (NSUserDefaultsController* THIS, NSUserDefaults * defaults, NSDictionary * initialValues) {
	NSLog(@"NSUserDefaultsController_initWithDefaults_initialValues");
	return [THIS initWithDefaults:defaults initialValues:initialValues];
}

NSUserDefaults * NSUserDefaultsController_defaults (NSUserDefaultsController* THIS) {
	NSLog(@"NSUserDefaultsController_defaults");
	return [THIS defaults];
}
void NSUserDefaultsController_setInitialValues (NSUserDefaultsController* THIS, NSDictionary * initialValues) {
	NSLog(@"NSUserDefaultsController_setInitialValues");
	[THIS setInitialValues:initialValues];
}

NSDictionary * NSUserDefaultsController_initialValues (NSUserDefaultsController* THIS) {
	NSLog(@"NSUserDefaultsController_initialValues");
	return [THIS initialValues];
}
void NSUserDefaultsController_setAppliesImmediately (NSUserDefaultsController* THIS, BOOL flag) {
	NSLog(@"NSUserDefaultsController_setAppliesImmediately");
	[THIS setAppliesImmediately:flag];
}

BOOL NSUserDefaultsController_appliesImmediately (NSUserDefaultsController* THIS) {
	NSLog(@"NSUserDefaultsController_appliesImmediately");
	return [THIS appliesImmediately];
}
NSUserDefaultsController * NSUserDefaultsController_values (NSUserDefaultsController* THIS) {
	NSLog(@"NSUserDefaultsController_values");
	return [THIS values];
}
void NSUserDefaultsController_revert (NSUserDefaultsController* THIS, NSUserDefaultsController * sender) {
	NSLog(@"NSUserDefaultsController_revert");
	[THIS revert:sender];
}

void NSUserDefaultsController_save (NSUserDefaultsController* THIS, NSUserDefaultsController * sender) {
	NSLog(@"NSUserDefaultsController_save");
	[THIS save:sender];
}

void NSUserDefaultsController_revertToInitialValues (NSUserDefaultsController* THIS, NSUserDefaultsController * sender) {
	NSLog(@"NSUserDefaultsController_revertToInitialValues");
	[THIS revertToInitialValues:sender];
}

NSUserDefaultsController * NSUserDefaultsController_alloc() {
	NSLog(@"NSUserDefaultsController_alloc()");
	return [NSUserDefaultsController alloc];
}
