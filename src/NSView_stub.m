#include <Cocoa/Cocoa.h>

void NSView_addSubview(NSView* THIS, NSView *aView)
{
	[THIS addSubview: aView];
}
