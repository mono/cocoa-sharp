all: ../../build/libAppKitGlue.dylib

../../build/libAppKitGlue.dylib: *.m
	gcc -dynamiclib -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libAppKitGlue.dylib *.m

clean:
	rm -f ../../build/libAppKitGlue.dylib
