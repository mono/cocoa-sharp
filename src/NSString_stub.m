#include <Cocoa/Cocoa.h>

NSString *NSString_initWithCString(const char * str)
{
	NSLog(@"NSString_initWithCString: '%s'\n", str);
	return [NSString stringWithCString: str];
}

SEL SEL_fromString(NSString *str)
{
	return NSSelectorFromString(str);
}
