#import <objc/objc-class.h>
#import <Foundation/NSObject.h>
#import <Foundation/NSString.h>
#import <Foundation/NSInvocation.h>
#import <Foundation/NSMethodSignature.h>


void AddMethods(Class cls,int numOfMethods,const char **methods,const char **signatures,IMP method,int count,...) {
    struct objc_method_list *methodsToAdd = (struct objc_method_list *)
        malloc((count+numOfMethods)*sizeof(struct objc_method) + sizeof(struct objc_method_list));
    methodsToAdd->method_count = count+numOfMethods;

    int i;
    struct objc_method *meth = methodsToAdd->method_list;

    for (i = 0; i < numOfMethods; ++i) {
        meth->method_name = sel_getUid(methods[i]);
        meth->method_types = strdup(signatures[i]);
        meth->method_imp = method;
        NSLog(@"  registering method: %s (%s) %p",sel_getName(meth->method_name),meth->method_types,meth->method_imp);
        ++meth;
    }

    va_list vl;
    va_start(vl,count);
    for (i = 0; i < count; ++i) {
        meth->method_name = va_arg(vl,SEL);
        meth->method_types = strdup(va_arg(vl,char *));
        meth->method_imp = va_arg(vl,IMP);
        NSLog(@"  registering method: %s (%s) %p",sel_getName(meth->method_name),meth->method_types,meth->method_imp);
        ++meth;
    }

    class_addMethods(cls, methodsToAdd);
}

NSMethodSignature * MakeMethodSignature(const char *types) {
    NSMethodSignature *ret = [NSMethodSignature signatureWithObjCTypes: types];
    NSLog(@"MakeMethodSignature %s --> %@",types,ret);
    return ret;
}

typedef id (*managedDelegate)(int what,id anInvocation);
#define GLUE_methodSignatureForSelector 0
#define GLUE_forwardInvocation 1

//- (id) initWithManagedDelegate: (managedDelegate) delegate
id glue_initWithManagedDelegate(id base, SEL sel, ...) {
    NSLog(@"glue_initWithManagedDelegate %@ %s", base, sel_getName(sel));

    va_list vl;
    va_start(vl,sel);
    managedDelegate delegate = va_arg(vl,managedDelegate);
    object_setInstanceVariable(base,"mDelegate",delegate);
    return base;
}

//- (NSMethodSignature *) methodSignatureForSelector: (SEL) aSelector
id glue_methodSignatureForSelector(id base, SEL sel, ...) {
    va_list vl;
    va_start(vl,sel);
    SEL aSelector = va_arg(vl,SEL);
    NSString *strSel = [NSString stringWithCString: sel_getName(aSelector)];
    
    NSLog(@"glue_methodSignatureForSelector %p %s", base, sel_getName(aSelector));

    NSMethodSignature* signature = [[base superclass] instanceMethodSignatureForSelector: aSelector];
    
    if (!signature && [strSel hasPrefix: @"_dotNet_"]) {
        managedDelegate delegate;
        object_getInstanceVariable(base,"mDelegate",(void**)&delegate);

        aSelector = sel_getUid([[strSel substringFromIndex: 8] cString]);
        signature = (NSMethodSignature*)delegate(GLUE_methodSignatureForSelector,(id)aSelector);
    }
    
    return signature;
}

//- (void) forwardInvocation: (NSInvocation *) anInvocation;
id glue_forwardInvocation(id base, SEL sel, ...) {
    va_list vl;
    va_start(vl,sel);
    NSInvocation * anInvocation = va_arg(vl,NSInvocation *);

    SEL aSelector = sel_getUid([[[NSString stringWithCString: sel_getName([anInvocation selector])] substringFromIndex: 8] cString]);
    [anInvocation setSelector: aSelector];

    NSLog(@"glue_forwardInvocation: calling delegate %p %s", base, sel_getName([anInvocation selector]));

    managedDelegate delegate;
    object_getInstanceVariable(base,"mDelegate",(void**)&delegate);

    if (delegate(GLUE_forwardInvocation,anInvocation) != nil)
        [base doesNotRecognizeSelector: [anInvocation selector]];
    return base;
}

id glue_implementMethod(id base, SEL sel, ...) {
    va_list vl;
    va_start(vl,sel);

    Method method = class_getInstanceMethod([base class], sel);
    int numArgs = method_getNumberOfArguments(method);
    int size = method_getSizeOfArguments(method);
    SEL forwardSel = sel_getUid([[NSString stringWithFormat: @"_dotNet_%s", sel_getName(sel)] cString]);
    marg_list margs;
    marg_malloc(margs, method);

    char *type;
    void *arg;
    int i, offset;
    // Add the id to the margs
    method_getArgumentInfo(method, 0, &type, &offset);
    marg_setValue(margs, offset, id, base);
    // Add the sel to the margs
    method_getArgumentInfo(method, 1, &type, &offset);
    marg_setValue(margs, offset, SEL, forwardSel);
    // Process the rest of the margs on the stack
    size = (numArgs)*4;
    for(i = 2; i < numArgs; i++) {
        arg = va_arg(vl, void *);
        method_getArgumentInfo(method, i, &type, &offset);
        NSLog(@"Getting arg: %i inserting at: %i", i, offset);
        marg_setValue(margs, offset, void *, arg); 
    }
    NSLog(@"glue_implementMethod %p %s (%s) method=%p margs=%p size=%i", base, sel_getName(forwardSel), sel_getName(method->method_name), method,margs,size);

    id ret;
    if(numArgs == 2)
        ret = (id)objc_msgSend(base, forwardSel);
    else
        ret = (id)objc_msgSendv(base, forwardSel, size, margs);
    marg_free(margs);
    return ret;
}

id DotNetForwarding_initWithManagedDelegate(id THIS, managedDelegate delegate) {
    NSLog(@"DotNetForwarding_initWithManagedDelegate: %@",THIS);
    return glue_initWithManagedDelegate(THIS, @selector(initWithManagedDelegate:), delegate);
}

Class CreateClassDefinition(const char * name, const char * superclassName,int numOfMethods,const char **methods,const char **signatures) {
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

    AddMethods(new_class, 
            numOfMethods, methods, signatures, glue_implementMethod,
            3, 
            @selector(initWithManagedDelegate:), "@12@0:4^?8", glue_initWithManagedDelegate,
            @selector(methodSignatureForSelector:), "@12@0:4:8", glue_methodSignatureForSelector,
            @selector(forwardInvocation:), "v12@0:4@8", glue_forwardInvocation);
    
    return new_class;
}

int GetInvocationArgumentSize(NSInvocation *invocation, int index) {
	return sizeof( [[invocation methodSignature] getArgumentTypeAtIndex:index] );
}
