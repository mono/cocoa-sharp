//
//  CSBrowserControl.m
//  SimpleWindow
//
//  Created by Adhamh Findlay on Mon Jun 14 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSBrowserControl.h"

#define MAX_VISIBLE_COLUMNS 4


@implementation CSBrowserControl

- (void)browser:(NSBrowser *)sender willDisplayCell:(id)cell atRow:(int)row column:(int)column {
    NSString   *containingDirPath = nil;
    FSNodeInfo *containingDirNode = nil;
    FSNodeInfo *displayedCellNode = nil;
    NSArray    *directoryContents = nil;
    
    // Get the absolute path represented by the browser selection, and create a fsnode for the path.
    // Since (row,column) represents the cell being displayed, containingDirPath is the path to it's containing directory.
    containingDirPath = [self fsPathToColumn: column];
    containingDirNode = [FSNodeInfo nodeWithParent: nil atRelativePath: containingDirPath];
    
    // Ask the parent for a list of visible nodes so we can get at a FSNodeInfo for the cell being displayed.
    // Then give the FSNodeInfo to the cell so it can determine how to display itself.
    directoryContents = [containingDirNode visibleSubNodes];
    displayedCellNode = [directoryContents objectAtIndex: row];
    
    [cell setAttributedStringValueFromFSNodeInfo: displayedCellNode];
}

// Use lazy initialization, since we don't want to touch the file system too much.
- (int)browser:(NSBrowser *)sender numberOfRowsInColumn:(int)column {
    NSString   *fsNodePath = nil;
    FSNodeInfo *fsNodeInfo = nil;
    
    // Get the absolute path represented by the browser selection, and create a fsnode for the path.
    // Since column represents the column being (lazily) loaded fsNodePath is the path for the last selected cell.
    fsNodePath = [self fsPathToColumn: column];
    fsNodeInfo = [FSNodeInfo nodeWithParent: nil atRelativePath: fsNodePath];
    
    return [[fsNodeInfo visibleSubNodes] count];
}

- (NSString*)fsPathToColumn:(int)column {
    NSString *path = nil;
    if(column==0) path = [NSString stringWithFormat: @"/"];
    else path = [fsBrowser pathToColumn: column];
    return path;
}

- (void)setupBrowser: (NSBrowser *) browser {
    // Make the browser user our custom browser cell.
    [browser setCellClass: [FSBrowserCell class]];
	
    // Tell the browser to send us messages when it is clicked.
    [browser setTarget: self];
    [browser setAction: @selector(browserSingleClick:)];
    [browser setDoubleAction: @selector(browserDoubleClick:)];
    
    // Configure the number of visible columns (default max visible columns is 1).
    [browser setMaxVisibleColumns: MAX_VISIBLE_COLUMNS];
    [browser setMinColumnWidth: NSWidth([fsBrowser bounds])/(float)MAX_VISIBLE_COLUMNS];
	
    // Prime the browser with an initial load of data.
    [self reloadData: nil];
}

- (id)reloadData:(id)sender {
    [fsBrowser loadColumnZero];
	return fsBrowser;
}

@end
