SUBDIRS=..

all: Test.exe Loader

CoreFoundation: /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation CoreFoundation

Foundation: /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Foundation

libFoundationGlue.dylib: ../src/foundation/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libFoundationGlue.dylib ../src/foundation/*.o

libAppKitGlue.dylib: ../src/appkit/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libAppKitGlue.dylib ../src/appkit/*.o

Test.exe: Test.cs ../build/*.dll libAppKitGlue.dylib libFoundationGlue.dylib Foundation CoreFoundation
	mcs -g Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .

Loader: Loader.c
	gcc -g Loader.c -o Loader `pkg-config --cflags --libs mono`

run: Test.exe Loader
	./Loader Test.exe
