#include <Cocoa/Cocoa.h>

NSControl *NSControl_initWithFrame(NSControl *THIS, NSRect rect)
{
	return [THIS initWithFrame: rect];
}

void NSControl_setTarget(NSControl *THIS, NSObject *anObject)
{
	[THIS setTarget: anObject];
}

void NSControl_setStringValue(NSControl *THIS, NSString *aString)
{
	[THIS setStringValue: aString];
}

void NSControl_setAction(NSControl *THIS, SEL sel)
{
	[THIS setAction: sel];
}
