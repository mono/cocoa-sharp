#import <objc/objc-class.h>
#import <Foundation/NSString.h>

Class CreateClassDefinition(const char * name, const char * superclassName) {
    NSLog(@"creating a subclass of %s named %s\n", superclassName, name);

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
    new_class->name = strdup(name);
    meta_class->name = new_class->name;

    //
    // Allocate empty method lists.
    // We can add methods later.
    //
    new_class->methodLists = (struct objc_method_list *)calloc( 1, sizeof(struct objc_method_list *) );
    *new_class->methodLists = -1;
    meta_class->methodLists = (struct objc_method_list *)calloc( 1, sizeof(struct objc_method_list *) );
    *meta_class->methodLists = -1;

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
    return new_class;
}
 
#if 0
struct objc_method_list *methodsToAdd = (struct objc_method_list *)
        malloc(totalMethodsToAdd * 
        sizeof(struct objc_method) + 
        sizeof(struct objc_method_list));
    methodsToAdd->method_count = totalMethodsToAdd;
    struct objc_method meth;
    meth = *class_getInstanceMethod(toGetMethodsFrom, @selector(before:));
    methodsToAdd->method_list[0] = meth;
    meth = *class_getInstanceMethod(toGetMethodsFrom, @selector(after:));
    methodsToAdd->method_list[1] = meth;

for( i = 2; i< totalMethodsToAdd; i++){
        //add identical method from the toWrap class
        struct objc_method *originalMeth = [mit nextMethod];        
        methodsToAdd->method_list[i].method_name = originalMeth->method_name;
        methodsToAdd->method_list[i].method_types = originalMeth->method_types;
        methodsToAdd->method_list[i].method_imp = originalMeth->method_imp;
        //replace methods in the toWrap class 
        meth = *class_getInstanceMethod(toGetMethodsFrom, [self getReplacementSEL: originalMeth]);
        originalMeth->method_imp = meth.method_imp;
    }
    class_addMethods(poser, methodsToAdd);
#endif
