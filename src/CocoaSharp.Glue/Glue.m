#import <objc/objc-class.h>
#import <Foundation/NSObject.h>
#import <Foundation/NSString.h>
#import <Foundation/NSInvocation.h>
#import <Foundation/NSMethodSignature.h>
#import <AppKit/NSTextField.h>

#if false
@interface ConverterController : NSObject {
    id converter;
    NSTextField* dollarField; 
    NSTextField* rateField; 
    NSTextField* totalField; 
}

//- (void)convert: (id) sender;
@end

@implementation ConverterController
@end
#endif

// This is needed until bug #61033 is fixed
#define JIT_HACK 1

typedef id (*constructorDelegate)(id THIS, const char *className);
typedef id (*managedDelegate)(int what,id anInvocation);
typedef int (*classHandlerDelegate)(const char *className);
#if JIT_HACK
typedef managedDelegate (*getManagedDelegate)(id THIS);
#else
typedef void (*getManagedDelegate)(id THIS);
#endif
#define GLUE_methodSignatureForSelector 0
#define GLUE_forwardInvocation 1

constructorDelegate sConstructorDelegate = nil;
getManagedDelegate sGetManagedDelegate = nil;
BOOL sIsGlueVerbose = YES;

BOOL IsGlueVerbose() { return sIsGlueVerbose; }
void SetGlueVerbose(BOOL verbose) { sIsGlueVerbose = verbose; }

void SetConstructorDelegate(constructorDelegate _constructorDelegate,getManagedDelegate _getManagedDelegate) {
    //if (IsGlueVerbose())
        NSLog(@"GLUE: Setting delegates (%p,%p)", _constructorDelegate,_getManagedDelegate);
    sConstructorDelegate = _constructorDelegate;
    sGetManagedDelegate = _getManagedDelegate;
}

void InitGlue(classHandlerDelegate classHandler) {
    objc_setClassHandler(classHandler);
}

const char * GetObjectClassName(id THIS) {
    return [[THIS className] cString];
}

#if JIT_HACK
managedDelegate sJIT_HACK_Delegate;
void SetJIT_HACK_Delegate(managedDelegate delegate) {
    NSLog(@"GLUE: SetJIT_HACK_Delegate: %p", delegate);
    sJIT_HACK_Delegate = delegate;
}
#endif

managedDelegate GetDelegateForBase(id base) {
    NSLog(@"GLUE: GetDelegateForBase base=%@",base); 
    managedDelegate delegate = nil;
    object_getInstanceVariable(base,"mDelegate",(void**)&delegate);
    if (delegate == nil)
#if JIT_HACK
    {
        NSLog(@"   inst var == nil --> fetch delegate"); 
        sGetManagedDelegate(base); delegate = sJIT_HACK_Delegate; 
	}
#else
        delegate = sGetManagedDelegate(base);
#endif
    return delegate;
}

void AddMethods(Class cls,int numOfMethods,const char **methods,const char **signatures,IMP method,int count,...) {
    struct objc_method_list *methodsToAdd = (struct objc_method_list *)
        malloc((count+numOfMethods)*sizeof(struct objc_method) + sizeof(struct objc_method_list));
    methodsToAdd->method_count = count+numOfMethods;

    int i;
    struct objc_method *meth = methodsToAdd->method_list;

    for (i = 0; i < numOfMethods; ++i) {
        meth->method_name = sel_getUid(methods[i]);
        meth->method_types = (char*)strdup(signatures[i]);
        meth->method_imp = method;
        if (IsGlueVerbose())
            NSLog(@"  registering method: %s (%s) %p",sel_getName(meth->method_name),meth->method_types,meth->method_imp);
        ++meth;
    }

    va_list vl;
    va_start(vl,count);
    for (i = 0; i < count; ++i) {
        meth->method_name = va_arg(vl,SEL);
        meth->method_types = (char*)strdup(va_arg(vl,const char *));
        meth->method_imp = va_arg(vl,IMP);
        if (IsGlueVerbose())
            NSLog(@"  registering method: %s (%s) %p",sel_getName(meth->method_name),meth->method_types,meth->method_imp);
        ++meth;
    }

    class_addMethods(cls, methodsToAdd);
}

void AddInstanceVariables(Class cls,int count,...) {
    struct objc_ivar_list *ivarList = (struct objc_ivar_list *)calloc( 
        count, sizeof(struct objc_ivar_list) + (count-1)*sizeof(struct objc_ivar) );

    int offset = cls->super_class->instance_size;
    int size = 0;
    ivarList->ivar_count = count;

    int i;
    struct objc_ivar * var = ivarList->ivar_list;

    va_list vl;
    va_start(vl,count);
    for (i = 0; i < count; ++i) {
        
        var->ivar_name = va_arg(vl,char *);
        var->ivar_type = (char*)strdup(va_arg(vl,const char *));
        int ivarSize = va_arg(vl,int);
        size += ivarSize;
        var->ivar_offset = offset;
        offset += ivarSize;

        if (IsGlueVerbose())
            NSLog(@"  registering var: %s (%s) %i",var->ivar_name,var->ivar_type,var->ivar_offset);
        ++var;
    }

    cls->instance_size = size;
    cls->ivars = ivarList;
}

NSMethodSignature * MakeMethodSignature(const char *types) {
    NSMethodSignature *ret = [NSMethodSignature signatureWithObjCTypes: types];
    if (IsGlueVerbose())
        NSLog(@"GLUE: MakeMethodSignature %s --> %@",types,ret);
    return ret;
}

//- (id) initWithManagedDelegate: (managedDelegate) delegate
id glue_initWithManagedDelegate(id base, SEL sel, ...) {
    if (IsGlueVerbose())
        NSLog(@"GLUE: glue_initWithManagedDelegate %@ %s", base, sel_getName(sel));

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
    
    if (IsGlueVerbose())
        NSLog(@"GLUE: glue_methodSignatureForSelector %p %s", base, sel_getName(aSelector));

    NSMethodSignature* signature = [[base superclass] instanceMethodSignatureForSelector: aSelector];
    
    if (!signature && [strSel hasPrefix: @"_dotNet_"]) {
#if true
        managedDelegate delegate = GetDelegateForBase(base);

        aSelector = sel_getUid([[strSel substringFromIndex: 8] cString]);
        signature = (NSMethodSignature*)delegate(GLUE_methodSignatureForSelector,(id)aSelector);
#else
        signature = MakeMethodSignature([[strSel substringFromIndex: 8] cString]);
#endif
    }
    
    return signature;
}

id glue_initToManaged(id base, SEL sel, ...) {
    //if (IsGlueVerbose())
        NSLog(@"GLUE: glue_initToManaged (base=%@)",base);
    sConstructorDelegate(base,GetObjectClassName(base));
    return base;
}
    
//- (void) forwardInvocation: (NSInvocation *) anInvocation;
id glue_forwardInvocation(id base, SEL sel, ...) {
    va_list vl;
    va_start(vl,sel);
    NSInvocation * anInvocation = va_arg(vl,NSInvocation *);

    SEL aSelector = sel_getUid([[[NSString stringWithCString: sel_getName([anInvocation selector])] substringFromIndex: 8] cString]);
    [anInvocation setSelector: aSelector];

    if (IsGlueVerbose())
        NSLog(@"GLUE: glue_forwardInvocation: calling delegate %@ %s", base, sel_getName([anInvocation selector]));

    managedDelegate delegate = GetDelegateForBase(base);
    if (IsGlueVerbose())
        NSLog(@"GLUE: glue_forwardInvocation: base=%@ delegate=%p",base,delegate);

    if (delegate == nil || delegate(GLUE_forwardInvocation,anInvocation) != nil)
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

    const char* type;
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
    if (IsGlueVerbose())
        NSLog(@"GLUE: glue_implementMethod base=%@",base);
    for(i = 2; i < numArgs; i++) {
        // TODO: handle structures and non-4 byte argument types
        arg = va_arg(vl, void *);
        method_getArgumentInfo(method, i, &type, &offset);
        if (IsGlueVerbose())
            NSLog(@"    Getting arg: %i (type=%s inserting at: %i = %p", i, type, offset, arg);
        marg_setValue(margs, offset, void *, arg); 
    }
    if (IsGlueVerbose())
        NSLog(@"glue_implementMethod %@ %s (%s) method=%p margs=%p size=%i", base, sel_getName(forwardSel), sel_getName(method->method_name), method,margs,size);

    id ret;
    if(numArgs == 2)
        ret = (id)objc_msgSend(base, forwardSel);
    else
        ret = (id)objc_msgSendv(base, forwardSel, size, margs);
    marg_free(margs);
    return ret;
}

id DotNetForwarding_initWithManagedDelegate(id THIS, managedDelegate delegate) {
    if (IsGlueVerbose())
        NSLog(@"GLUE: DotNetForwarding_initWithManagedDelegate: %@",THIS);
    return glue_initWithManagedDelegate(THIS, @selector(initWithManagedDelegate:), delegate);
}

Class CreateClassDefinition(const char * name, const char * superclassName,int numOfMethods,const char **methods,const char **signatures) {
    //
    // Ensure that the superclass exists and that someone
    // hasn't already implemented a class with the same name
    //
    Class super_class = (Class)objc_lookUpClass (superclassName);
    if (super_class == nil)
        return nil;

    Class new_class = (Class)objc_lookUpClass (name);
    if (new_class == nil) {
        if (IsGlueVerbose())
            NSLog(@"GLUE: creating a subclass of %s named %s", superclassName, name);

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
        // Connect the class definition to the class hierarchy:
        // Connect the class to the superclass.
        // Connect the metaclass to the metaclass of the superclass.
        // Connect the metaclass of the metaclass to
        //      the metaclass of the root class.
        new_class->super_class  = super_class;
        meta_class->super_class = super_class->isa;
        meta_class->isa         = (void *)root_class->isa;
    
        //
        // Allocate empty method lists.
        // We can add methods later.
        //
        new_class->methodLists = (struct objc_method_list**)calloc( 1, sizeof(struct objc_method_list *) );
        *new_class->methodLists = (struct objc_method_list*)-1;
        meta_class->methodLists = (struct objc_method_list**)calloc( 1, sizeof(struct objc_method_list *) );
        *meta_class->methodLists = (struct objc_method_list*)-1;

        if (strcmp(name,"ConverterController") == 0)
            AddInstanceVariables(
                new_class, 5,
                "mDelegate", @encode(managedDelegate), sizeof(managedDelegate),
                "converter", @encode(id), sizeof(id), 
                "dollarField", @encode(NSTextField*), sizeof(NSTextField*),
                "rateField", @encode(NSTextField*), sizeof(NSTextField*),
                "totalField", @encode(NSTextField*), sizeof(NSTextField*)
            );
        else
            AddInstanceVariables(
                new_class, 1,
                "mDelegate", @encode(managedDelegate), sizeof(managedDelegate)
            );
    
        // Finally, register the class with the runtime.
        objc_addClass( new_class );

        AddMethods(new_class, 
                numOfMethods, methods, signatures, glue_implementMethod,
                4, 
                @selector(init), "@8@0:4", glue_initToManaged,
                @selector(initWithManagedDelegate:), "@12@0:4^?8", glue_initWithManagedDelegate,
                @selector(methodSignatureForSelector:), "@12@0:4:8", glue_methodSignatureForSelector,
                @selector(forwardInvocation:), "v12@0:4@8", glue_forwardInvocation);
    }
    else
        AddMethods(new_class, 
                numOfMethods, methods, signatures, glue_implementMethod,
                3, 
                @selector(init), "@8@0:4", glue_initToManaged,
                @selector(methodSignatureForSelector:), "@12@0:4:8", glue_methodSignatureForSelector,
                @selector(forwardInvocation:), "v12@0:4@8", glue_forwardInvocation);

    return new_class;
}
