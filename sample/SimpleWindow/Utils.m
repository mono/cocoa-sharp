//
//  Utils.m
//  SimpleWindow
//
//  Created by adhamh on Tue Jun 08 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "Utils.h"


@implementation Utils
- (void) stopRunLoop: (id) sender

{
	[[NSRunLoop currentRunLoop] runUntilDate: [NSDate distantPast]];
	return;
}

- (void)runUntilStop;
{
    runLoopDone = NO;
    while ( runLoopDone == NO )
        [[NSRunLoop currentRunLoop] runMode:NSDefaultRunLoopMode beforeDate:[NSDate distantFuture]];  // run loop 'til event happens
}

- (void)stop;
{
	NSLog(@"Booo");
    runLoopDone = YES;
}

- (void)notifyDismiss:(NSNotification *) notification
{
	runLoopDone = YES; // signal
}

- (void)stopModal
{
	[[NSApplication sharedApplication] stopModal];
}


@end
