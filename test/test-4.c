#import <Cocoa/Cocoa.h>
#import <objc/objc-class.h>

@interface ArgClass : NSObject {
}
@end

@implementation ArgClass
+(void)sampleFunction:(NSString *)val1 stringTwo:(NSString *)val2 {
	NSLog(@"val1=%s", val1);
	NSLog(@"val2=%s", val1);
}
@end
void callManagedDelegateWithArgs(char *methodToInvoke, void *(managedDelegate)(char *m, void *args)) {
	printf("NATIVE: callBackToManagedWithArgs(%s)\n", methodToInvoke);
	void * list = nil;
	char *type;
	NSString *string1 = [NSString stringWithCString:"foo"];
	NSString *string2 = [NSString stringWithCString:"bar"];
	int offset;
	
	printf("NATIVE: callBackToManagedWithArgs(%s):getClass\n", methodToInvoke);
	Class class = [[ArgClass init] class];
	if(class == nil)
		printf("class is nil\n");
	printf("NATIVE: callBackToManagedWithArgs(%s):getMethod\n", methodToInvoke);
	struct objc_method *method = class_getClassMethod(class, @selector(sampleFunction:stringTwo:));
	if(method == nil)
		printf("method is null\n");
	printf("NATIVE: callBackToManagedWithArgs(%s):alloc_margs\n", methodToInvoke);
	marg_malloc(list, method);
	printf("NATIVE: callBackToManagedWithArgs(%s):method_getArgumentInfo\n", methodToInvoke);
	method_getArgumentInfo(method, 0, &type, &offset);
	printf("OFFSET: %i\n", offset);
	id receiver = [ArgClass init];
	printf("NATIVE: callBackToManagedWithArgs(%s):method_setValue\n", methodToInvoke);
	marg_setValue(list, offset, id, receiver);
	method_getArgumentInfo(method, 1, &type, &offset);
	printf("OFFSET: %i\n", offset);
	printf("NATIVE: callBackToManagedWithArgs(%s):method_setValue\n", methodToInvoke);
	marg_setValue(list, offset, SEL, @selector(sampleFunction));
	method_getArgumentInfo(method, 2, &type, &offset);
	printf("OFFSET: %i\n", offset);
	printf("NATIVE: callBackToManagedWithArgs(%s):method_setValue\n", methodToInvoke);
	marg_setValue(list, offset, NSString *, string1);
	method_getArgumentInfo(method, 3, &type, &offset);
	printf("OFFSET: %i\n", offset);
	printf("NATIVE: callBackToManagedWithArgs(%s):method_setValue\n", methodToInvoke);
	marg_setValue(list, offset, NSString *, string2);
	
	NSString *bar = marg_getValue(list, offset, NSString *);
	printf("%i %s\n", offset, [bar cString]);
	printf("prearg_size: %i\n", marg_prearg_size);
	managedDelegate(methodToInvoke, list);
}
void callManagedDelegate(char *methodToInvoke, void *(managedDelegate)(char *m)) {
	printf("NATIVE: callBackToManaged(%s)\n", methodToInvoke);
	managedDelegate(methodToInvoke);
}

