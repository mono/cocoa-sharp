//
//  AppController.m
//
//  Copyright (c) 2001-2002, Apple. All rights reserved.
//

#import <Cocoa/Cocoa.h>
#import "CSBrowser.h"

@interface AppController : NSObject {
@private
	NSBrowser    *fsBrowser;
    NSImageView  *nodeIconWell;  // Image well showing the selected items icon.
    NSTextField  *nodeInspector; // Text field showing the selected items attributes.
}

// Force a reload of column zero and thus, all the data.
- (void)reloadData:(id)sender;

// Methods sent by the browser to us from theBrowser.
- (void)browserSingleClick:(id)sender;
- (void)browserDoubleClick:(id)sender;

- (CSBrowser *)setupBrowser;
- (void)runUntilStop;
- (void)stopLoop;

@end
