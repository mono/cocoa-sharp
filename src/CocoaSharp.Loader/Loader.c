/*
 *  Loader.c
 *
 *  Created by Urs C Muff on Fri Feb 20 2004.
 *  Modified by Geoff Norton on Fri Jun 4 2004.
 *  Forked from MonoHelper.c in objc-sharp
 *  Changed to run all code at thread 0 to keep cocoa happy.
 *  Copyright (c) 2004 Quark Inc. All rights reserved.
 *
 */
#include <crt_externs.h>
#define environ (* _NSGetEnviron())

#include <mono/jit/jit.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/tokentype.h>
#include <mono/metadata/class.h>
#include <mono/metadata/object.h>
#include <mono/metadata/loader.h>

#include <string.h>

const char * sFile = "Test.exe";

int main(int argc, const char* argv[]) {
	char *realFile = (char*)malloc(1024);
	char *cwd = (char*)malloc(1024);
	void *pool = BeginApp(cwd);

	chdir(cwd);
	strcpy(realFile,cwd);
	strcat(realFile, "/");
	if (argc > 1 && strncmp(argv[argc-1], "-psn", 4) != 0)
		strcat(realFile, argv[argc-1]);
	else
		// TODO: search in resource folder to a .exe rather then assuming 'Test.exe'
		strcat(realFile, sFile);

	printf("DEBUG:\n\tAssembly: %s\n", realFile);
	
	MonoDomain *domain = mono_jit_init (realFile);

	if(domain == NULL) {
	    printf("ERROR: No domain for assembly: %s\n",realFile);
		exit(1);
	}

	MonoAssembly *assembly = mono_domain_assembly_open (domain,
		realFile);

	if(assembly == NULL) {
	    printf("ERROR: Assembly load failed: %s\n",realFile);
		exit(1);
	}

	MonoImage *image = mono_assembly_get_image(assembly);

	if(image == NULL) {
	    printf("ERROR: No assembly image: %s\n",realFile);
		exit(1);
	}

	mono_jit_exec (domain, assembly, argc, (char**)argv);

	int retval = mono_environment_exitcode_get ();
	// Clean up the pool before the jit
	
	// Clean up the JIT environment
	mono_jit_cleanup (domain);

	EndApp(pool);
	return retval;
}
