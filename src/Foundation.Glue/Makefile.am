all: ../../build/libFoundationGlue.dylib

../../build/libFoundationGlue.dylib: *.m
	gcc -dynamiclib -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libFoundationGlue.dylib *.m

clean:
	rm -f ../../build/libFoundationGlue.dylib

install-data-local:
	if test -n '@prefix@/lib/libFoundationGlue.dylib'; then  cp ../../build/libFoundationGlue.dylib @prefix@/lib/libFoundationGlue.dylib; fi 
