#include <Cocoa/Cocoa.h>

NSButton * NSButton_alloc() {
	return [NSButton alloc];
}

void NSButton_setTitle(NSButton *THIS, NSString *str)
{
	[THIS setTitle: str];
}
