//
//  CSWindow.m
//  SimpleWindow
//
//  Created by adhamh on Wed Jun 09 2004.
//  Copyright (c) 2004 __MyCompanyName__. All rights reserved.
//

#import "CSWindow.h"


@implementation CSWindow
- (BOOL)canBecomeKeyWindow
{
	return YES;
}


- (BOOL)canBecomeMainWindow
{
	return YES;
}
- (void)stop 
{
	[self orderFront:nil];
	[NSApp terminate:nil];
}
@end
