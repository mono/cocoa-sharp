//
//  CSBrowserControl.h
//  SimpleWindow
//
//  Created by Adhamh Findlay on Mon Jun 14 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import <Cocoa/Cocoa.h>
#import "FSNodeInfo.h"
#import "FSBrowserCell.h"


@interface CSBrowserControl : NSObject {
    NSBrowser    *fsBrowser;
    NSImageView  *nodeIconWell;  // Image well showing the selected items icon.
    NSTextField  *nodeInspector; // Text field showing the selected items attributes.
}

- (void)browser:(NSBrowser *)sender willDisplayCell:(id)cell atRow:(int)row column:(int)column;
- (int)browser:(NSBrowser *)sender numberOfRowsInColumn:(int)column;
- (void)setupBrowser: (NSBrowser *) fsBrowser;
- (id)reloadData:(id)sender;
- (NSString*)fsPathToColumn:(int)column;

	
@end
