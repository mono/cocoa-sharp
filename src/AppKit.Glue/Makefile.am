all: ../../build/libAppKitGlue.dylib

../../build/libAppKitGlue.dylib: *.m
	gcc -dynamiclib -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libAppKitGlue.dylib *.m

clean:
	rm -f ../../build/libAppKitGlue.dylib

install-data-local:
	if test -n '@prefix@/lib/libAppKitGlue.dylib'; then cp ../../build/libAppKitGlue.dylib @prefix@/lib/libAppKitGlue.dylib; fi
