/*
 *  Bundle.m
 *
 *  Created by Urs C Muff on Fri Feb 20 2004.
 *  Modified by Geoff Norton on Fri Jun 4 2004.
 *  Forked from MonoHelper.c in objc-sharp
 *  Changed to run all code at thread 0 to keep cocoa happy.
 *  Copyright (c) 2004 Quark Inc. All rights reserved.
 *
 */
#import <Foundation/NSBundle.h>
#import <Foundation/NSString.h>
#import <Foundation/NSAutoreleasePool.h>
#import <AppKit/NSApplication.h>

void * BeginApp() {
	void * pool = [[NSAutoreleasePool alloc] init];
	[NSApplication sharedApplication];
	return pool;
}

void EndApp(void * pool) {
	[(id)pool release];
}

char *getBundleDir() {
	// Create a pool to clean up after us
	void * pool = [[NSAutoreleasePool alloc] init];
	char * dir = [[[NSBundle mainBundle] resourcePath] cString];
	// Release the pool so it doesn't fight with mono / loader stuff
	return dir;
}
