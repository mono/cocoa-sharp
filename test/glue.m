#import <objc/objc-class.h>
#import <Foundation/NSObject.h>
#import <Foundation/NSProxy.h>
#import <Foundation/NSString.h>
#import <Foundation/NSInvocation.h>

// static code: has to go
@interface _CSControl : NSObject {}
- (void) _stop;
- (void) _swap;
@end

@implementation _CSControl
- (void) _stop {}
- (void) _swap {}
@end
// end of static code


void AddMethods(Class cls,int count,...) {
    struct objc_method_list *methodsToAdd = (struct objc_method_list *)
        malloc(count*sizeof(struct objc_method) + sizeof(struct objc_method_list));
    methodsToAdd->method_count = count;

	int i;
	va_list vl;
	va_start(vl,count);
	for (i = 0; i < count; ++i) {
		SEL name = va_arg(vl,SEL);
		char *types = va_arg(vl,char *);
		IMP imp = va_arg(vl,IMP);
	
		struct objc_method *meth = methodsToAdd->method_list+i;
		meth->method_name = name;
		meth->method_types = types;
		meth->method_imp = imp;
	}

    class_addMethods(cls, methodsToAdd);
}

id Class_instanceMethodSignatureForSelector(Class CLASS, SEL aSelector) {
	return [CLASS instanceMethodSignatureForSelector: aSelector];
}

typedef id (*managedDelegate)(int what,id anInvocation);
#define GLUE_methodSignatureForSelector 0
#define GLUE_forwardInvocation 1

//- (id)initWithManagedDelegate:(managedDelegate)delegate
id glue_initWithManagedDelegate(id base, SEL sel, ...) {
    NSLog(@"glue_initWithManagedDelegate %@ %s", base, sel_getName(sel));

	va_list vl;
	va_start(vl,sel);
	managedDelegate delegate = va_arg(vl,managedDelegate);
	object_setInstanceVariable(base,"mDelegate",delegate);
	return base;
}

//- (NSMethodSignature *)methodSignatureForSelector:(SEL)aSelector
id glue_methodSignatureForSelector(id base, SEL sel, ...) {
	va_list vl;
	va_start(vl,sel);
	SEL aSelector = va_arg(vl,SEL);
	
    NSLog(@"glue_methodSignatureForSelector %p %s", base, sel_getName(aSelector));

	NSMethodSignature* signature = [[base superclass] instanceMethodSignatureForSelector: aSelector];
	
	if (!signature)
	{
		managedDelegate delegate;
		object_getInstanceVariable(base,"mDelegate",(void**)&delegate);

		signature = delegate(GLUE_methodSignatureForSelector,aSelector);
	}
	
	return signature;
}

//- (void)forwardInvocation:(NSInvocation *)anInvocation;
id glue_forwardInvocation(id base, SEL sel, ...) {
	va_list vl;
	va_start(vl,sel);
	NSInvocation * anInvocation = va_arg(vl,NSInvocation *);
	
    NSLog(@"glue_forwardInvocation: calling delegate %p %s", base, sel_getName([anInvocation selector]));
	
	managedDelegate delegate;
	object_getInstanceVariable(base,"mDelegate",(void**)&delegate);
	
	if (delegate(GLUE_forwardInvocation,anInvocation) != nil)
        [base doesNotRecognizeSelector: [anInvocation selector]];
	return base;
}

id DotNetForwarding_initWithManagedDelegate(id *THIS, managedDelegate delegate) {
	NSLog(@"DotNetForwarding_initWithManagedDelegate: %@",THIS);
	return glue_initWithManagedDelegate(THIS, @selector(initWithManagedDelegate:), delegate);
}


Class CreateClassDefinition(const char * name, const char * superclassName) {
    NSLog(@"creating a subclass of %s named %s", superclassName, name);

    //
    // Ensure that the superclass exists and that someone
    // hasn't already implemented a class with the same name
    //
    Class super_class = (Class)objc_lookUpClass (superclassName);
    if (super_class == nil)
        return nil;

    Class new_class = (Class)objc_lookUpClass (name);
    if (new_class != nil) 
        return new_class;

    // Find the root class
    Class root_class = (Class)super_class;
    while(root_class->super_class != nil)
        root_class = root_class->super_class;

    // Allocate space for the class and its metaclass
    new_class = (Class)calloc( 2, sizeof(struct objc_class) );
    Class meta_class = &new_class[1];

    // setup class
    new_class->isa      = meta_class;
    new_class->info     = CLS_CLASS;
    meta_class->info    = CLS_META;

    //
    // Create a copy of the class name.
    // For efficiency, we have the metaclass and the class itself 
    // to share this copy of the name, but this is not a requirement
    // imposed by the runtime.
    //
    new_class->name = (const char *)strdup(name);
    meta_class->name = new_class->name;

    //
    // Allocate empty method lists.
    // We can add methods later.
    //
    new_class->methodLists = (struct objc_method_list**)calloc( 1, sizeof(struct objc_method_list *) );
    *new_class->methodLists = (struct objc_method_list*)-1;
    meta_class->methodLists = (struct objc_method_list**)calloc( 1, sizeof(struct objc_method_list *) );
    *meta_class->methodLists = (struct objc_method_list*)-1;

    struct objc_ivar_list *ivarList = (struct objc_ivar_list *)calloc( 1, sizeof(struct objc_ivar_list) + (1-1)*sizeof(struct objc_ivar) );

    ivarList->ivar_count = 1;
    ivarList->ivar_list[0].ivar_name = "mDelegate";
    ivarList->ivar_list[0].ivar_type = @encode(managedDelegate);
    ivarList->ivar_list[0].ivar_offset = super_class->instance_size;

    new_class->instance_size = sizeof(managedDelegate);
    new_class->ivars = ivarList;

    //
    // Connect the class definition to the class hierarchy:
    // Connect the class to the superclass.
    // Connect the metaclass to the metaclass of the superclass.
    // Connect the metaclass of the metaclass to
    //      the metaclass of the root class.
    new_class->super_class  = super_class;
    meta_class->super_class = super_class->isa;
    meta_class->isa         = (void *)root_class->isa;

    // Finally, register the class with the runtime.
    objc_addClass( new_class );
	
    AddMethods(new_class, 3, 
			  @selector(initWithManagedDelegate:), "@12@0:4^?8", glue_initWithManagedDelegate,
			  @selector(methodSignatureForSelector:), "@12@0:4:8", glue_methodSignatureForSelector,
			  @selector(forwardInvocation:), "v12@0:4@8", glue_forwardInvocation);
    
    return new_class;
}


