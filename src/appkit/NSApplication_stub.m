/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/NSResponder.h>

#include <AppKit/AppKitDefines.h>

NSApplication * NSApplication_sharedApplication () {
	NSLog(@"NSApplication_sharedApplication");
	return [NSApplication sharedApplication];
}
void NSApplication_setDelegate (NSApplication* THIS, NSApplication * anObject) {
	NSLog(@"NSApplication_setDelegate");
	[THIS setDelegate:anObject];
}

NSApplication * NSApplication_delegate (NSApplication* THIS) {
	NSLog(@"NSApplication_delegate");
	return [THIS delegate];
}
NSGraphicsContext* NSApplication_context (NSApplication* THIS) {
	NSLog(@"NSApplication_context");
	return [THIS context];
}
void NSApplication_hide (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_hide");
	[THIS hide:sender];
}

void NSApplication_unhide (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_unhide");
	[THIS unhide:sender];
}

void NSApplication_unhideWithoutActivation (NSApplication* THIS) {
	NSLog(@"NSApplication_unhideWithoutActivation");
	[THIS unhideWithoutActivation];
}
NSWindow * NSApplication_windowWithWindowNumber (NSApplication* THIS, int windowNum) {
	NSLog(@"NSApplication_windowWithWindowNumber");
	return [THIS windowWithWindowNumber:windowNum];
}

NSWindow * NSApplication_mainWindow (NSApplication* THIS) {
	NSLog(@"NSApplication_mainWindow");
	return [THIS mainWindow];
}
NSWindow * NSApplication_keyWindow (NSApplication* THIS) {
	NSLog(@"NSApplication_keyWindow");
	return [THIS keyWindow];
}
BOOL NSApplication_isActive (NSApplication* THIS) {
	NSLog(@"NSApplication_isActive");
	return [THIS isActive];
}
BOOL NSApplication_isHidden (NSApplication* THIS) {
	NSLog(@"NSApplication_isHidden");
	return [THIS isHidden];
}
BOOL NSApplication_isRunning (NSApplication* THIS) {
	NSLog(@"NSApplication_isRunning");
	return [THIS isRunning];
}
void NSApplication_deactivate (NSApplication* THIS) {
	NSLog(@"NSApplication_deactivate");
	[THIS deactivate];
}
void NSApplication_activateIgnoringOtherApps (NSApplication* THIS, BOOL flag) {
	NSLog(@"NSApplication_activateIgnoringOtherApps");
	[THIS activateIgnoringOtherApps:flag];
}

void NSApplication_hideOtherApplications (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_hideOtherApplications");
	[THIS hideOtherApplications:sender];
}

void NSApplication_unhideAllApplications (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_unhideAllApplications");
	[THIS unhideAllApplications:sender];
}

void NSApplication_finishLaunching (NSApplication* THIS) {
	NSLog(@"NSApplication_finishLaunching");
	[THIS finishLaunching];
}
void NSApplication_run (NSApplication* THIS) {
	NSLog(@"NSApplication_run");
	[THIS run];
}
int NSApplication_runModalForWindow (NSApplication* THIS, NSWindow * theWindow) {
	NSLog(@"NSApplication_runModalForWindow");
	return [THIS runModalForWindow:theWindow];
}

void NSApplication_stop (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_stop");
	[THIS stop:sender];
}

void NSApplication_stopModal (NSApplication* THIS) {
	NSLog(@"NSApplication_stopModal");
	[THIS stopModal];
}
void NSApplication_stopModalWithCode (NSApplication* THIS, int returnCode) {
	NSLog(@"NSApplication_stopModalWithCode");
	[THIS stopModalWithCode:returnCode];
}

void NSApplication_abortModal (NSApplication* THIS) {
	NSLog(@"NSApplication_abortModal");
	[THIS abortModal];
}
NSWindow * NSApplication_modalWindow (NSApplication* THIS) {
	NSLog(@"NSApplication_modalWindow");
	return [THIS modalWindow];
}
NSModalSession NSApplication_beginModalSessionForWindow (NSApplication* THIS, NSWindow * theWindow) {
	NSLog(@"NSApplication_beginModalSessionForWindow");
	return [THIS beginModalSessionForWindow:theWindow];
}

int NSApplication_runModalSession (NSApplication* THIS, NSModalSession session) {
	NSLog(@"NSApplication_runModalSession");
	return [THIS runModalSession:session];
}

void NSApplication_endModalSession (NSApplication* THIS, NSModalSession session) {
	NSLog(@"NSApplication_endModalSession");
	[THIS endModalSession:session];
}

void NSApplication_terminate (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_terminate");
	[THIS terminate:sender];
}

int NSApplication_requestUserAttention (NSApplication* THIS, NSRequestUserAttentionType requestType) {
	NSLog(@"NSApplication_requestUserAttention");
	return [THIS requestUserAttention:requestType];
}

void NSApplication_cancelUserAttentionRequest (NSApplication* THIS, int request) {
	NSLog(@"NSApplication_cancelUserAttentionRequest");
	[THIS cancelUserAttentionRequest:request];
}

void NSApplication_beginSheet_modalForWindow_modalDelegate_didEndSelector_contextInfo (NSApplication* THIS, NSWindow * sheet, NSWindow * docWindow, NSApplication * modalDelegate, SEL didEndSelector, void * contextInfo) {
	NSLog(@"NSApplication_beginSheet_modalForWindow_modalDelegate_didEndSelector_contextInfo");
	[THIS beginSheet:sheet modalForWindow:docWindow modalDelegate:modalDelegate didEndSelector:didEndSelector contextInfo:contextInfo];
}

void NSApplication_endSheet (NSApplication* THIS, NSWindow * sheet) {
	NSLog(@"NSApplication_endSheet");
	[THIS endSheet:sheet];
}

void NSApplication_endSheet_returnCode (NSApplication* THIS, NSWindow * sheet, int returnCode) {
	NSLog(@"NSApplication_endSheet_returnCode");
	[THIS endSheet:sheet returnCode:returnCode];
}

int NSApplication_runModalForWindow_relativeToWindow (NSApplication* THIS, NSWindow * theWindow, NSWindow * docWindow) {
	NSLog(@"NSApplication_runModalForWindow_relativeToWindow");
	return [THIS runModalForWindow:theWindow relativeToWindow:docWindow];
}

NSModalSession NSApplication_beginModalSessionForWindow_relativeToWindow (NSApplication* THIS, NSWindow * theWindow, NSWindow * docWindow) {
	NSLog(@"NSApplication_beginModalSessionForWindow_relativeToWindow");
	return [THIS beginModalSessionForWindow:theWindow relativeToWindow:docWindow];
}

NSEvent * NSApplication_nextEventMatchingMask_untilDate_inMode_dequeue (NSApplication* THIS, unsigned int mask, NSDate * expiration, NSString * mode, BOOL deqFlag) {
	NSLog(@"NSApplication_nextEventMatchingMask_untilDate_inMode_dequeue");
	return [THIS nextEventMatchingMask:mask untilDate:expiration inMode:mode dequeue:deqFlag];
}

void NSApplication_discardEventsMatchingMask_beforeEvent (NSApplication* THIS, unsigned int mask, NSEvent * lastEvent) {
	NSLog(@"NSApplication_discardEventsMatchingMask_beforeEvent");
	[THIS discardEventsMatchingMask:mask beforeEvent:lastEvent];
}

void NSApplication_postEvent_atStart (NSApplication* THIS, NSEvent * event, BOOL flag) {
	NSLog(@"NSApplication_postEvent_atStart");
	[THIS postEvent:event atStart:flag];
}

NSEvent * NSApplication_currentEvent (NSApplication* THIS) {
	NSLog(@"NSApplication_currentEvent");
	return [THIS currentEvent];
}
void NSApplication_sendEvent (NSApplication* THIS, NSEvent * theEvent) {
	NSLog(@"NSApplication_sendEvent");
	[THIS sendEvent:theEvent];
}

void NSApplication_preventWindowOrdering (NSApplication* THIS) {
	NSLog(@"NSApplication_preventWindowOrdering");
	[THIS preventWindowOrdering];
}
NSWindow * NSApplication_makeWindowsPerform_inOrder (NSApplication* THIS, SEL aSelector, BOOL flag) {
	NSLog(@"NSApplication_makeWindowsPerform_inOrder");
	return [THIS makeWindowsPerform:aSelector inOrder:flag];
}

NSArray * NSApplication_windows (NSApplication* THIS) {
	NSLog(@"NSApplication_windows");
	return [THIS windows];
}
void NSApplication_setWindowsNeedUpdate (NSApplication* THIS, BOOL needUpdate) {
	NSLog(@"NSApplication_setWindowsNeedUpdate");
	[THIS setWindowsNeedUpdate:needUpdate];
}

void NSApplication_updateWindows (NSApplication* THIS) {
	NSLog(@"NSApplication_updateWindows");
	[THIS updateWindows];
}
void NSApplication_setMainMenu (NSApplication* THIS, NSMenu * aMenu) {
	NSLog(@"NSApplication_setMainMenu");
	[THIS setMainMenu:aMenu];
}

NSMenu * NSApplication_mainMenu (NSApplication* THIS) {
	NSLog(@"NSApplication_mainMenu");
	return [THIS mainMenu];
}
void NSApplication_setApplicationIconImage (NSApplication* THIS, NSImage * image) {
	NSLog(@"NSApplication_setApplicationIconImage");
	[THIS setApplicationIconImage:image];
}

NSImage * NSApplication_applicationIconImage (NSApplication* THIS) {
	NSLog(@"NSApplication_applicationIconImage");
	return [THIS applicationIconImage];
}
BOOL NSApplication_sendAction_to_from (NSApplication* THIS, SEL theAction, NSApplication * theTarget, NSApplication * sender) {
	NSLog(@"NSApplication_sendAction_to_from");
	return [THIS sendAction:theAction to:theTarget from:sender];
}

NSApplication * NSApplication_targetForAction (NSApplication* THIS, SEL theAction) {
	NSLog(@"NSApplication_targetForAction");
	return [THIS targetForAction:theAction];
}

NSApplication * NSApplication_targetForAction_to_from (NSApplication* THIS, SEL theAction, NSApplication * theTarget, NSApplication * sender) {
	NSLog(@"NSApplication_targetForAction_to_from");
	return [THIS targetForAction:theAction to:theTarget from:sender];
}

BOOL NSApplication_tryToPerform_with (NSApplication* THIS, SEL anAction, NSApplication * anObject) {
	NSLog(@"NSApplication_tryToPerform_with");
	return [THIS tryToPerform:anAction with:anObject];
}

NSApplication * NSApplication_validRequestorForSendType_returnType (NSApplication* THIS, NSString * sendType, NSString * returnType) {
	NSLog(@"NSApplication_validRequestorForSendType_returnType");
	return [THIS validRequestorForSendType:sendType returnType:returnType];
}

void NSApplication_reportException (NSApplication* THIS, NSException * theException) {
	NSLog(@"NSApplication_reportException");
	[THIS reportException:theException];
}

void NSApplication_detachDrawingThread_toTarget_withObject (NSApplication* THIS, SEL selector, NSApplication * target, NSApplication * argument) {
	NSLog(@"NSApplication_detachDrawingThread_toTarget_withObject");
	[THIS detachDrawingThread:selector toTarget:target withObject:argument];
}

void NSApplication_replyToApplicationShouldTerminate (NSApplication* THIS, BOOL shouldTerminate) {
	NSLog(@"NSApplication_replyToApplicationShouldTerminate");
	[THIS replyToApplicationShouldTerminate:shouldTerminate];
}

void NSApplication_replyToOpenOrPrint (NSApplication* THIS, NSApplicationDelegateReply reply) {
	NSLog(@"NSApplication_replyToOpenOrPrint");
	[THIS replyToOpenOrPrint:reply];
}

void NSApplication_orderFrontCharacterPalette (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_orderFrontCharacterPalette");
	[THIS orderFrontCharacterPalette:sender];
}

void NSApplication_setWindowsMenu (NSApplication* THIS, NSMenu * aMenu) {
	NSLog(@"NSApplication_setWindowsMenu");
	[THIS setWindowsMenu:aMenu];
}

NSMenu * NSApplication_windowsMenu (NSApplication* THIS) {
	NSLog(@"NSApplication_windowsMenu");
	return [THIS windowsMenu];
}
void NSApplication_arrangeInFront (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_arrangeInFront");
	[THIS arrangeInFront:sender];
}

void NSApplication_removeWindowsItem (NSApplication* THIS, NSWindow * win) {
	NSLog(@"NSApplication_removeWindowsItem");
	[THIS removeWindowsItem:win];
}

void NSApplication_addWindowsItem_title_filename (NSApplication* THIS, NSWindow * win, NSString * aString, BOOL isFilename) {
	NSLog(@"NSApplication_addWindowsItem_title_filename");
	[THIS addWindowsItem:win title:aString filename:isFilename];
}

void NSApplication_changeWindowsItem_title_filename (NSApplication* THIS, NSWindow * win, NSString * aString, BOOL isFilename) {
	NSLog(@"NSApplication_changeWindowsItem_title_filename");
	[THIS changeWindowsItem:win title:aString filename:isFilename];
}

void NSApplication_updateWindowsItem (NSApplication* THIS, NSWindow * win) {
	NSLog(@"NSApplication_updateWindowsItem");
	[THIS updateWindowsItem:win];
}

void NSApplication_miniaturizeAll (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_miniaturizeAll");
	[THIS miniaturizeAll:sender];
}

void NSApplication_applicationWillFinishLaunching (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillFinishLaunching");
	[THIS applicationWillFinishLaunching:notification];
}

void NSApplication_applicationDidFinishLaunching (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidFinishLaunching");
	[THIS applicationDidFinishLaunching:notification];
}

void NSApplication_applicationWillHide (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillHide");
	[THIS applicationWillHide:notification];
}

void NSApplication_applicationDidHide (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidHide");
	[THIS applicationDidHide:notification];
}

void NSApplication_applicationWillUnhide (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillUnhide");
	[THIS applicationWillUnhide:notification];
}

void NSApplication_applicationDidUnhide (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidUnhide");
	[THIS applicationDidUnhide:notification];
}

void NSApplication_applicationWillBecomeActive (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillBecomeActive");
	[THIS applicationWillBecomeActive:notification];
}

void NSApplication_applicationDidBecomeActive (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidBecomeActive");
	[THIS applicationDidBecomeActive:notification];
}

void NSApplication_applicationWillResignActive (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillResignActive");
	[THIS applicationWillResignActive:notification];
}

void NSApplication_applicationDidResignActive (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidResignActive");
	[THIS applicationDidResignActive:notification];
}

void NSApplication_applicationWillUpdate (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillUpdate");
	[THIS applicationWillUpdate:notification];
}

void NSApplication_applicationDidUpdate (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidUpdate");
	[THIS applicationDidUpdate:notification];
}

void NSApplication_applicationWillTerminate (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationWillTerminate");
	[THIS applicationWillTerminate:notification];
}

void NSApplication_applicationDidChangeScreenParameters (NSApplication* THIS, NSNotification * notification) {
	NSLog(@"NSApplication_applicationDidChangeScreenParameters");
	[THIS applicationDidChangeScreenParameters:notification];
}

NSApplicationTerminateReply NSApplication_applicationShouldTerminate (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_applicationShouldTerminate");
	return [THIS applicationShouldTerminate:sender];
}

BOOL NSApplication_application_openFile (NSApplication* THIS, NSApplication * sender, NSString * filename) {
	NSLog(@"NSApplication_application_openFile");
	return [THIS application:sender openFile:filename];
}

void NSApplication_application_openFiles (NSApplication* THIS, NSApplication * sender, NSArray * filenames) {
	NSLog(@"NSApplication_application_openFiles");
	[THIS application:sender openFiles:filenames];
}

BOOL NSApplication_application_openTempFile (NSApplication* THIS, NSApplication * sender, NSString * filename) {
	NSLog(@"NSApplication_application_openTempFile");
	return [THIS application:sender openTempFile:filename];
}

BOOL NSApplication_applicationShouldOpenUntitledFile (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_applicationShouldOpenUntitledFile");
	return [THIS applicationShouldOpenUntitledFile:sender];
}

BOOL NSApplication_applicationOpenUntitledFile (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_applicationOpenUntitledFile");
	return [THIS applicationOpenUntitledFile:sender];
}

BOOL NSApplication_application_openFileWithoutUI (NSApplication* THIS, NSApplication * sender, NSString * filename) {
	NSLog(@"NSApplication_application_openFileWithoutUI");
	return [THIS application:sender openFileWithoutUI:filename];
}

BOOL NSApplication_application_printFile (NSApplication* THIS, NSApplication * sender, NSString * filename) {
	NSLog(@"NSApplication_application_printFile");
	return [THIS application:sender printFile:filename];
}

void NSApplication_application_printFiles (NSApplication* THIS, NSApplication * sender, NSArray * filenames) {
	NSLog(@"NSApplication_application_printFiles");
	[THIS application:sender printFiles:filenames];
}

BOOL NSApplication_applicationShouldTerminateAfterLastWindowClosed (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_applicationShouldTerminateAfterLastWindowClosed");
	return [THIS applicationShouldTerminateAfterLastWindowClosed:sender];
}

BOOL NSApplication_applicationShouldHandleReopen_hasVisibleWindows (NSApplication* THIS, NSApplication * sender, BOOL flag) {
	NSLog(@"NSApplication_applicationShouldHandleReopen_hasVisibleWindows");
	return [THIS applicationShouldHandleReopen:sender hasVisibleWindows:flag];
}

NSMenu * NSApplication_applicationDockMenu (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_applicationDockMenu");
	return [THIS applicationDockMenu:sender];
}

void NSApplication_setAppleMenu (NSApplication* THIS, NSMenu * menu) {
	NSLog(@"NSApplication_setAppleMenu");
	[THIS setAppleMenu:menu];
}

void NSApplication_setServicesMenu (NSApplication* THIS, NSMenu * aMenu) {
	NSLog(@"NSApplication_setServicesMenu");
	[THIS setServicesMenu:aMenu];
}

NSMenu * NSApplication_servicesMenu (NSApplication* THIS) {
	NSLog(@"NSApplication_servicesMenu");
	return [THIS servicesMenu];
}
void NSApplication_registerServicesMenuSendTypes_returnTypes (NSApplication* THIS, NSArray * sendTypes, NSArray * returnTypes) {
	NSLog(@"NSApplication_registerServicesMenuSendTypes_returnTypes");
	[THIS registerServicesMenuSendTypes:sendTypes returnTypes:returnTypes];
}

BOOL NSApplication_writeSelectionToPasteboard_types (NSApplication* THIS, NSPasteboard * pboard, NSArray * types) {
	NSLog(@"NSApplication_writeSelectionToPasteboard_types");
	return [THIS writeSelectionToPasteboard:pboard types:types];
}

BOOL NSApplication_readSelectionFromPasteboard (NSApplication* THIS, NSPasteboard * pboard) {
	NSLog(@"NSApplication_readSelectionFromPasteboard");
	return [THIS readSelectionFromPasteboard:pboard];
}

void NSApplication_setServicesProvider (NSApplication* THIS, NSApplication * provider) {
	NSLog(@"NSApplication_setServicesProvider");
	[THIS setServicesProvider:provider];
}

NSApplication * NSApplication_servicesProvider (NSApplication* THIS) {
	NSLog(@"NSApplication_servicesProvider");
	return [THIS servicesProvider];
}
void NSApplication_orderFrontStandardAboutPanel (NSApplication* THIS, NSApplication * sender) {
	NSLog(@"NSApplication_orderFrontStandardAboutPanel");
	[THIS orderFrontStandardAboutPanel:sender];
}

void NSApplication_orderFrontStandardAboutPanelWithOptions (NSApplication* THIS, NSDictionary * optionsDictionary) {
	NSLog(@"NSApplication_orderFrontStandardAboutPanelWithOptions");
	[THIS orderFrontStandardAboutPanelWithOptions:optionsDictionary];
}

void NSApplication_setApplicationHandle_commandLine_show (NSApplication* THIS, NSString * cmdLine, int cmdShow) {
	NSLog(@"NSApplication_setApplicationHandle_commandLine_show");
	[THIS setApplicationHandlecommandLine:cmdLine show:cmdShow];
}

void NSApplication_useRunningCopyOfApplication (NSApplication* THIS) {
	NSLog(@"NSApplication_useRunningCopyOfApplication");
	[THIS useRunningCopyOfApplication];
}
/* UNSUPPORTED: 
- (void * *HINSTANCE*)applicationHandle;
 */

/* UNSUPPORTED: 
- (NSWindow *)windowWithWindowHandle:(void * *HWND*)hWnd;  does not create a new NSWindow
 */

NSApplication * NSApplication_alloc() {
	NSLog(@"NSApplication_alloc()");
	return [NSApplication alloc];
}
