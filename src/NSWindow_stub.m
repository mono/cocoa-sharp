#include <Cocoa/Cocoa.h>

NSWindow * NSWindow_alloc() {
	return [NSWindow alloc];
}

NSObject * NSWindow_initWithContentRect_styleMask_backing_defer(NSWindow* this, NSRect contentRect, unsigned int aStyle, NSBackingStoreType bufferingtype, BOOL flag) {
	return [this initWithContentRect:contentRect styleMask:aStyle backing:bufferingtype defer:flag];
}

void NSWindow_setTitle(NSWindow* this, NSString* aString)
{
	[this setTitle:aString];
}

void NSWindow_center(NSWindow* this)
{
	[this center];
}

void NSWindow_makeKeyAndOrderFront(NSWindow* this, NSObject* sender)
{
	[this makeKeyAndOrderFront:sender];
}

NSObject * NSWindow_contentView(NSWindow* this)
{
	return [this contentView];
}
