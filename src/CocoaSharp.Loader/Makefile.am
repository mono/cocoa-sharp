INCLUDES= \
	@MONO_CFLAGS@

bin_PROGRAMS=CocoaSharpLoader
CocoaSharpLoader_SOURCES = \
	Bundle.m \
	Loader.c

Bundle.o: Bundle.m
	gcc -x objective-c -c Bundle.m

Loader.o: Loader.c
	gcc -c Loader.c @MONO_CFLAGS@

CocoaSharpLoader$(EXEEXT):
	gcc Bundle.o Loader.o -o CocoaSharpLoader -framework Cocoa @MONO_LIBS@
