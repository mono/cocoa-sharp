//
//  CSControl.h
//  SimpleWindow
//
//  Created by adhamh on Thu Jun 10 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import <Cocoa/Cocoa.h>
#import "CSWindow.h"
#import "CSMenu.h"
#import "CSBrowser.h"
#import "CSBrowserControl.h"
#import "AppController.h"

@interface CSControl : NSObject {

}
- (void)displayMainWindow;
+ (void)quit: (id) sender;
- (NSTextField *)displayTextField;
- (NSButton *)displayQuitButton;
//- (void) displayApplicationMenu;
- (NSButton *) displayBrowserButton;
- (void) displayFileBrowser;

@end
