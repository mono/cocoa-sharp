SUBDIRS=..

all: Test.exe 2ndTest.exe Loader test-4 test-4.exe

CoreFoundation: /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation CoreFoundation

Foundation: /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Foundation

libFoundationGlue.dylib: ../src/foundation/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libFoundationGlue.dylib ../src/foundation/*.o

libAppKitGlue.dylib: ../src/appkit/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libAppKitGlue.dylib ../src/appkit/*.o

libGlue.so: glue.m
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libGlue.so glue.m

Test.exe: test.cs ../build/*.dll libAppKitGlue.dylib libFoundationGlue.dylib Foundation CoreFoundation
	mcs -g Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .

2ndTest.exe: test-2.cs CSControl.cs libGlue.so ../build/*.dll libAppKitGlue.dylib libFoundationGlue.dylib Foundation CoreFoundation
	mcs -g test-2.cs CSControl.cs -out:2ndTest.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .

test-4: test-4.c
	gcc -g -x objective-c test-4.c -o test-4 -dynamiclib -framework Cocoa

test-4.exe: test-4.cs ../build/Apple.Foundation.dll
	mcs -g test-4.cs -r:../build/Apple.Foundation.dll
	cp ../build/*.dll .

Loader: Loader.c
	gcc -g Loader.c -o Loader `pkg-config --cflags --libs mono`

run: Test.exe Loader
	./Loader Test.exe
