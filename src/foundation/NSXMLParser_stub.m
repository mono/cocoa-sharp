/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <Foundation/NSObject.h>

NSXMLParser * NSXMLParser_initWithContentsOfURL (NSXMLParser* THIS, NSURL * url) {
	NSLog(@"NSXMLParser_initWithContentsOfURL");
	return [THIS initWithContentsOfURL:url];
}

NSXMLParser * NSXMLParser_initWithData (NSXMLParser* THIS, NSData * data) {
	NSLog(@"NSXMLParser_initWithData");
	return [THIS initWithData:data];
}

NSXMLParser * NSXMLParser_delegate (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_delegate");
	return [THIS delegate];
}
void NSXMLParser_setDelegate (NSXMLParser* THIS, NSXMLParser * delegate) {
	NSLog(@"NSXMLParser_setDelegate");
	[THIS setDelegate:delegate];
}

void NSXMLParser_setShouldProcessNamespaces (NSXMLParser* THIS, BOOL shouldProcessNamespaces) {
	NSLog(@"NSXMLParser_setShouldProcessNamespaces");
	[THIS setShouldProcessNamespaces:shouldProcessNamespaces];
}

void NSXMLParser_setShouldReportNamespacePrefixes (NSXMLParser* THIS, BOOL shouldReportNamespacePrefixes) {
	NSLog(@"NSXMLParser_setShouldReportNamespacePrefixes");
	[THIS setShouldReportNamespacePrefixes:shouldReportNamespacePrefixes];
}

void NSXMLParser_setShouldResolveExternalEntities (NSXMLParser* THIS, BOOL shouldResolveExternalEntities) {
	NSLog(@"NSXMLParser_setShouldResolveExternalEntities");
	[THIS setShouldResolveExternalEntities:shouldResolveExternalEntities];
}

BOOL NSXMLParser_shouldProcessNamespaces (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_shouldProcessNamespaces");
	return [THIS shouldProcessNamespaces];
}
BOOL NSXMLParser_shouldReportNamespacePrefixes (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_shouldReportNamespacePrefixes");
	return [THIS shouldReportNamespacePrefixes];
}
BOOL NSXMLParser_shouldResolveExternalEntities (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_shouldResolveExternalEntities");
	return [THIS shouldResolveExternalEntities];
}
BOOL NSXMLParser_parse (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_parse");
	return [THIS parse];
}
void NSXMLParser_abortParsing (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_abortParsing");
	[THIS abortParsing];
}
NSError * NSXMLParser_parserError (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_parserError");
	return [THIS parserError];
}
NSString * NSXMLParser_publicID (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_publicID");
	return [THIS publicID];
}
NSString * NSXMLParser_systemID (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_systemID");
	return [THIS systemID];
}
int NSXMLParser_lineNumber (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_lineNumber");
	return [THIS lineNumber];
}
int NSXMLParser_columnNumber (NSXMLParser* THIS) {
	NSLog(@"NSXMLParser_columnNumber");
	return [THIS columnNumber];
}
void NSXMLParser_parserDidStartDocument (NSXMLParser* THIS, NSXMLParser * parser) {
	NSLog(@"NSXMLParser_parserDidStartDocument");
	[THIS parserDidStartDocument:parser];
}

void NSXMLParser_parserDidEndDocument (NSXMLParser* THIS, NSXMLParser * parser) {
	NSLog(@"NSXMLParser_parserDidEndDocument");
	[THIS parserDidEndDocument:parser];
}

void NSXMLParser_parser_foundNotationDeclarationWithName_publicID_systemID (NSXMLParser* THIS, NSXMLParser * parser, NSString * name, NSString * publicID, NSString * systemID) {
	NSLog(@"NSXMLParser_parser_foundNotationDeclarationWithName_publicID_systemID");
	[THIS parser:parser foundNotationDeclarationWithName:name publicID:publicID systemID:systemID];
}

void NSXMLParser_parser_foundUnparsedEntityDeclarationWithName_publicID_systemID_notationName (NSXMLParser* THIS, NSXMLParser * parser, NSString * name, NSString * publicID, NSString * systemID, NSString * notationName) {
	NSLog(@"NSXMLParser_parser_foundUnparsedEntityDeclarationWithName_publicID_systemID_notationName");
	[THIS parser:parser foundUnparsedEntityDeclarationWithName:name publicID:publicID systemID:systemID notationName:notationName];
}

void NSXMLParser_parser_foundAttributeDeclarationWithName_forElement_type_defaultValue (NSXMLParser* THIS, NSXMLParser * parser, NSString * attributeName, NSString * elementName, NSString * type, NSString * defaultValue) {
	NSLog(@"NSXMLParser_parser_foundAttributeDeclarationWithName_forElement_type_defaultValue");
	[THIS parser:parser foundAttributeDeclarationWithName:attributeName forElement:elementName type:type defaultValue:defaultValue];
}

void NSXMLParser_parser_foundElementDeclarationWithName_model (NSXMLParser* THIS, NSXMLParser * parser, NSString * elementName, NSString * model) {
	NSLog(@"NSXMLParser_parser_foundElementDeclarationWithName_model");
	[THIS parser:parser foundElementDeclarationWithName:elementName model:model];
}

void NSXMLParser_parser_foundInternalEntityDeclarationWithName_value (NSXMLParser* THIS, NSXMLParser * parser, NSString * name, NSString * value) {
	NSLog(@"NSXMLParser_parser_foundInternalEntityDeclarationWithName_value");
	[THIS parser:parser foundInternalEntityDeclarationWithName:name value:value];
}

void NSXMLParser_parser_foundExternalEntityDeclarationWithName_publicID_systemID (NSXMLParser* THIS, NSXMLParser * parser, NSString * name, NSString * publicID, NSString * systemID) {
	NSLog(@"NSXMLParser_parser_foundExternalEntityDeclarationWithName_publicID_systemID");
	[THIS parser:parser foundExternalEntityDeclarationWithName:name publicID:publicID systemID:systemID];
}

void NSXMLParser_parser_didStartElement_namespaceURI_qualifiedName_attributes (NSXMLParser* THIS, NSXMLParser * parser, NSString * elementName, NSString * namespaceURI, NSString * qName, NSDictionary * attributeDict) {
	NSLog(@"NSXMLParser_parser_didStartElement_namespaceURI_qualifiedName_attributes");
	[THIS parser:parser didStartElement:elementName namespaceURI:namespaceURI qualifiedName:qName attributes:attributeDict];
}

void NSXMLParser_parser_didEndElement_namespaceURI_qualifiedName (NSXMLParser* THIS, NSXMLParser * parser, NSString * elementName, NSString * namespaceURI, NSString * qName) {
	NSLog(@"NSXMLParser_parser_didEndElement_namespaceURI_qualifiedName");
	[THIS parser:parser didEndElement:elementName namespaceURI:namespaceURI qualifiedName:qName];
}

void NSXMLParser_parser_didStartMappingPrefix_toURI (NSXMLParser* THIS, NSXMLParser * parser, NSString * prefix, NSString * namespaceURI) {
	NSLog(@"NSXMLParser_parser_didStartMappingPrefix_toURI");
	[THIS parser:parser didStartMappingPrefix:prefix toURI:namespaceURI];
}

void NSXMLParser_parser_didEndMappingPrefix (NSXMLParser* THIS, NSXMLParser * parser, NSString * prefix) {
	NSLog(@"NSXMLParser_parser_didEndMappingPrefix");
	[THIS parser:parser didEndMappingPrefix:prefix];
}

void NSXMLParser_parser_foundCharacters (NSXMLParser* THIS, NSXMLParser * parser, NSString * string) {
	NSLog(@"NSXMLParser_parser_foundCharacters");
	[THIS parser:parser foundCharacters:string];
}

void NSXMLParser_parser_foundIgnorableWhitespace (NSXMLParser* THIS, NSXMLParser * parser, NSString * whitespaceString) {
	NSLog(@"NSXMLParser_parser_foundIgnorableWhitespace");
	[THIS parser:parser foundIgnorableWhitespace:whitespaceString];
}

void NSXMLParser_parser_foundProcessingInstructionWithTarget_data (NSXMLParser* THIS, NSXMLParser * parser, NSString * target, NSString * data) {
	NSLog(@"NSXMLParser_parser_foundProcessingInstructionWithTarget_data");
	[THIS parser:parser foundProcessingInstructionWithTarget:target data:data];
}

void NSXMLParser_parser_foundComment (NSXMLParser* THIS, NSXMLParser * parser, NSString * comment) {
	NSLog(@"NSXMLParser_parser_foundComment");
	[THIS parser:parser foundComment:comment];
}

void NSXMLParser_parser_foundCDATA (NSXMLParser* THIS, NSXMLParser * parser, NSData * CDATABlock) {
	NSLog(@"NSXMLParser_parser_foundCDATA");
	[THIS parser:parser foundCDATA:CDATABlock];
}

NSData * NSXMLParser_parser_resolveExternalEntityName_systemID (NSXMLParser* THIS, NSXMLParser * parser, NSString * name, NSString * systemID) {
	NSLog(@"NSXMLParser_parser_resolveExternalEntityName_systemID");
	return [THIS parser:parser resolveExternalEntityName:name systemID:systemID];
}

void NSXMLParser_parser_parseErrorOccurred (NSXMLParser* THIS, NSXMLParser * parser, NSError * parseError) {
	NSLog(@"NSXMLParser_parser_parseErrorOccurred");
	[THIS parser:parser parseErrorOccurred:parseError];
}

void NSXMLParser_parser_validationErrorOccurred (NSXMLParser* THIS, NSXMLParser * parser, NSError * validationError) {
	NSLog(@"NSXMLParser_parser_validationErrorOccurred");
	[THIS parser:parser validationErrorOccurred:validationError];
}

NSXMLParser * NSXMLParser_alloc() {
	NSLog(@"NSXMLParser_alloc()");
	return [NSXMLParser alloc];
}
