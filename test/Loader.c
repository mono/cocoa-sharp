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

#define JIT 1

#if JIT
#include <mono/jit/jit.h>
#else
#include <mono/interpreter/embed.h>
#endif
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
	
	if (argc >= 2)
		sFile = argv[1];
	
	printf("file: %s\n",sFile);
	
	/*
	 * mono_jit_init() creates a domain: each assembly is
	 * loaded and run in a MonoDomain.
	 */
#if JIT
	domain = mono_jit_init (sFile);
#else
	domain = mono_interp_init (sFile);
#endif

	if(domain == NULL) {
	    printf("ERROR: No domain for assembly: %s\n",sFile);
		exit(1);
	}

	/*
	 * We add our special internal call, so that C# code
	 * can call us back.
	 */
	assembly = mono_domain_assembly_open (domain,
					      sFile);

	if(assembly == NULL)
	{
	    printf("ERROR: Assembly load failed: %s\n",sFile);
		exit(1);
	}

	image = assembly->image;

	if(image == NULL)
	{
	    printf("ERROR: No assembly image: %s\n",sFile);
		exit(1);
	}

	mono_jit_exec (domain, assembly, argc, (char**)argv);

	retval = mono_environment_exitcode_get ();
	
#if JIT
	mono_jit_cleanup (domain);
#else
	mono_interp_cleanup (domain);
#endif
	return retval;
}
