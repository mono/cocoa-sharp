SUBDIRS=..

all: Test.exe 2ndTest.exe Loader test-4 test-4.exe

CoreFoundation: /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation CoreFoundation

Foundation: /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Foundation

libFoundationGlue.dylib: ../src/Foundation.Glue/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libFoundationGlue.dylib ../src/Foundation.Glue/*.o

libAppKitGlue.dylib: ../src/AppKit.Glue/*.o
	gcc -bundle -flat_namespace -undefined suppress -framework Cocoa -o libAppKitGlue.dylib ../src/AppKit.Glue/*.o

libGlue.so: glue.m
	gcc -g -bundle -flat_namespace -undefined suppress -framework Cocoa -o libGlue.so glue.m

Test.exe: test.cs ../build/*.dll libAppKitGlue.dylib libFoundationGlue.dylib Foundation CoreFoundation
	mcs -g Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .

2ndTest.exe: test-2.cs CSTextControl.cs CSControl.cs libGlue.so ../build/*.dll libAppKitGlue.dylib libFoundationGlue.dylib Foundation CoreFoundation
	mcs -g test-2.cs CSTextControl.cs CSControl.cs -out:2ndTest.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .

test-4: test-4.c
	gcc -g -x objective-c test-4.c -o test-4 -dynamiclib -framework Cocoa

test-4.exe: test-4.cs ../build/Apple.Foundation.dll
	mcs -g test-4.cs -r:../build/Apple.Foundation.dll
	cp ../build/*.dll .

Loader: Loader.o Bundle.o
	gcc Bundle.o Loader.o -o Loader -framework Cocoa `pkg-config --libs mono`

Loader.o: Loader.c
	gcc -g -c Loader.c -o Loader.o `pkg-config --cflags mono`

Bundle.o: Bundle.m
	gcc -x objective-c -g -c Bundle.m -o Bundle.o

run2: 2ndTest.exe Loader
	rm -Rf Loader.app
	mkdir -p Loader.app/Contents/MacOS
	mkdir -p Loader.app/Contents/Resources
	cp ../sample/SimpleWindow/*.icns Loader.app/Contents/Resources
	cp Info.plist Loader.app/Contents/
	cp 2ndTest.exe Loader.app/Contents/Resources
	cp *.dll* Loader.app/Contents/Resources
	cp *Glue* Loader.app/Contents/Resources
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation Loader.app/Contents/Resources/CoreFoundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Loader.app/Contents/Resources/Foundation
	cp Loader Loader.app/Contents/MacOS
	./Loader.app/Contents/MacOS/Loader 2ndTest.exe

debug2: 2ndTest.exe Loader
	rm -Rf Loader.app
	mkdir -p Loader.app/Contents/MacOS
	mkdir -p Loader.app/Contents/Resources
	cp ../sample/SimpleWindow/*.icns Loader.app/Contents/Resources
	cp Info.plist Loader.app/Contents/
	cp 2ndTest.exe Loader.app/Contents/Resources
	cp *.dll* Loader.app/Contents/Resources
	cp *Glue* Loader.app/Contents/Resources
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation Loader.app/Contents/Resources/CoreFoundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Loader.app/Contents/Resources/Foundation
	cp Loader Loader.app/Contents/MacOS
	gdb --args ./Loader.app/Contents/MacOS/Loader 2ndTest.exe

run: Test.exe Loader
	rm -Rf Loader.app
	mkdir -p Loader.app/Contents/MacOS
	mkdir -p Loader.app/Contents/Resources
	cp ../sample/SimpleWindow/*.icns Loader.app/Contents/Resources
	cp Info.plist Loader.app/Contents/
	cp mono.icns Loader.app/Contents/Resources
	cp Test.exe Loader.app/Contents/Resources
	cp *.dll* Loader.app/Contents/Resources
	cp *Glue* Loader.app/Contents/Resources
	ln -s /System/Library/Frameworks/CoreFoundation.framework/Versions/Current/CoreFoundation Loader.app/Contents/Resources/CoreFoundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Loader.app/Contents/Resources/Foundation
	cp Loader Loader.app/Contents/MacOS
	./Loader.app/Contents/MacOS/Loader

