#include <Cocoa/Cocoa.h>

NSAutoreleasePool* NSAutoreleasePool_alloc()
{
	NSLog(@"NSAutoreleasePool_alloc\n");
	NSAutoreleasePool* ret = [NSAutoreleasePool alloc];
	NSLog(@"ret = %@\n",ret);
	return ret;
}
