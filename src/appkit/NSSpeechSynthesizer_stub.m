/* Generated by genstubs.pl
 (c) 2004 kangaroo
*/

#include <Cocoa/Cocoa.h>

#include <AppKit/AppKitDefines.h>

#include <Foundation/NSObject.h>

#include <Foundation/NSRange.h>

NSSpeechSynthesizer * NSSpeechSynthesizer_initWithVoice (NSSpeechSynthesizer* THIS, NSString * voice) {
	NSLog(@"NSSpeechSynthesizer_initWithVoice");
	return [THIS initWithVoice:voice];
}

BOOL NSSpeechSynthesizer_startSpeakingString (NSSpeechSynthesizer* THIS, NSString * string) {
	NSLog(@"NSSpeechSynthesizer_startSpeakingString");
	return [THIS startSpeakingString:string];
}

BOOL NSSpeechSynthesizer_startSpeakingString_toURL (NSSpeechSynthesizer* THIS, NSString * string, NSURL * url) {
	NSLog(@"NSSpeechSynthesizer_startSpeakingString_toURL");
	return [THIS startSpeakingString:string toURL:url];
}

void NSSpeechSynthesizer_stopSpeaking (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_stopSpeaking");
	[THIS stopSpeaking];
}
BOOL NSSpeechSynthesizer_isSpeaking (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_isSpeaking");
	return [THIS isSpeaking];
}
NSSpeechSynthesizer * NSSpeechSynthesizer_delegate (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_delegate");
	return [THIS delegate];
}
void NSSpeechSynthesizer_setDelegate (NSSpeechSynthesizer* THIS, NSSpeechSynthesizer * anObject) {
	NSLog(@"NSSpeechSynthesizer_setDelegate");
	[THIS setDelegate:anObject];
}

NSString * NSSpeechSynthesizer_voice (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_voice");
	return [THIS voice];
}
BOOL NSSpeechSynthesizer_setVoice (NSSpeechSynthesizer* THIS, NSString * voice) {
	NSLog(@"NSSpeechSynthesizer_setVoice");
	return [THIS setVoice:voice];
}

BOOL NSSpeechSynthesizer_usesFeedbackWindow (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_usesFeedbackWindow");
	return [THIS usesFeedbackWindow];
}
void NSSpeechSynthesizer_setUsesFeedbackWindow (NSSpeechSynthesizer* THIS, BOOL flag) {
	NSLog(@"NSSpeechSynthesizer_setUsesFeedbackWindow");
	[THIS setUsesFeedbackWindow:flag];
}

BOOL NSSpeechSynthesizer_isAnyApplicationSpeaking (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_isAnyApplicationSpeaking");
	return [THIS isAnyApplicationSpeaking];
}
NSString * NSSpeechSynthesizer_defaultVoice (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_defaultVoice");
	return [THIS defaultVoice];
}
NSArray * NSSpeechSynthesizer_availableVoices (NSSpeechSynthesizer* THIS) {
	NSLog(@"NSSpeechSynthesizer_availableVoices");
	return [THIS availableVoices];
}
NSDictionary * NSSpeechSynthesizer_attributesForVoice (NSSpeechSynthesizer* THIS, NSString* voice) {
	NSLog(@"NSSpeechSynthesizer_attributesForVoice");
	return [THIS attributesForVoice:voice];
}

void NSSpeechSynthesizer_speechSynthesizer_didFinishSpeaking (NSSpeechSynthesizer* THIS, NSSpeechSynthesizer * sender, BOOL finishedSpeaking) {
	NSLog(@"NSSpeechSynthesizer_speechSynthesizer_didFinishSpeaking");
	[THIS speechSynthesizer:sender didFinishSpeaking:finishedSpeaking];
}

void NSSpeechSynthesizer_speechSynthesizer_willSpeakWord_ofString (NSSpeechSynthesizer* THIS, NSSpeechSynthesizer * sender, NSRange characterRange, NSString * string) {
	NSLog(@"NSSpeechSynthesizer_speechSynthesizer_willSpeakWord_ofString");
	[THIS speechSynthesizer:sender willSpeakWord:characterRange ofString:string];
}

void NSSpeechSynthesizer_speechSynthesizer_willSpeakPhoneme (NSSpeechSynthesizer* THIS, NSSpeechSynthesizer * sender, short phonemeOpcode) {
	NSLog(@"NSSpeechSynthesizer_speechSynthesizer_willSpeakPhoneme");
	[THIS speechSynthesizer:sender willSpeakPhoneme:phonemeOpcode];
}

NSSpeechSynthesizer * NSSpeechSynthesizer_alloc() {
	NSLog(@"NSSpeechSynthesizer_alloc()");
	return [NSSpeechSynthesizer alloc];
}
