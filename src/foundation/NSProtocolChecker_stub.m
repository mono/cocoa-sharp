/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSProxy.h>

#include <Foundation/NSObject.h>

Protocol * NSProtocolChecker_protocol (NSProtocolChecker* THIS) {
	NSLog(@"NSProtocolChecker_protocol");
	return [THIS protocol];
}
NSObject * NSProtocolChecker_target (NSProtocolChecker* THIS) {
	NSLog(@"NSProtocolChecker_target");
	return [THIS target];
}
NSProtocolChecker * NSProtocolChecker_protocolCheckerWithTarget_protocol (NSProtocolChecker* THIS, NSObject * anObject, Protocol * aProtocol) {
	NSLog(@"NSProtocolChecker_protocolCheckerWithTarget_protocol");
	return [THIS protocolCheckerWithTarget:anObject protocol:aProtocol];
}

NSProtocolChecker * NSProtocolChecker_initWithTarget_protocol (NSProtocolChecker* THIS, NSObject * anObject, Protocol * aProtocol) {
	NSLog(@"NSProtocolChecker_initWithTarget_protocol");
	return [THIS initWithTarget:anObject protocol:aProtocol];
}

NSProtocolChecker * NSProtocolChecker_alloc() {
	NSLog(@"NSProtocolChecker_alloc()");
	return [NSProtocolChecker alloc];
}
