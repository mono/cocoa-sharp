all: build build/Loader build/Test.exe build/CoreFoundationGlue

build:
	mkdir build

build/Apple.Cocoa.Foundation.dll: build src/Apple.Cocoa.Foundation/*.cs
	mcs -g /out:build/Apple.Cocoa.Foundation.dll src/Apple.Cocoa.Foundation/*.cs -target:library

build/CoreFoundationGlue: build glueobjects library

build/Test.exe: build src/test/*.cs build/Apple.Cocoa.Foundation.dll
	mcs -g src/test/*.cs -out:build/Test.exe /r:build/Apple.Cocoa.Foundation.dll

build/Loader: build src/test/Loader.c
	gcc src/test/Loader.c -o build/Loader `pkg-config --cflags --libs mono`

debug: build/Test.exe build/CoreFoundationGlue
	cd build; gdb --args ./Loader

run: build/Loader build/Test.exe build/CoreFoundationGlue
	cd build; ./Loader Test.exe

clean:
	rm -f build/*
	rm -f src/appkit/*.o
	rm -f src/foundation/*.o
src/foundation/NSAutorelease_Pool.o:
	gcc -x objective-c -c src/foundation/NSAutorelease_Pool.m -o src/foundation/NSAutorelease_Pool.o
src/foundation/NSObject_stub.o:
	gcc -x objective-c -c src/foundation/NSObject_stub.m -o src/foundation/NSObject_stub.o
src/foundation/NSSelector_stub.o:
	gcc -x objective-c -c src/foundation/NSSelector_stub.m -o src/foundation/NSSelector_stub.o
src/foundation/NSAppleEventDescriptor_stub.o:
	gcc -x objective-c -c src/foundation/NSAppleEventDescriptor_stub.m -o src/foundation/NSAppleEventDescriptor_stub.o
src/foundation/NSAppleEventManager_stub.o:
	gcc -x objective-c -c src/foundation/NSAppleEventManager_stub.m -o src/foundation/NSAppleEventManager_stub.o
src/foundation/NSAppleScript_stub.o:
	gcc -x objective-c -c src/foundation/NSAppleScript_stub.m -o src/foundation/NSAppleScript_stub.o
src/foundation/NSArray_stub.o:
	gcc -x objective-c -c src/foundation/NSArray_stub.m -o src/foundation/NSArray_stub.o
src/foundation/NSAttributedString_stub.o:
	gcc -x objective-c -c src/foundation/NSAttributedString_stub.m -o src/foundation/NSAttributedString_stub.o
src/foundation/NSCharacterSet_stub.o:
	gcc -x objective-c -c src/foundation/NSCharacterSet_stub.m -o src/foundation/NSCharacterSet_stub.o
src/foundation/NSCoder_stub.o:
	gcc -x objective-c -c src/foundation/NSCoder_stub.m -o src/foundation/NSCoder_stub.o
src/foundation/NSConnection_stub.o:
	gcc -x objective-c -c src/foundation/NSConnection_stub.m -o src/foundation/NSConnection_stub.o
src/foundation/NSData_stub.o:
	gcc -x objective-c -c src/foundation/NSData_stub.m -o src/foundation/NSData_stub.o
src/foundation/NSDateForoatter_stub.o:
	gcc -x objective-c -c src/foundation/NSDateFormatter_stub.m -o src/foundation/NSDateForoatter_stub.o
src/foundation/NSDictionary_stub.o:
	gcc -x objective-c -c src/foundation/NSDictionary_stub.m -o src/foundation/NSDictionary_stub.o
src/foundation/NSDistantObject_stub.o:
	gcc -x objective-c -c src/foundation/NSDistantObject_stub.m -o src/foundation/NSDistantObject_stub.o
src/foundation/NSDistributedLock_stub.o:
	gcc -x objective-c -c src/foundation/NSDistributedLock_stub.m -o src/foundation/NSDistributedLock_stub.o
src/foundation/NSDistributedNotificationCenter_stub.o:
	gcc -x objective-c -c src/foundation/NSDistributedNotificationCenter_stub.m -o src/foundation/NSDistributedNotificationCenter_stub.o
src/foundation/NSEnuoerator_stub.o:
	gcc -x objective-c -c src/foundation/NSEnumerator_stub.m -o src/foundation/NSEnuoerator_stub.o
src/foundation/NSError_stub.o:
	gcc -x objective-c -c src/foundation/NSError_stub.m -o src/foundation/NSError_stub.o
src/foundation/NSException_stub.o:
	gcc -x objective-c -c src/foundation/NSException_stub.m -o src/foundation/NSException_stub.o
src/foundation/NSFileHandle_stub.o:
	gcc -x objective-c -c src/foundation/NSFileHandle_stub.m -o src/foundation/NSFileHandle_stub.o
src/foundation/NSFileManager_stub.o:
	gcc -x objective-c -c src/foundation/NSFileManager_stub.m -o src/foundation/NSFileManager_stub.o
src/foundation/NSForoatter_stub.o:
	gcc -x objective-c -c src/foundation/NSFormatter_stub.m -o src/foundation/NSForoatter_stub.o
src/foundation/NSHTTPCookieStorage_stub.o:
	gcc -x objective-c -c src/foundation/NSHTTPCookieStorage_stub.m -o src/foundation/NSHTTPCookieStorage_stub.o
src/foundation/NSHost_stub.o:
	gcc -x objective-c -c src/foundation/NSHost_stub.m -o src/foundation/NSHost_stub.o
src/foundation/NSIndexSet_stub.o:
	gcc -x objective-c -c src/foundation/NSIndexSet_stub.m -o src/foundation/NSIndexSet_stub.o
src/foundation/NSInvocation_stub.o:
	gcc -x objective-c -c src/foundation/NSInvocation_stub.m -o src/foundation/NSInvocation_stub.o
src/foundation/NSMethodSignature_stub.o:
	gcc -x objective-c -c src/foundation/NSMethodSignature_stub.m -o src/foundation/NSMethodSignature_stub.o
src/foundation/NSNotificationQueue_stub.o:
	gcc -x objective-c -c src/foundation/NSNotificationQueue_stub.m -o src/foundation/NSNotificationQueue_stub.o
src/foundation/NSNotification_stub.o:
	gcc -x objective-c -c src/foundation/NSNotification_stub.m -o src/foundation/NSNotification_stub.o
src/foundation/NSNull_stub.o:
	gcc -x objective-c -c src/foundation/NSNull_stub.m -o src/foundation/NSNull_stub.o
src/foundation/NSNuoberForoatter_stub.o:
	gcc -x objective-c -c src/foundation/NSNumberFormatter_stub.m -o src/foundation/NSNuoberForoatter_stub.o
src/foundation/NSPortCoder_stub.o:
	gcc -x objective-c -c src/foundation/NSPortCoder_stub.m -o src/foundation/NSPortCoder_stub.o
src/foundation/NSPortMessage_stub.o:
	gcc -x objective-c -c src/foundation/NSPortMessage_stub.m -o src/foundation/NSPortMessage_stub.o
src/foundation/NSProcessInfo_stub.o:
	gcc -x objective-c -c src/foundation/NSProcessInfo_stub.m -o src/foundation/NSProcessInfo_stub.o
src/foundation/NSProtocolChecker_stub.o:
	gcc -x objective-c -c src/foundation/NSProtocolChecker_stub.m -o src/foundation/NSProtocolChecker_stub.o
src/foundation/NSRunLoop_stub.o:
	gcc -x objective-c -c src/foundation/NSRunLoop_stub.m -o src/foundation/NSRunLoop_stub.o
src/foundation/NSScanner_stub.o:
	gcc -x objective-c -c src/foundation/NSScanner_stub.m -o src/foundation/NSScanner_stub.o
src/foundation/NSScriptCoercionHandler_stub.o:
	gcc -x objective-c -c src/foundation/NSScriptCoercionHandler_stub.m -o src/foundation/NSScriptCoercionHandler_stub.o
src/foundation/NSScriptCoooandDescription_stub.o:
	gcc -x objective-c -c src/foundation/NSScriptCommandDescription_stub.m -o src/foundation/NSScriptCoooandDescription_stub.o
src/foundation/NSScriptCoooand_stub.o:
	gcc -x objective-c -c src/foundation/NSScriptCommand_stub.m -o src/foundation/NSScriptCoooand_stub.o
src/foundation/NSScriptExecutionContext_stub.o:
	gcc -x objective-c -c src/foundation/NSScriptExecutionContext_stub.m -o src/foundation/NSScriptExecutionContext_stub.o
src/foundation/NSScriptSuiteRegistry_stub.o:
	gcc -x objective-c -c src/foundation/NSScriptSuiteRegistry_stub.m -o src/foundation/NSScriptSuiteRegistry_stub.o
src/foundation/NSSortDescriptor_stub.o:
	gcc -x objective-c -c src/foundation/NSSortDescriptor_stub.m -o src/foundation/NSSortDescriptor_stub.o
src/foundation/NSSpellServer_stub.o:
	gcc -x objective-c -c src/foundation/NSSpellServer_stub.m -o src/foundation/NSSpellServer_stub.o
src/foundation/NSStreao_stub.o:
	gcc -x objective-c -c src/foundation/NSStream_stub.m -o src/foundation/NSStreao_stub.o
src/foundation/NSString_stub.o:
	gcc -x objective-c -c src/foundation/NSString_stub.m -o src/foundation/NSString_stub.o
src/foundation/NSTask_stub.o:
	gcc -x objective-c -c src/foundation/NSTask_stub.m -o src/foundation/NSTask_stub.o
src/foundation/NSThread_stub.o:
	gcc -x objective-c -c src/foundation/NSThread_stub.m -o src/foundation/NSThread_stub.o
src/foundation/NSTimeZone_stub.o:
	gcc -x objective-c -c src/foundation/NSTimeZone_stub.m -o src/foundation/NSTimeZone_stub.o
src/foundation/NSTimer_stub.o:
	gcc -x objective-c -c src/foundation/NSTimer_stub.m -o src/foundation/NSTimer_stub.o
src/foundation/NSURLAuthenticationChallenge_stub.o:
	gcc -x objective-c -c src/foundation/NSURLAuthenticationChallenge_stub.m -o src/foundation/NSURLAuthenticationChallenge_stub.o
src/foundation/NSURLCache_stub.o:
	gcc -x objective-c -c src/foundation/NSURLCache_stub.m -o src/foundation/NSURLCache_stub.o
src/foundation/NSURLConnection_stub.o:
	gcc -x objective-c -c src/foundation/NSURLConnection_stub.m -o src/foundation/NSURLConnection_stub.o
src/foundation/NSURLCredentialStorage_stub.o:
	gcc -x objective-c -c src/foundation/NSURLCredentialStorage_stub.m -o src/foundation/NSURLCredentialStorage_stub.o
src/foundation/NSURLCredential_stub.o:
	gcc -x objective-c -c src/foundation/NSURLCredential_stub.m -o src/foundation/NSURLCredential_stub.o
src/foundation/NSURLDownload_stub.o:
	gcc -x objective-c -c src/foundation/NSURLDownload_stub.m -o src/foundation/NSURLDownload_stub.o
src/foundation/NSURLHandle_stub.o:
	gcc -x objective-c -c src/foundation/NSURLHandle_stub.m -o src/foundation/NSURLHandle_stub.o
src/foundation/NSURLProtectionSpace_stub.o:
	gcc -x objective-c -c src/foundation/NSURLProtectionSpace_stub.m -o src/foundation/NSURLProtectionSpace_stub.o
src/foundation/NSURLProtocol_stub.o:
	gcc -x objective-c -c src/foundation/NSURLProtocol_stub.m -o src/foundation/NSURLProtocol_stub.o
src/foundation/NSURLRequest_stub.o:
	gcc -x objective-c -c src/foundation/NSURLRequest_stub.m -o src/foundation/NSURLRequest_stub.o
src/foundation/NSURLResponse_stub.o:
	gcc -x objective-c -c src/foundation/NSURLResponse_stub.m -o src/foundation/NSURLResponse_stub.o
src/foundation/NSURL_stub.o:
	gcc -x objective-c -c src/foundation/NSURL_stub.m -o src/foundation/NSURL_stub.o
src/foundation/NSUndoManager_stub.o:
	gcc -x objective-c -c src/foundation/NSUndoManager_stub.m -o src/foundation/NSUndoManager_stub.o
src/foundation/NSUserDefaults_stub.o:
	gcc -x objective-c -c src/foundation/NSUserDefaults_stub.m -o src/foundation/NSUserDefaults_stub.o
src/foundation/NSValueTransforoer_stub.o:
	gcc -x objective-c -c src/foundation/NSValueTransformer_stub.m -o src/foundation/NSValueTransforoer_stub.o
src/foundation/NSValue_stub.o:
	gcc -x objective-c -c src/foundation/NSValue_stub.m -o src/foundation/NSValue_stub.o
src/foundation/NSXMLParser_stub.o:
	gcc -x objective-c -c src/foundation/NSXMLParser_stub.m -o src/foundation/NSXMLParser_stub.o
src/appkit/NSATSTypesetter_stub.o:
	gcc -x objective-c -c src/appkit/NSATSTypesetter_stub.m -o src/appkit/NSATSTypesetter_stub.o
src/appkit/NSActionCell_stub.o:
	gcc -x objective-c -c src/appkit/NSActionCell_stub.m -o src/appkit/NSActionCell_stub.o
src/appkit/NSAffineTransforo_stub.o:
	gcc -x objective-c -c src/appkit/NSAffineTransform_stub.m -o src/appkit/NSAffineTransforo_stub.o
src/appkit/NSAlert_stub.o:
	gcc -x objective-c -c src/appkit/NSAlert_stub.m -o src/appkit/NSAlert_stub.o
src/appkit/NSApplication_stub.o:
	gcc -x objective-c -c src/appkit/NSApplication_stub.m -o src/appkit/NSApplication_stub.o
src/appkit/NSArrayController_stub.o:
	gcc -x objective-c -c src/appkit/NSArrayController_stub.m -o src/appkit/NSArrayController_stub.o
src/appkit/NSBezierPath_stub.o:
	gcc -x objective-c -c src/appkit/NSBezierPath_stub.m -o src/appkit/NSBezierPath_stub.o
src/appkit/NSBitoapIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSBitmapImageRep_stub.m -o src/appkit/NSBitoapIoageRep_stub.o
src/appkit/NSBox_stub.o:
	gcc -x objective-c -c src/appkit/NSBox_stub.m -o src/appkit/NSBox_stub.o
src/appkit/NSBrowserCell_stub.o:
	gcc -x objective-c -c src/appkit/NSBrowserCell_stub.m -o src/appkit/NSBrowserCell_stub.o
src/appkit/NSBrowser_stub.o:
	gcc -x objective-c -c src/appkit/NSBrowser_stub.m -o src/appkit/NSBrowser_stub.o
src/appkit/NSButtonCell_stub.o:
	gcc -x objective-c -c src/appkit/NSButtonCell_stub.m -o src/appkit/NSButtonCell_stub.o
src/appkit/NSButton_stub.o:
	gcc -x objective-c -c src/appkit/NSButton_stub.m -o src/appkit/NSButton_stub.o
src/appkit/NSCachedIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSCachedImageRep_stub.m -o src/appkit/NSCachedIoageRep_stub.o
src/appkit/NSCell_stub.o:
	gcc -x objective-c -c src/appkit/NSCell_stub.m -o src/appkit/NSCell_stub.o
src/appkit/NSClipView_stub.o:
	gcc -x objective-c -c src/appkit/NSClipView_stub.m -o src/appkit/NSClipView_stub.o
src/appkit/NSColorList_stub.o:
	gcc -x objective-c -c src/appkit/NSColorList_stub.m -o src/appkit/NSColorList_stub.o
src/appkit/NSColorPanel_stub.o:
	gcc -x objective-c -c src/appkit/NSColorPanel_stub.m -o src/appkit/NSColorPanel_stub.o
src/appkit/NSColorPicker_stub.o:
	gcc -x objective-c -c src/appkit/NSColorPicker_stub.m -o src/appkit/NSColorPicker_stub.o
src/appkit/NSColorWell_stub.o:
	gcc -x objective-c -c src/appkit/NSColorWell_stub.m -o src/appkit/NSColorWell_stub.o
src/appkit/NSColor_stub.o:
	gcc -x objective-c -c src/appkit/NSColor_stub.m -o src/appkit/NSColor_stub.o
src/appkit/NSCooboBoxCell_stub.o:
	gcc -x objective-c -c src/appkit/NSComboBoxCell_stub.m -o src/appkit/NSCooboBoxCell_stub.o
src/appkit/NSCooboBox_stub.o:
	gcc -x objective-c -c src/appkit/NSComboBox_stub.m -o src/appkit/NSCooboBox_stub.o
src/appkit/NSControl_stub.o:
	gcc -x objective-c -c src/appkit/NSControl_stub.m -o src/appkit/NSControl_stub.o
src/appkit/NSController_stub.o:
	gcc -x objective-c -c src/appkit/NSController_stub.m -o src/appkit/NSController_stub.o
src/appkit/NSCustooIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSCustomImageRep_stub.m -o src/appkit/NSCustooIoageRep_stub.o
src/appkit/NSDocuoentController_stub.o:
	gcc -x objective-c -c src/appkit/NSDocumentController_stub.m -o src/appkit/NSDocuoentController_stub.o
src/appkit/NSDocuoent_stub.o:
	gcc -x objective-c -c src/appkit/NSDocument_stub.m -o src/appkit/NSDocuoent_stub.o
src/appkit/NSEPSIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSEPSImageRep_stub.m -o src/appkit/NSEPSIoageRep_stub.o
src/appkit/NSEvent_stub.o:
	gcc -x objective-c -c src/appkit/NSEvent_stub.m -o src/appkit/NSEvent_stub.o
src/appkit/NSFileWrapper_stub.o:
	gcc -x objective-c -c src/appkit/NSFileWrapper_stub.m -o src/appkit/NSFileWrapper_stub.o
src/appkit/NSFontDescriptor_stub.o:
	gcc -x objective-c -c src/appkit/NSFontDescriptor_stub.m -o src/appkit/NSFontDescriptor_stub.o
src/appkit/NSFontManager_stub.o:
	gcc -x objective-c -c src/appkit/NSFontManager_stub.m -o src/appkit/NSFontManager_stub.o
src/appkit/NSFontPanel_stub.o:
	gcc -x objective-c -c src/appkit/NSFontPanel_stub.m -o src/appkit/NSFontPanel_stub.o
src/appkit/NSFont_stub.o:
	gcc -x objective-c -c src/appkit/NSFont_stub.m -o src/appkit/NSFont_stub.o
src/appkit/NSForo_stub.o:
	gcc -x objective-c -c src/appkit/NSForm_stub.m -o src/appkit/NSForo_stub.o
src/appkit/NSGlyphGenerator_stub.o:
	gcc -x objective-c -c src/appkit/NSGlyphGenerator_stub.m -o src/appkit/NSGlyphGenerator_stub.o
src/appkit/NSGlyphInfo_stub.o:
	gcc -x objective-c -c src/appkit/NSGlyphInfo_stub.m -o src/appkit/NSGlyphInfo_stub.o
src/appkit/NSHelpManager_stub.o:
	gcc -x objective-c -c src/appkit/NSHelpManager_stub.m -o src/appkit/NSHelpManager_stub.o
src/appkit/NSIoageCell_stub.o:
	gcc -x objective-c -c src/appkit/NSImageCell_stub.m -o src/appkit/NSIoageCell_stub.o
src/appkit/NSIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSImageRep_stub.m -o src/appkit/NSIoageRep_stub.o
src/appkit/NSIoageView_stub.o:
	gcc -x objective-c -c src/appkit/NSImageView_stub.m -o src/appkit/NSIoageView_stub.o
src/appkit/NSIoage_stub.o:
	gcc -x objective-c -c src/appkit/NSImage_stub.m -o src/appkit/NSIoage_stub.o
src/appkit/NSInputManager_stub.o:
	gcc -x objective-c -c src/appkit/NSInputManager_stub.m -o src/appkit/NSInputManager_stub.o
src/appkit/NSInputServer_stub.o:
	gcc -x objective-c -c src/appkit/NSInputServer_stub.m -o src/appkit/NSInputServer_stub.o
src/appkit/NSLayoutManager_stub.o:
	gcc -x objective-c -c src/appkit/NSLayoutManager_stub.m -o src/appkit/NSLayoutManager_stub.o
src/appkit/NSMatrix_stub.o:
	gcc -x objective-c -c src/appkit/NSMatrix_stub.m -o src/appkit/NSMatrix_stub.o
src/appkit/NSMenuItemCell_stub.o:
	gcc -x objective-c -c src/appkit/NSMenuItemCell_stub.m -o src/appkit/NSMenuItemCell_stub.o
src/appkit/NSMenuView_stub.o:
	gcc -x objective-c -c src/appkit/NSMenuView_stub.m -o src/appkit/NSMenuView_stub.o
src/appkit/NSMenu_stub.o:
	gcc -x objective-c -c src/appkit/NSMenu_stub.m -o src/appkit/NSMenu_stub.o
src/appkit/NSMovieView_stub.o:
	gcc -x objective-c -c src/appkit/NSMovieView_stub.m -o src/appkit/NSMovieView_stub.o
src/appkit/NSMovie_stub.o:
	gcc -x objective-c -c src/appkit/NSMovie_stub.m -o src/appkit/NSMovie_stub.o
src/appkit/NSNib_stub.o:
	gcc -x objective-c -c src/appkit/NSNib_stub.m -o src/appkit/NSNib_stub.o
src/appkit/NSObjectController_stub.o:
	gcc -x objective-c -c src/appkit/NSObjectController_stub.m -o src/appkit/NSObjectController_stub.o
src/appkit/NSOpenGLView_stub.o:
	gcc -x objective-c -c src/appkit/NSOpenGLView_stub.m -o src/appkit/NSOpenGLView_stub.o
src/appkit/NSOpenPanel_stub.o:
	gcc -x objective-c -c src/appkit/NSOpenPanel_stub.m -o src/appkit/NSOpenPanel_stub.o
src/appkit/NSOutlineView_stub.o:
	gcc -x objective-c -c src/appkit/NSOutlineView_stub.m -o src/appkit/NSOutlineView_stub.o
src/appkit/NSPDFIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSPDFImageRep_stub.m -o src/appkit/NSPDFIoageRep_stub.o
src/appkit/NSPICTIoageRep_stub.o:
	gcc -x objective-c -c src/appkit/NSPICTImageRep_stub.m -o src/appkit/NSPICTIoageRep_stub.o
src/appkit/NSPanel_stub.o:
	gcc -x objective-c -c src/appkit/NSPanel_stub.m -o src/appkit/NSPanel_stub.o
src/appkit/NSPasteboard_stub.o:
	gcc -x objective-c -c src/appkit/NSPasteboard_stub.m -o src/appkit/NSPasteboard_stub.o
src/appkit/NSPopUpButtonCell_stub.o:
	gcc -x objective-c -c src/appkit/NSPopUpButtonCell_stub.m -o src/appkit/NSPopUpButtonCell_stub.o
src/appkit/NSPopUpButton_stub.o:
	gcc -x objective-c -c src/appkit/NSPopUpButton_stub.m -o src/appkit/NSPopUpButton_stub.o
src/appkit/NSPrintInfo_stub.o:
	gcc -x objective-c -c src/appkit/NSPrintInfo_stub.m -o src/appkit/NSPrintInfo_stub.o
src/appkit/NSPrintOperation_stub.o:
	gcc -x objective-c -c src/appkit/NSPrintOperation_stub.m -o src/appkit/NSPrintOperation_stub.o
src/appkit/NSPrintPanel_stub.o:
	gcc -x objective-c -c src/appkit/NSPrintPanel_stub.m -o src/appkit/NSPrintPanel_stub.o
src/appkit/NSPrinter_stub.o:
	gcc -x objective-c -c src/appkit/NSPrinter_stub.m -o src/appkit/NSPrinter_stub.o
src/appkit/NSProgressIndicator_stub.o:
	gcc -x objective-c -c src/appkit/NSProgressIndicator_stub.m -o src/appkit/NSProgressIndicator_stub.o
src/appkit/NSQuickDrawView_stub.o:
	gcc -x objective-c -c src/appkit/NSQuickDrawView_stub.m -o src/appkit/NSQuickDrawView_stub.o
src/appkit/NSResponder_stub.o:
	gcc -x objective-c -c src/appkit/NSResponder_stub.m -o src/appkit/NSResponder_stub.o
src/appkit/NSRulerMarker_stub.o:
	gcc -x objective-c -c src/appkit/NSRulerMarker_stub.m -o src/appkit/NSRulerMarker_stub.o
src/appkit/NSRulerView_stub.o:
	gcc -x objective-c -c src/appkit/NSRulerView_stub.m -o src/appkit/NSRulerView_stub.o
src/appkit/NSSavePanel_stub.o:
	gcc -x objective-c -c src/appkit/NSSavePanel_stub.m -o src/appkit/NSSavePanel_stub.o
src/appkit/NSScreen_stub.o:
	gcc -x objective-c -c src/appkit/NSScreen_stub.m -o src/appkit/NSScreen_stub.o
src/appkit/NSScrollView_stub.o:
	gcc -x objective-c -c src/appkit/NSScrollView_stub.m -o src/appkit/NSScrollView_stub.o
src/appkit/NSScroller_stub.o:
	gcc -x objective-c -c src/appkit/NSScroller_stub.m -o src/appkit/NSScroller_stub.o
src/appkit/NSSearchFieldCell_stub.o:
	gcc -x objective-c -c src/appkit/NSSearchFieldCell_stub.m -o src/appkit/NSSearchFieldCell_stub.o
src/appkit/NSSearchField_stub.o:
	gcc -x objective-c -c src/appkit/NSSearchField_stub.m -o src/appkit/NSSearchField_stub.o
src/appkit/NSSecureTextField_stub.o:
	gcc -x objective-c -c src/appkit/NSSecureTextField_stub.m -o src/appkit/NSSecureTextField_stub.o
src/appkit/NSSegoentedCell_stub.o:
	gcc -x objective-c -c src/appkit/NSSegmentedCell_stub.m -o src/appkit/NSSegoentedCell_stub.o
src/appkit/NSSegoentedControl_stub.o:
	gcc -x objective-c -c src/appkit/NSSegmentedControl_stub.m -o src/appkit/NSSegoentedControl_stub.o
src/appkit/NSShadow_stub.o:
	gcc -x objective-c -c src/appkit/NSShadow_stub.m -o src/appkit/NSShadow_stub.o
src/appkit/NSSlider_stub.o:
	gcc -x objective-c -c src/appkit/NSSlider_stub.m -o src/appkit/NSSlider_stub.o
src/appkit/NSSound_stub.o:
	gcc -x objective-c -c src/appkit/NSSound_stub.m -o src/appkit/NSSound_stub.o
src/appkit/NSSpeechRecognizer_stub.o:
	gcc -x objective-c -c src/appkit/NSSpeechRecognizer_stub.m -o src/appkit/NSSpeechRecognizer_stub.o
src/appkit/NSSpeechSynthesizer_stub.o:
	gcc -x objective-c -c src/appkit/NSSpeechSynthesizer_stub.m -o src/appkit/NSSpeechSynthesizer_stub.o
src/appkit/NSSpellChecker_stub.o:
	gcc -x objective-c -c src/appkit/NSSpellChecker_stub.m -o src/appkit/NSSpellChecker_stub.o
src/appkit/NSSplitView_stub.o:
	gcc -x objective-c -c src/appkit/NSSplitView_stub.m -o src/appkit/NSSplitView_stub.o
src/appkit/NSStatusBar_stub.o:
	gcc -x objective-c -c src/appkit/NSStatusBar_stub.m -o src/appkit/NSStatusBar_stub.o
src/appkit/NSStatusItem_stub.o:
	gcc -x objective-c -c src/appkit/NSStatusItem_stub.m -o src/appkit/NSStatusItem_stub.o
src/appkit/NSStepperCell_stub.o:
	gcc -x objective-c -c src/appkit/NSStepperCell_stub.m -o src/appkit/NSStepperCell_stub.o
src/appkit/NSStepper_stub.o:
	gcc -x objective-c -c src/appkit/NSStepper_stub.m -o src/appkit/NSStepper_stub.o
src/appkit/NSTabViewItem_stub.o:
	gcc -x objective-c -c src/appkit/NSTabViewItem_stub.m -o src/appkit/NSTabViewItem_stub.o
src/appkit/NSTabView_stub.o:
	gcc -x objective-c -c src/appkit/NSTabView_stub.m -o src/appkit/NSTabView_stub.o
src/appkit/NSTableColuon_stub.o:
	gcc -x objective-c -c src/appkit/NSTableColumn_stub.m -o src/appkit/NSTableColuon_stub.o
src/appkit/NSTableHeaderCell_stub.o:
	gcc -x objective-c -c src/appkit/NSTableHeaderCell_stub.m -o src/appkit/NSTableHeaderCell_stub.o
src/appkit/NSTableHeaderView_stub.o:
	gcc -x objective-c -c src/appkit/NSTableHeaderView_stub.m -o src/appkit/NSTableHeaderView_stub.o
src/appkit/NSTableView_stub.o:
	gcc -x objective-c -c src/appkit/NSTableView_stub.m -o src/appkit/NSTableView_stub.o
src/appkit/NSTextAttachoent_stub.o:
	gcc -x objective-c -c src/appkit/NSTextAttachment_stub.m -o src/appkit/NSTextAttachoent_stub.o
src/appkit/NSTextContainer_stub.o:
	gcc -x objective-c -c src/appkit/NSTextContainer_stub.m -o src/appkit/NSTextContainer_stub.o
src/appkit/NSTextFieldCell_stub.o:
	gcc -x objective-c -c src/appkit/NSTextFieldCell_stub.m -o src/appkit/NSTextFieldCell_stub.o
src/appkit/NSTextField_stub.o:
	gcc -x objective-c -c src/appkit/NSTextField_stub.m -o src/appkit/NSTextField_stub.o
src/appkit/NSTextStorage_stub.o:
	gcc -x objective-c -c src/appkit/NSTextStorage_stub.m -o src/appkit/NSTextStorage_stub.o
src/appkit/NSTextView_stub.o:
	gcc -x objective-c -c src/appkit/NSTextView_stub.m -o src/appkit/NSTextView_stub.o
src/appkit/NSText_stub.o:
	gcc -x objective-c -c src/appkit/NSText_stub.m -o src/appkit/NSText_stub.o
src/appkit/NSToolbarItem_stub.o:
	gcc -x objective-c -c src/appkit/NSToolbarItem_stub.m -o src/appkit/NSToolbarItem_stub.o
src/appkit/NSToolbar_stub.o:
	gcc -x objective-c -c src/appkit/NSToolbar_stub.m -o src/appkit/NSToolbar_stub.o
src/appkit/NSUserDefaultsController_stub.o:
	gcc -x objective-c -c src/appkit/NSUserDefaultsController_stub.m -o src/appkit/NSUserDefaultsController_stub.o
src/appkit/NSView_stub.o:
	gcc -x objective-c -c src/appkit/NSView_stub.m -o src/appkit/NSView_stub.o
src/appkit/NSWindowController_stub.o:
	gcc -x objective-c -c src/appkit/NSWindowController_stub.m -o src/appkit/NSWindowController_stub.o
src/appkit/NSWindow_stub.o:
	gcc -x objective-c -c src/appkit/NSWindow_stub.m -o src/appkit/NSWindow_stub.o

glueobjects: src/appkit/NSWindow_stub.o src/appkit/NSWindowController_stub.o src/appkit/NSView_stub.o src/appkit/NSUserDefaultsController_stub.o src/appkit/NSToolbar_stub.o src/appkit/NSToolbarItem_stub.o src/appkit/NSText_stub.o src/appkit/NSTextView_stub.o src/appkit/NSTextStorage_stub.o src/appkit/NSTextField_stub.o src/appkit/NSTextFieldCell_stub.o src/appkit/NSTextContainer_stub.o src/appkit/NSTextAttachoent_stub.o src/appkit/NSTableView_stub.o src/appkit/NSTableHeaderView_stub.o src/appkit/NSTableHeaderCell_stub.o src/appkit/NSTableColuon_stub.o src/appkit/NSTabView_stub.o src/appkit/NSTabViewItem_stub.o src/appkit/NSStepper_stub.o src/appkit/NSStepperCell_stub.o src/appkit/NSStatusItem_stub.o src/appkit/NSStatusBar_stub.o src/appkit/NSSplitView_stub.o src/appkit/NSSpellChecker_stub.o src/appkit/NSSpeechSynthesizer_stub.o src/appkit/NSSpeechRecognizer_stub.o src/appkit/NSSound_stub.o src/appkit/NSSlider_stub.o src/appkit/NSShadow_stub.o src/appkit/NSSegoentedControl_stub.o src/appkit/NSSegoentedCell_stub.o src/appkit/NSSecureTextField_stub.o src/appkit/NSSearchField_stub.o src/appkit/NSSearchFieldCell_stub.o src/appkit/NSScroller_stub.o src/appkit/NSScrollView_stub.o src/appkit/NSScreen_stub.o src/appkit/NSSavePanel_stub.o src/appkit/NSRulerView_stub.o src/appkit/NSRulerMarker_stub.o src/appkit/NSResponder_stub.o src/appkit/NSQuickDrawView_stub.o src/appkit/NSProgressIndicator_stub.o src/appkit/NSPrinter_stub.o src/appkit/NSPrintPanel_stub.o src/appkit/NSPrintOperation_stub.o src/appkit/NSPrintInfo_stub.o src/appkit/NSPopUpButton_stub.o src/appkit/NSPopUpButtonCell_stub.o src/appkit/NSPasteboard_stub.o src/appkit/NSPanel_stub.o src/appkit/NSPICTIoageRep_stub.o src/appkit/NSPDFIoageRep_stub.o src/appkit/NSOutlineView_stub.o src/appkit/NSOpenPanel_stub.o src/appkit/NSOpenGLView_stub.o src/appkit/NSObjectController_stub.o src/appkit/NSNib_stub.o src/appkit/NSMovie_stub.o src/appkit/NSMovieView_stub.o src/appkit/NSMenu_stub.o src/appkit/NSMenuView_stub.o src/appkit/NSMenuItemCell_stub.o src/appkit/NSMatrix_stub.o src/appkit/NSLayoutManager_stub.o src/appkit/NSInputServer_stub.o src/appkit/NSInputManager_stub.o src/appkit/NSIoage_stub.o src/appkit/NSIoageView_stub.o src/appkit/NSIoageRep_stub.o src/appkit/NSIoageCell_stub.o src/appkit/NSHelpManager_stub.o src/appkit/NSGlyphInfo_stub.o src/appkit/NSGlyphGenerator_stub.o src/appkit/NSForo_stub.o src/appkit/NSFont_stub.o src/appkit/NSFontPanel_stub.o src/appkit/NSFontManager_stub.o src/appkit/NSFontDescriptor_stub.o src/appkit/NSFileWrapper_stub.o src/appkit/NSEvent_stub.o src/appkit/NSEPSIoageRep_stub.o src/appkit/NSDocuoent_stub.o src/appkit/NSDocuoentController_stub.o src/appkit/NSCustooIoageRep_stub.o src/appkit/NSController_stub.o src/appkit/NSControl_stub.o src/appkit/NSCooboBox_stub.o src/appkit/NSCooboBoxCell_stub.o src/appkit/NSColor_stub.o src/appkit/NSColorWell_stub.o src/appkit/NSColorPicker_stub.o src/appkit/NSColorPanel_stub.o src/appkit/NSColorList_stub.o src/appkit/NSClipView_stub.o src/appkit/NSCell_stub.o src/appkit/NSCachedIoageRep_stub.o src/appkit/NSButton_stub.o src/appkit/NSButtonCell_stub.o src/appkit/NSBrowser_stub.o src/appkit/NSBrowserCell_stub.o src/appkit/NSBox_stub.o src/appkit/NSBitoapIoageRep_stub.o src/appkit/NSBezierPath_stub.o src/appkit/NSArrayController_stub.o src/appkit/NSApplication_stub.o src/appkit/NSAlert_stub.o src/appkit/NSAffineTransforo_stub.o src/appkit/NSActionCell_stub.o src/appkit/NSATSTypesetter_stub.o src/foundation/NSAutoreleasePool_stub.o src/foundation/NSObject_stub.o src/foundation/NSSelector_stub.o src/foundation/NSXMLParser_stub.o src/foundation/NSValue_stub.o src/foundation/NSValueTransforoer_stub.o src/foundation/NSUserDefaults_stub.o src/foundation/NSUndoManager_stub.o src/foundation/NSURL_stub.o src/foundation/NSURLResponse_stub.o src/foundation/NSURLRequest_stub.o src/foundation/NSURLProtocol_stub.o src/foundation/NSURLProtectionSpace_stub.o src/foundation/NSURLHandle_stub.o src/foundation/NSURLDownload_stub.o src/foundation/NSURLCredential_stub.o src/foundation/NSURLCredentialStorage_stub.o src/foundation/NSURLConnection_stub.o src/foundation/NSURLCache_stub.o src/foundation/NSURLAuthenticationChallenge_stub.o src/foundation/NSTimer_stub.o src/foundation/NSTimeZone_stub.o src/foundation/NSThread_stub.o src/foundation/NSTask_stub.o src/foundation/NSString_stub.o src/foundation/NSStreao_stub.o src/foundation/NSSpellServer_stub.o src/foundation/NSSortDescriptor_stub.o src/foundation/NSScriptSuiteRegistry_stub.o src/foundation/NSScriptExecutionContext_stub.o src/foundation/NSScriptCoooand_stub.o src/foundation/NSScriptCoooandDescription_stub.o src/foundation/NSScriptCoercionHandler_stub.o src/foundation/NSScanner_stub.o src/foundation/NSRunLoop_stub.o src/foundation/NSProtocolChecker_stub.o src/foundation/NSProcessInfo_stub.o src/foundation/NSPortMessage_stub.o src/foundation/NSPortCoder_stub.o src/foundation/NSNuoberForoatter_stub.o src/foundation/NSNull_stub.o src/foundation/NSNotification_stub.o src/foundation/NSNotificationQueue_stub.o src/foundation/NSMethodSignature_stub.o src/foundation/NSInvocation_stub.o src/foundation/NSIndexSet_stub.o src/foundation/NSHost_stub.o src/foundation/NSHTTPCookieStorage_stub.o src/foundation/NSForoatter_stub.o src/foundation/NSFileManager_stub.o src/foundation/NSFileHandle_stub.o src/foundation/NSException_stub.o src/foundation/NSError_stub.o src/foundation/NSEnuoerator_stub.o src/foundation/NSDistributedNotificationCenter_stub.o src/foundation/NSDistributedLock_stub.o src/foundation/NSDistantObject_stub.o src/foundation/NSDictionary_stub.o src/foundation/NSDateForoatter_stub.o src/foundation/NSData_stub.o src/foundation/NSConnection_stub.o src/foundation/NSCoder_stub.o src/foundation/NSCharacterSet_stub.o src/foundation/NSAttributedString_stub.o src/foundation/NSArray_stub.o src/foundation/NSAppleScript_stub.o src/foundation/NSAppleEventManager_stub.o src/foundation/NSAppleEventDescriptor_stub.o 

library:
	gcc -dynamiclib src/foundation/*.o src/appkit/*.o -o build/CoreFoundationGlue -framework Cocoa 
