#include <crt_externs.h>
#define environ (* _NSGetEnviron())


#include <mono/jit/jit.h>
#include <stdio.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/tabledefs.h>
#include <mono/metadata/tokentype.h>
#include <mono/metadata/class.h>
#include <mono/metadata/object.h>
#include <mono/metadata/loader.h>
#include <mono/metadata/debug-helpers.h>

void printAddr(MonoObject* THIS) {
	printf("printAddr: %x %s\n", THIS, THIS->vtable->klass->name);
//	MonoMethodDesc* desc = mono_method_desc_new("Bar:GetHashCode()", FALSE);
//	if(desc == NULL)
//		printf("desc == null\n");
//	MonoClass* class = mono_object_class(THIS);
//	if(class == NULL)
//		printf("class == null\n");
//	MonoMethod *method = mono_method_desc_search_in_class(desc, class);
//	mono_runtime_invoke(method, THIS, NULL, NULL);
}
