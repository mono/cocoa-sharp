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

char *getBundleDir() {
	return [[[NSBundle mainBundle] resourcePath] cString];
}
