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
#include <mono/metadata/tabledefs.h>
#include <mono/metadata/tokentype.h>
#include <mono/metadata/class.h>
#include <mono/metadata/object.h>
#include <mono/metadata/loader.h>

#include <string.h>

const char * sFile = "Test.exe";

int 
main(int argc, const char* argv[]) {
	MonoDomain *domain;
	MonoImage *image;
	MonoAssembly *assembly;
	int retval;

	char *cwd = getBundleDir();
	chdir(cwd);
	char *realFile = (char*)malloc(strlen(sFile)+strlen(cwd)+2);
	realFile = strcat(cwd, "/");
	if (argc >= 2 && memcmp(argv[1], "-psn", 4) != 0)
	{
		realFile = strcat(realFile, argv[1]);
	} else {
		realFile = strcat(realFile, sFile);
	}	
	printf("DEBUG:\n\tAssembly: %s\n", realFile);
	
	domain = mono_jit_init (realFile);

	if(domain == NULL) {
	    printf("ERROR: No domain for assembly: %s\n",realFile);
		exit(1);
	}

	assembly = mono_domain_assembly_open (domain,
					      realFile);

	if(assembly == NULL)
	{
	    printf("ERROR: Assembly load failed: %s\n",realFile);
		exit(1);
	}

	image = assembly->image;

	if(image == NULL)
	{
	    printf("ERROR: No assembly image: %s\n",realFile);
		exit(1);
	}

	// Start a pool before the assembly
	void *pool = BeginApp();	
	mono_jit_exec (domain, assembly, argc, (char**)argv);

	retval = mono_environment_exitcode_get ();
	// Clean up the pool before the jit
	EndApp(pool);
	
	// Clean up the JIT environment
	mono_jit_cleanup (domain);

	return retval;
}
