//
//  Utils.h
//  SimpleWindow
//
//  Created by adhamh on Tue Jun 08 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import <Cocoa/Cocoa.h>


@interface Utils : NSObject {

}

static BOOL runLoopDone = NO;

- (void)runUntilStop;
- (void) stopRunLoop: (id) sender;
@end
