#include <Cocoa/Cocoa.h>

NSString *NSString_initWithCString(const char * str)
{
	NSLog(@"NSString_initWithCString: '%s'\n", str);
	return [NSString stringWithCString: str];
}
