/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSCoder.h>

BOOL NSPortCoder_isBycopy (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_isBycopy");
	return [THIS isBycopy];
}
BOOL NSPortCoder_isByref (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_isByref");
	return [THIS isByref];
}
NSConnection * NSPortCoder_connection (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_connection");
	return [THIS connection];
}
void NSPortCoder_encodePortObject (NSPortCoder* THIS, NSPort * aport) {
	NSLog(@"NSPortCoder_encodePortObject");
	[THIS encodePortObject:aport];
}

NSPort * NSPortCoder_decodePortObject (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_decodePortObject");
	return [THIS decodePortObject];
}
/* UNSUPPORTED: 
+ portCoderWithReceivePort:(NSPort *)rcvPort sendPort:(NSPort *)sndPort components:(NSArray *)comps;
 */

NSPortCoder * NSPortCoder_initWithReceivePort_sendPort_components (NSPortCoder* THIS, NSPort * rcvPort, NSPort * sndPort, NSArray * comps) {
	NSLog(@"NSPortCoder_initWithReceivePort_sendPort_components");
	return [THIS initWithReceivePort:rcvPort sendPort:sndPort components:comps];
}

void NSPortCoder_dispatch (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_dispatch");
	[THIS dispatch];
}
Class NSPortCoder_classForPortCoder (NSPortCoder* THIS) {
	NSLog(@"NSPortCoder_classForPortCoder");
	return [THIS classForPortCoder];
}
NSPortCoder * NSPortCoder_replacementObjectForPortCoder (NSPortCoder* THIS, NSPortCoder * coder) {
	NSLog(@"NSPortCoder_replacementObjectForPortCoder");
	return [THIS replacementObjectForPortCoder:coder];
}

NSPortCoder * NSPortCoder_alloc() {
	NSLog(@"NSPortCoder_alloc()");
	return [NSPortCoder alloc];
}
