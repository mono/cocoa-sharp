#include <Cocoa/Cocoa.h>

NSApplication * NSApplication_sharedApplication() {
	return [NSApplication sharedApplication];
}

int NSApplication_runModalForWindow(NSApplication * this, NSWindow * theWindow) {
	return [this runModalForWindow: theWindow];
}
