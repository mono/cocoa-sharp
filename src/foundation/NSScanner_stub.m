/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSString * NSScanner_string (NSScanner* THIS) {
	NSLog(@"NSScanner_string");
	return [THIS string];
}
unsigned NSScanner_scanLocation (NSScanner* THIS) {
	NSLog(@"NSScanner_scanLocation");
	return [THIS scanLocation];
}
void NSScanner_setScanLocation (NSScanner* THIS, unsigned pos) {
	NSLog(@"NSScanner_setScanLocation");
	[THIS setScanLocation:pos];
}

void NSScanner_setCharactersToBeSkipped (NSScanner* THIS, NSCharacterSet * set) {
	NSLog(@"NSScanner_setCharactersToBeSkipped");
	[THIS setCharactersToBeSkipped:set];
}

void NSScanner_setCaseSensitive (NSScanner* THIS, BOOL flag) {
	NSLog(@"NSScanner_setCaseSensitive");
	[THIS setCaseSensitive:flag];
}

void NSScanner_setLocale (NSScanner* THIS, NSDictionary * dict) {
	NSLog(@"NSScanner_setLocale");
	[THIS setLocale:dict];
}

NSCharacterSet * NSScanner_charactersToBeSkipped (NSScanner* THIS) {
	NSLog(@"NSScanner_charactersToBeSkipped");
	return [THIS charactersToBeSkipped];
}
BOOL NSScanner_caseSensitive (NSScanner* THIS) {
	NSLog(@"NSScanner_caseSensitive");
	return [THIS caseSensitive];
}
NSDictionary * NSScanner_locale (NSScanner* THIS) {
	NSLog(@"NSScanner_locale");
	return [THIS locale];
}
BOOL NSScanner_scanInt (NSScanner* THIS, int * value) {
	NSLog(@"NSScanner_scanInt");
	return [THIS scanInt:value];
}

BOOL NSScanner_scanHexInt (NSScanner* THIS, unsigned * value) {
	NSLog(@"NSScanner_scanHexInt");
	return [THIS scanHexInt:value];
}

BOOL NSScanner_scanLongLong (NSScanner* THIS, long long * value) {
	NSLog(@"NSScanner_scanLongLong");
	return [THIS scanLongLong:value];
}

BOOL NSScanner_scanFloat (NSScanner* THIS, float * value) {
	NSLog(@"NSScanner_scanFloat");
	return [THIS scanFloat:value];
}

BOOL NSScanner_scanDouble (NSScanner* THIS, double * value) {
	NSLog(@"NSScanner_scanDouble");
	return [THIS scanDouble:value];
}

BOOL NSScanner_scanString_intoString (NSScanner* THIS, NSString * string, NSString ** value) {
	NSLog(@"NSScanner_scanString_intoString");
	return [THIS scanString:string intoString:value];
}

BOOL NSScanner_scanCharactersFromSet_intoString (NSScanner* THIS, NSCharacterSet * set, NSString ** value) {
	NSLog(@"NSScanner_scanCharactersFromSet_intoString");
	return [THIS scanCharactersFromSet:set intoString:value];
}

BOOL NSScanner_scanUpToString_intoString (NSScanner* THIS, NSString * string, NSString ** value) {
	NSLog(@"NSScanner_scanUpToString_intoString");
	return [THIS scanUpToString:string intoString:value];
}

BOOL NSScanner_scanUpToCharactersFromSet_intoString (NSScanner* THIS, NSCharacterSet * set, NSString ** value) {
	NSLog(@"NSScanner_scanUpToCharactersFromSet_intoString");
	return [THIS scanUpToCharactersFromSet:set intoString:value];
}

BOOL NSScanner_isAtEnd (NSScanner* THIS) {
	NSLog(@"NSScanner_isAtEnd");
	return [THIS isAtEnd];
}
NSScanner * NSScanner_initWithString (NSScanner* THIS, NSString * string) {
	NSLog(@"NSScanner_initWithString");
	return [THIS initWithString:string];
}

NSScanner * NSScanner_scannerWithString (NSScanner* THIS, NSString * string) {
	NSLog(@"NSScanner_scannerWithString");
	return [THIS scannerWithString:string];
}

NSScanner * NSScanner_localizedScannerWithString (NSScanner* THIS, NSString * string) {
	NSLog(@"NSScanner_localizedScannerWithString");
	return [THIS localizedScannerWithString:string];
}

NSScanner * NSScanner_alloc() {
	NSLog(@"NSScanner_alloc()");
	return [NSScanner alloc];
}
