/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSString * NSNotification_name (NSNotification* THIS) {
	NSLog(@"NSNotification_name");
	return [THIS name];
}
NSNotification * NSNotification_object (NSNotification* THIS) {
	NSLog(@"NSNotification_object");
	return [THIS object];
}
NSDictionary * NSNotification_userInfo (NSNotification* THIS) {
	NSLog(@"NSNotification_userInfo");
	return [THIS userInfo];
}
NSNotification * NSNotification_notificationWithName_object (NSNotification* THIS, NSString * aName, NSNotification * anObject) {
	NSLog(@"NSNotification_notificationWithName_object");
	return [THIS notificationWithName:aName object:anObject];
}

NSNotification * NSNotification_notificationWithName_object_userInfo (NSNotification* THIS, NSString * aName, NSNotification * anObject, NSDictionary * aUserInfo) {
	NSLog(@"NSNotification_notificationWithName_object_userInfo");
	return [THIS notificationWithName:aName object:anObject userInfo:aUserInfo];
}

NSNotification * NSNotification_defaultCenter (NSNotification* THIS) {
	NSLog(@"NSNotification_defaultCenter");
	return [THIS defaultCenter];
}
void NSNotification_addObserver_selector_name_object (NSNotification* THIS, NSNotification * observer, SEL aSelector, NSString * aName, NSNotification * anObject) {
	NSLog(@"NSNotification_addObserver_selector_name_object");
	[THIS addObserver:observer selector:aSelector name:aName object:anObject];
}

void NSNotification_postNotification (NSNotification* THIS, NSNotification * notification) {
	NSLog(@"NSNotification_postNotification");
	[THIS postNotification:notification];
}

void NSNotification_postNotificationName_object (NSNotification* THIS, NSString * aName, NSNotification * anObject) {
	NSLog(@"NSNotification_postNotificationName_object");
	[THIS postNotificationName:aName object:anObject];
}

void NSNotification_postNotificationName_object_userInfo (NSNotification* THIS, NSString * aName, NSNotification * anObject, NSDictionary * aUserInfo) {
	NSLog(@"NSNotification_postNotificationName_object_userInfo");
	[THIS postNotificationName:aName object:anObject userInfo:aUserInfo];
}

void NSNotification_removeObserver (NSNotification* THIS, NSNotification * observer) {
	NSLog(@"NSNotification_removeObserver");
	[THIS removeObserver:observer];
}

void NSNotification_removeObserver_name_object (NSNotification* THIS, NSNotification * observer, NSString * aName, NSNotification * anObject) {
	NSLog(@"NSNotification_removeObserver_name_object");
	[THIS removeObserver:observer name:aName object:anObject];
}

NSNotification * NSNotification_alloc() {
	NSLog(@"NSNotification_alloc()");
	return [NSNotification alloc];
}
