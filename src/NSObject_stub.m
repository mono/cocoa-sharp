#include <Cocoa/Cocoa.h>

/* NSObject stub file.  It wraps Objective-C method calls in C type calls
 * This is not yet finished
 */

NSObject* NSObject_alloc()
{
	NSLog(@"NSObject_alloc\n");
	return [NSObject alloc];
}

NSObject* NSObject_allocWithZone(NSZone *zone)
{
	NSLog(@"NSObject_allocWithZone\n");
	return [NSObject allocWithZone: zone];
}
		

NSObject* NSObject_init(NSObject* this)
{
	NSLog(@"NSObject_init: %@\n",this);
	return [this init];
}

void NSObject_dealloc()
{
	NSLog(@"NSObject_dealloc \n");
	NSObject *object = [[NSObject alloc]init];
	[object dealloc];
}	

NSObject* NSObject_copy() 
{
	NSLog(@"NSObject_copy\n");
	NSObject *object = [[NSObject alloc]init];
	[object copy];
}

NSObject* NSObject_mutableCopy(NSObject* this)
{
	 NSLog(@"NSObject_mutableCopy\n");
	 [thist mutableCopy];
}

NSString* NSObject_description()
{
	NSLog(@"NSObject_description\n");
	return [NSObject description];
}

NSObject* NSObject_replacementObjectForCoder(NSObject* this, NSCoder *aNSCoder)
{
	NSLog(@"NSObject_replacementObjectForCoder\n");
	return [this replacementObjectForCoder: aNSCoder];
}

NSObject* awakeAfterUsingCoder(NSObject* this, NSCoder *aNSCoder)
{
	NSLog(@"NSObject_awakeAfterUsingCoder\n");
	return [this awakeAfterUsingCoder: aNSCoder];

void NSObject_release(NSObject* this)
{
	NSLog(@"NSObject_release: %@\n",this);
	[this release];
}
