#include <Cocoa/Cocoa.h>

NSTextField * NSTextField_alloc()
{
	return [NSTextField alloc];
}

void NSTextField_setEditable(NSTextField *THIS, BOOL val)
{
	[THIS setEditable: val];
}

void NSTextField_setBezeled(NSTextField *THIS, BOOL val)
{
	[THIS setBezeled: val];
}
