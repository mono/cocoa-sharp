/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

#include <Foundation/NSArray.h>

#include <Foundation/NSRange.h>

NSData * NSFileHandle_availableData (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_availableData");
	return [THIS availableData];
}
NSData * NSFileHandle_readDataToEndOfFile (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_readDataToEndOfFile");
	return [THIS readDataToEndOfFile];
}
NSData * NSFileHandle_readDataOfLength (NSFileHandle* THIS, unsigned int length) {
	NSLog(@"NSFileHandle_readDataOfLength");
	return [THIS readDataOfLength:length];
}

void NSFileHandle_writeData (NSFileHandle* THIS, NSData * data) {
	NSLog(@"NSFileHandle_writeData");
	[THIS writeData:data];
}

unsigned long long NSFileHandle_offsetInFile (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_offsetInFile");
	return [THIS offsetInFile];
}
unsigned long long NSFileHandle_seekToEndOfFile (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_seekToEndOfFile");
	return [THIS seekToEndOfFile];
}
void NSFileHandle_seekToFileOffset (NSFileHandle* THIS, unsigned long long offset) {
	NSLog(@"NSFileHandle_seekToFileOffset");
	[THIS seekToFileOffset:offset];
}

void NSFileHandle_truncateFileAtOffset (NSFileHandle* THIS, unsigned long long offset) {
	NSLog(@"NSFileHandle_truncateFileAtOffset");
	[THIS truncateFileAtOffset:offset];
}

void NSFileHandle_synchronizeFile (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_synchronizeFile");
	[THIS synchronizeFile];
}
void NSFileHandle_closeFile (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_closeFile");
	[THIS closeFile];
}
NSFileHandle * NSFileHandle_fileHandleWithStandardInput (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleWithStandardInput");
	return [THIS fileHandleWithStandardInput];
}
NSFileHandle * NSFileHandle_fileHandleWithStandardOutput (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleWithStandardOutput");
	return [THIS fileHandleWithStandardOutput];
}
NSFileHandle * NSFileHandle_fileHandleWithStandardError (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleWithStandardError");
	return [THIS fileHandleWithStandardError];
}
NSFileHandle * NSFileHandle_fileHandleWithNullDevice (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleWithNullDevice");
	return [THIS fileHandleWithNullDevice];
}
NSFileHandle * NSFileHandle_fileHandleForReadingAtPath (NSFileHandle* THIS, NSString * path) {
	NSLog(@"NSFileHandle_fileHandleForReadingAtPath");
	return [THIS fileHandleForReadingAtPath:path];
}

NSFileHandle * NSFileHandle_fileHandleForWritingAtPath (NSFileHandle* THIS, NSString * path) {
	NSLog(@"NSFileHandle_fileHandleForWritingAtPath");
	return [THIS fileHandleForWritingAtPath:path];
}

NSFileHandle * NSFileHandle_fileHandleForUpdatingAtPath (NSFileHandle* THIS, NSString * path) {
	NSLog(@"NSFileHandle_fileHandleForUpdatingAtPath");
	return [THIS fileHandleForUpdatingAtPath:path];
}

void NSFileHandle_readInBackgroundAndNotifyForModes (NSFileHandle* THIS, NSArray * modes) {
	NSLog(@"NSFileHandle_readInBackgroundAndNotifyForModes");
	[THIS readInBackgroundAndNotifyForModes:modes];
}

void NSFileHandle_readInBackgroundAndNotify (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_readInBackgroundAndNotify");
	[THIS readInBackgroundAndNotify];
}
void NSFileHandle_readToEndOfFileInBackgroundAndNotifyForModes (NSFileHandle* THIS, NSArray * modes) {
	NSLog(@"NSFileHandle_readToEndOfFileInBackgroundAndNotifyForModes");
	[THIS readToEndOfFileInBackgroundAndNotifyForModes:modes];
}

void NSFileHandle_readToEndOfFileInBackgroundAndNotify (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_readToEndOfFileInBackgroundAndNotify");
	[THIS readToEndOfFileInBackgroundAndNotify];
}
void NSFileHandle_acceptConnectionInBackgroundAndNotifyForModes (NSFileHandle* THIS, NSArray * modes) {
	NSLog(@"NSFileHandle_acceptConnectionInBackgroundAndNotifyForModes");
	[THIS acceptConnectionInBackgroundAndNotifyForModes:modes];
}

void NSFileHandle_acceptConnectionInBackgroundAndNotify (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_acceptConnectionInBackgroundAndNotify");
	[THIS acceptConnectionInBackgroundAndNotify];
}
void NSFileHandle_waitForDataInBackgroundAndNotifyForModes (NSFileHandle* THIS, NSArray * modes) {
	NSLog(@"NSFileHandle_waitForDataInBackgroundAndNotifyForModes");
	[THIS waitForDataInBackgroundAndNotifyForModes:modes];
}

void NSFileHandle_waitForDataInBackgroundAndNotify (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_waitForDataInBackgroundAndNotify");
	[THIS waitForDataInBackgroundAndNotify];
}
NSFileHandle * NSFileHandle_initWithNativeHandle_closeOnDealloc (NSFileHandle* THIS, void * nativeHandle, BOOL closeopt) {
	NSLog(@"NSFileHandle_initWithNativeHandle_closeOnDealloc");
	return [THIS initWithNativeHandle:nativeHandle closeOnDealloc:closeopt];
}

NSFileHandle * NSFileHandle_initWithNativeHandle (NSFileHandle* THIS, void * nativeHandle) {
	NSLog(@"NSFileHandle_initWithNativeHandle");
	return [THIS initWithNativeHandle:nativeHandle];
}

void * NSFileHandle_nativeHandle (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_nativeHandle");
	[THIS nativeHandle];
}
NSFileHandle * NSFileHandle_initWithFileDescriptor_closeOnDealloc (NSFileHandle* THIS, int fd, BOOL closeopt) {
	NSLog(@"NSFileHandle_initWithFileDescriptor_closeOnDealloc");
	return [THIS initWithFileDescriptor:fd closeOnDealloc:closeopt];
}

NSFileHandle * NSFileHandle_initWithFileDescriptor (NSFileHandle* THIS, int fd) {
	NSLog(@"NSFileHandle_initWithFileDescriptor");
	return [THIS initWithFileDescriptor:fd];
}

int NSFileHandle_fileDescriptor (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileDescriptor");
	return [THIS fileDescriptor];
}
NSFileHandle * NSFileHandle_fileHandleForReading (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleForReading");
	return [THIS fileHandleForReading];
}
NSFileHandle * NSFileHandle_fileHandleForWriting (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_fileHandleForWriting");
	return [THIS fileHandleForWriting];
}
NSFileHandle * NSFileHandle_init (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_init");
	return [THIS init];
}
NSFileHandle * NSFileHandle_pipe (NSFileHandle* THIS) {
	NSLog(@"NSFileHandle_pipe");
	return [THIS pipe];
}
NSFileHandle * NSFileHandle_alloc() {
	NSLog(@"NSFileHandle_alloc()");
	return [NSFileHandle alloc];
}
