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

@interface CSControl : NSObject {

}
- (void)displayWindow;
+ (void)stop;
- (NSTextField *)displayTextField;
- (NSButton *)displayButton;
- (void) displayMenu;

@end
