/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSNetServices * NSNetServices_initWithDomain_type_name_port (NSNetServices* THIS, NSString * domain, NSString * type, NSString * name, int port) {
	NSLog(@"NSNetServices_initWithDomain_type_name_port");
	return [THIS initWithDomain:domain type:type name:name port:port];
}

NSNetServices * NSNetServices_initWithDomain_type_name (NSNetServices* THIS, NSString * domain, NSString * type, NSString * name) {
	NSLog(@"NSNetServices_initWithDomain_type_name");
	return [THIS initWithDomain:domain type:type name:name];
}

NSNetServices * NSNetServices_delegate (NSNetServices* THIS) {
	NSLog(@"NSNetServices_delegate");
	return [THIS delegate];
}
void NSNetServices_setDelegate (NSNetServices* THIS, NSNetServices * delegate) {
	NSLog(@"NSNetServices_setDelegate");
	[THIS setDelegate:delegate];
}

void NSNetServices_scheduleInRunLoop_forMode (NSNetServices* THIS, NSRunLoop * aRunLoop, NSString * mode) {
	NSLog(@"NSNetServices_scheduleInRunLoop_forMode");
	[THIS scheduleInRunLoop:aRunLoop forMode:mode];
}

void NSNetServices_removeFromRunLoop_forMode (NSNetServices* THIS, NSRunLoop * aRunLoop, NSString * mode) {
	NSLog(@"NSNetServices_removeFromRunLoop_forMode");
	[THIS removeFromRunLoop:aRunLoop forMode:mode];
}

NSString * NSNetServices_domain (NSNetServices* THIS) {
	NSLog(@"NSNetServices_domain");
	return [THIS domain];
}
NSString * NSNetServices_type (NSNetServices* THIS) {
	NSLog(@"NSNetServices_type");
	return [THIS type];
}
NSString * NSNetServices_name (NSNetServices* THIS) {
	NSLog(@"NSNetServices_name");
	return [THIS name];
}
NSString * NSNetServices_protocolSpecificInformation (NSNetServices* THIS) {
	NSLog(@"NSNetServices_protocolSpecificInformation");
	return [THIS protocolSpecificInformation];
}
void NSNetServices_setProtocolSpecificInformation (NSNetServices* THIS, NSString * specificInformation) {
	NSLog(@"NSNetServices_setProtocolSpecificInformation");
	[THIS setProtocolSpecificInformation:specificInformation];
}

NSArray * NSNetServices_addresses (NSNetServices* THIS) {
	NSLog(@"NSNetServices_addresses");
	return [THIS addresses];
}
void NSNetServices_publish (NSNetServices* THIS) {
	NSLog(@"NSNetServices_publish");
	[THIS publish];
}
void NSNetServices_resolve (NSNetServices* THIS) {
	NSLog(@"NSNetServices_resolve");
	[THIS resolve];
}
void NSNetServices_stop (NSNetServices* THIS) {
	NSLog(@"NSNetServices_stop");
	[THIS stop];
}
NSNetServices * NSNetServices_init (NSNetServices* THIS) {
	NSLog(@"NSNetServices_init");
	return [THIS init];
}
NSNetServices * NSNetServices_delegate (NSNetServices* THIS) {
	NSLog(@"NSNetServices_delegate");
	return [THIS delegate];
}
void NSNetServices_setDelegate (NSNetServices* THIS, NSNetServices * delegate) {
	NSLog(@"NSNetServices_setDelegate");
	[THIS setDelegate:delegate];
}

void NSNetServices_scheduleInRunLoop_forMode (NSNetServices* THIS, NSRunLoop * aRunLoop, NSString * mode) {
	NSLog(@"NSNetServices_scheduleInRunLoop_forMode");
	[THIS scheduleInRunLoop:aRunLoop forMode:mode];
}

void NSNetServices_removeFromRunLoop_forMode (NSNetServices* THIS, NSRunLoop * aRunLoop, NSString * mode) {
	NSLog(@"NSNetServices_removeFromRunLoop_forMode");
	[THIS removeFromRunLoop:aRunLoop forMode:mode];
}

void NSNetServices_searchForAllDomains (NSNetServices* THIS) {
	NSLog(@"NSNetServices_searchForAllDomains");
	[THIS searchForAllDomains];
}
void NSNetServices_searchForRegistrationDomains (NSNetServices* THIS) {
	NSLog(@"NSNetServices_searchForRegistrationDomains");
	[THIS searchForRegistrationDomains];
}
void NSNetServices_searchForServicesOfType_inDomain (NSNetServices* THIS, NSString * type, NSString * domainString) {
	NSLog(@"NSNetServices_searchForServicesOfType_inDomain");
	[THIS searchForServicesOfType:type inDomain:domainString];
}

void NSNetServices_stop (NSNetServices* THIS) {
	NSLog(@"NSNetServices_stop");
	[THIS stop];
}
void NSNetServices_netServiceWillPublish (NSNetServices* THIS, NSNetService * sender) {
	NSLog(@"NSNetServices_netServiceWillPublish");
	[THIS netServiceWillPublish:sender];
}

void NSNetServices_netServiceWillResolve (NSNetServices* THIS, NSNetService * sender) {
	NSLog(@"NSNetServices_netServiceWillResolve");
	[THIS netServiceWillResolve:sender];
}

void NSNetServices_netService_didNotPublish (NSNetServices* THIS, NSNetService * sender, NSDictionary * errorDict) {
	NSLog(@"NSNetServices_netService_didNotPublish");
	[THIS netService:sender didNotPublish:errorDict];
}

void NSNetServices_netServiceDidResolveAddress (NSNetServices* THIS, NSNetService * sender) {
	NSLog(@"NSNetServices_netServiceDidResolveAddress");
	[THIS netServiceDidResolveAddress:sender];
}

void NSNetServices_netService_didNotResolve (NSNetServices* THIS, NSNetService * sender, NSDictionary * errorDict) {
	NSLog(@"NSNetServices_netService_didNotResolve");
	[THIS netService:sender didNotResolve:errorDict];
}

void NSNetServices_netServiceDidStop (NSNetServices* THIS, NSNetService * sender) {
	NSLog(@"NSNetServices_netServiceDidStop");
	[THIS netServiceDidStop:sender];
}

void NSNetServices_netServiceBrowserWillSearch (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser) {
	NSLog(@"NSNetServices_netServiceBrowserWillSearch");
	[THIS netServiceBrowserWillSearch:aNetServiceBrowser];
}

void NSNetServices_netServiceBrowser_didFindDomain_moreComing (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser, NSString * domainString, BOOL moreComing) {
	NSLog(@"NSNetServices_netServiceBrowser_didFindDomain_moreComing");
	[THIS netServiceBrowser:aNetServiceBrowser didFindDomain:domainString moreComing:moreComing];
}

void NSNetServices_netServiceBrowser_didFindService_moreComing (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser, NSNetService * aNetService, BOOL moreComing) {
	NSLog(@"NSNetServices_netServiceBrowser_didFindService_moreComing");
	[THIS netServiceBrowser:aNetServiceBrowser didFindService:aNetService moreComing:moreComing];
}

void NSNetServices_netServiceBrowser_didNotSearch (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser, NSDictionary * errorDict) {
	NSLog(@"NSNetServices_netServiceBrowser_didNotSearch");
	[THIS netServiceBrowser:aNetServiceBrowser didNotSearch:errorDict];
}

void NSNetServices_netServiceBrowserDidStopSearch (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser) {
	NSLog(@"NSNetServices_netServiceBrowserDidStopSearch");
	[THIS netServiceBrowserDidStopSearch:aNetServiceBrowser];
}

void NSNetServices_netServiceBrowser_didRemoveDomain_moreComing (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser, NSString * domainString, BOOL moreComing) {
	NSLog(@"NSNetServices_netServiceBrowser_didRemoveDomain_moreComing");
	[THIS netServiceBrowser:aNetServiceBrowser didRemoveDomain:domainString moreComing:moreComing];
}

void NSNetServices_netServiceBrowser_didRemoveService_moreComing (NSNetServices* THIS, NSNetServiceBrowser * aNetServiceBrowser, NSNetService * aNetService, BOOL moreComing) {
	NSLog(@"NSNetServices_netServiceBrowser_didRemoveService_moreComing");
	[THIS netServiceBrowser:aNetServiceBrowser didRemoveService:aNetService moreComing:moreComing];
}

NSNetServices * NSNetServices_alloc() {
	NSLog(@"NSNetServices_alloc()");
	return [NSNetServices alloc];
}
