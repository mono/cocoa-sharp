all: ../../build/libCocoaSharpGlue.dylib

../../build/libCocoaSharpGlue.dylib: *.m
	gcc -g -bundle -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libCocoaSharpGlue.dylib *.m

clean:
	rm -f ../../build/libCocoaSharpGlue.dylib

install-data-local:
	if test -n '@prefix@/lib/libCocoaSharpGlue.dylib'; then cp ../../build/libCocoaSharpGlue.dylib @prefix@/lib/libCocoaSharpGlue.dylib; fi
