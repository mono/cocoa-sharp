#include <Cocoa/Cocoa.h>

NSObject* NSObject_alloc()
{
	NSLog(@"NSObject_alloc\n");
	return [NSObject alloc];
}

NSObject* NSObject_init(NSObject* this)
{
	NSLog(@"NSObject_init: %@\n",this);
	return [this init];
}

void NSObject_release(NSObject* this)
{
	NSLog(@"NSObject_release: %@\n",this);
	[this release];
}
