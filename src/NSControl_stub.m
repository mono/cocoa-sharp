#include <Cocoa/Cocoa.h>

NSControl *NSControl_initWithFrame(NSControl *THIS, NSRect rect)
{
	return [THIS initWithFrame: rect];
}
