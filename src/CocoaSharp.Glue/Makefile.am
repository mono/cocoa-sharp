all: ../../build/libCocoaSharpGlue.dylib

../../build/libCocoaSharpGlue.dylib: *.m
	gcc -dynamiclib -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libCocoaSharpGlue.dylib *.m

clean:
	rm -f ../../build/libCocoaSharpGlue.dylib
