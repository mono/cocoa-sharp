/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

#include <Foundation/NSData.h>

void NSSerialization_serializeInt (NSSerialization* THIS, int value) {
	NSLog(@"NSSerialization_serializeInt");
	[THIS serializeInt:value];
}

void NSSerialization_serializeInts_count (NSSerialization* THIS, int * intBuffer, unsigned numInts) {
	NSLog(@"NSSerialization_serializeInts_count");
	[THIS serializeInts:intBuffer count:numInts];
}

void NSSerialization_serializeInt_atIndex (NSSerialization* THIS, int value, unsigned index) {
	NSLog(@"NSSerialization_serializeInt_atIndex");
	[THIS serializeInt:value atIndex:index];
}

void NSSerialization_serializeInts_count_atIndex (NSSerialization* THIS, int * intBuffer, unsigned numInts, unsigned index) {
	NSLog(@"NSSerialization_serializeInts_count_atIndex");
	[THIS serializeInts:intBuffer count:numInts atIndex:index];
}

void NSSerialization_serializeAlignedBytesLength (NSSerialization* THIS, unsigned length) {
	NSLog(@"NSSerialization_serializeAlignedBytesLength");
	[THIS serializeAlignedBytesLength:length];
}

int NSSerialization_deserializeIntAtIndex (NSSerialization* THIS, unsigned index) {
	NSLog(@"NSSerialization_deserializeIntAtIndex");
	return [THIS deserializeIntAtIndex:index];
}

void NSSerialization_deserializeInts_count_atIndex (NSSerialization* THIS, int * intBuffer, unsigned numInts, unsigned index) {
	NSLog(@"NSSerialization_deserializeInts_count_atIndex");
	[THIS deserializeInts:intBuffer count:numInts atIndex:index];
}

int NSSerialization_deserializeIntAtCursor (NSSerialization* THIS, unsigned * cursor) {
	NSLog(@"NSSerialization_deserializeIntAtCursor");
	return [THIS deserializeIntAtCursor:cursor];
}

void NSSerialization_deserializeInts_count_atCursor (NSSerialization* THIS, int * intBuffer, unsigned numInts, unsigned * cursor) {
	NSLog(@"NSSerialization_deserializeInts_count_atCursor");
	[THIS deserializeInts:intBuffer count:numInts atCursor:cursor];
}

unsigned NSSerialization_deserializeAlignedBytesLengthAtCursor (NSSerialization* THIS, unsigned * cursor) {
	NSLog(@"NSSerialization_deserializeAlignedBytesLengthAtCursor");
	return [THIS deserializeAlignedBytesLengthAtCursor:cursor];
}

void NSSerialization_deserializeBytes_length_atCursor (NSSerialization* THIS, void * buffer, unsigned bytes, unsigned * cursor) {
	NSLog(@"NSSerialization_deserializeBytes_length_atCursor");
	[THIS deserializeBytes:buffer length:bytes atCursor:cursor];
}

void NSSerialization_serializeObjectAt_ofObjCType_intoData (NSSerialization* THIS, NSSerialization * * object, const char * type, NSMutableData * data) {
	NSLog(@"NSSerialization_serializeObjectAt_ofObjCType_intoData");
	[THIS serializeObjectAt:object ofObjCType:type intoData:data];
}

void NSSerialization_deserializeObjectAt_ofObjCType_fromData_atCursor (NSSerialization* THIS, NSSerialization * * object, const char * type, NSData * data, unsigned * cursor) {
	NSLog(@"NSSerialization_deserializeObjectAt_ofObjCType_fromData_atCursor");
	[THIS deserializeObjectAt:object ofObjCType:type fromData:data atCursor:cursor];
}

/* UNSUPPORTED: 
- (void)serializeDataAt:(const void *)data ofObjCType:(const char *)type context:(id <NSObjCTypeSerializationCallBack>)callback;
 */

/* UNSUPPORTED: 
- (void)deserializeDataAt:(void *)data ofObjCType:(const char *)type atCursor:(unsigned *)cursor context:(id <NSObjCTypeSerializationCallBack>)callback;
 */

void NSSerialization_serializePropertyList_intoData (NSSerialization* THIS, NSSerialization * aPropertyList, NSMutableData * mdata) {
	NSLog(@"NSSerialization_serializePropertyList_intoData");
	[THIS serializePropertyList:aPropertyList intoData:mdata];
}

NSData * NSSerialization_serializePropertyList (NSSerialization* THIS, NSSerialization * aPropertyList) {
	NSLog(@"NSSerialization_serializePropertyList");
	return [THIS serializePropertyList:aPropertyList];
}

NSSerialization * NSSerialization_deserializePropertyListFromData_atCursor_mutableContainers (NSSerialization* THIS, NSData * data, unsigned * cursor, BOOL mut) {
	NSLog(@"NSSerialization_deserializePropertyListFromData_atCursor_mutableContainers");
	return [THIS deserializePropertyListFromData:data atCursor:cursor mutableContainers:mut];
}

NSSerialization * NSSerialization_deserializePropertyListLazilyFromData_atCursor_length_mutableContainers (NSSerialization* THIS, NSData * data, unsigned * cursor, unsigned length, BOOL mut) {
	NSLog(@"NSSerialization_deserializePropertyListLazilyFromData_atCursor_length_mutableContainers");
	return [THIS deserializePropertyListLazilyFromData:data atCursor:cursor length:length mutableContainers:mut];
}

NSSerialization * NSSerialization_deserializePropertyListFromData_mutableContainers (NSSerialization* THIS, NSData * serialization, BOOL mut) {
	NSLog(@"NSSerialization_deserializePropertyListFromData_mutableContainers");
	return [THIS deserializePropertyListFromData:serialization mutableContainers:mut];
}

NSSerialization * NSSerialization_alloc() {
	NSLog(@"NSSerialization_alloc()");
	return [NSSerialization alloc];
}
