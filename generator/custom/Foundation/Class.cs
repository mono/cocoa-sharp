using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation {
        public class Class : NSObject {
                /*
                void class_addMethods(Class aClass, struct objc_method_list* methodList);
                id class_createInstance(Class theClass, unsigned additionalByteCount);
                Method class_getClassMethod(Class aClass, SEL aSelector);
                Method class_getInstanceMethod(Class aClass, SEL aSelector);
                Ivar class_getInstanceVariable(Class aClass, const char* aVariableName);
                int class_getVersion(Class theClass);
                struct objc_method_list* class_nextMethodList(Class theClass, void** iterator);
                Class class_poseAs(Class imposter, Class original);
                void class_removeMethods(Class aClass, struct objc_method_list* methodList);
                void class_setVersion(Class theClass, int version);
                */

                #region -- Foundation --
                [DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
                protected static extern IntPtr /*(Class)*/ NSClassFromString(IntPtr /*(NSString*)*/ str);
                #endregion

                public static IntPtr Get(string className) {
                        return NSClassFromString(new NSString(className).Raw);
                }

                private Class() : this(IntPtr.Zero,false) {}

                protected internal Class(IntPtr raw,bool release) : base(raw,release) {}
                public Class(string name) : this(NSClassFromString(new NSString(name).Raw),false) {}

                public string Name {
                        get { return ClassName; }
                }
        }
}

