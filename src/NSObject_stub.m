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
	NSLog(@"NSObject_allocWithZone: %@\n", zone);
	return [NSObject allocWithZone: zone];
}
		

NSObject* NSObject_init(NSObject* this)
{
	NSLog(@"NSObject_init: %@\n", this);
	return [this init];
}

void NSObject_dealloc(NSObject* this)
{
	NSLog(@"NSObject_dealloc: %@\n", this);
	[this dealloc];
}	

NSObject* NSObject_copy(NSObject* this) 
{
	NSLog(@"NSObject_copy: %@\n", this);
	return [this copy];
}

NSObject* NSObject_mutableCopy(NSObject* this)
{
	 NSLog(@"NSObject_mutableCopy: %@\n", this);
	 return [this mutableCopy];
}

NSString* NSObject_description(NSObject* this)
{
	NSLog(@"NSObject_description: %@\n", this);
	return [this description];
}

NSObject* NSObject_replacementObjectForCoder(NSObject* this, NSCoder *aNSCoder)
{
	NSLog(@"NSObject_replacementObjectForCoder: %@, %@\n", this, aNSCoder);
	return [this replacementObjectForCoder: aNSCoder];
}

NSObject* awakeAfterUsingCoder(NSObject* this, NSCoder *aNSCoder)
{
	NSLog(@"NSObject_awakeAfterUsingCoder: %@, %@\n", this, aNSCoder);
	return [this awakeAfterUsingCoder: aNSCoder];
}

void NSObject_release(NSObject* this)
{
	NSLog(@"NSObject_release: %@\n",this);
	[this release];
}
