SUBDIRS=..

all: Loader test test2 test-4 test-4.exe CocoaSharpBrowser CocoaSharpFinder

clean:
	rm -f *.dylib
	rm -f *.exe
	rm -f *.dll
	rm -f Loader
	rm -rf *.app
	rm -f test-4
	rm -f *.so
	rm -f *.o

test-4: test-4.c
	gcc -g -x objective-c test-4.c -o test-4 -dynamiclib -framework Cocoa

test-4.exe: test-4.cs
	mcs -g test-4.cs -r:../build/Apple.Foundation.dll

Loader: Loader.o Bundle.o
	gcc Bundle.o Loader.o -o Loader -framework Cocoa `pkg-config --libs mono`

Loader.o: Loader.c
	gcc -g -c Loader.c -o Loader.o `pkg-config --cflags mono`

Bundle.o: Bundle.m
	gcc -x objective-c -g -c Bundle.m -o Bundle.o

CocoaSharpFinder: CocoaSharpFinder.cs Loader
	rm -Rf CocoaSharpFinder.app
	mkdir -p CocoaSharpFinder.app/Contents/Resources
	mcs -g CocoaSharpFinder.cs -r:../build/Apple.AppKit.dll -r:../build/Apple.WebKit.dll -r:../build/Apple.Foundation.dll -out:CocoaSharpFinder.app/Contents/Resources/Test.exe
	mkdir -p CocoaSharpFinder.app/Contents/MacOS
	cp Loader CocoaSharpFinder.app/Contents/MacOS/CocoaSharpFinder
	cp ../sample/SimpleWindow/*.icns CocoaSharpFinder.app/Contents/Resources
	cp CocoaSharpFinder.plist CocoaSharpFinder.app/Contents/Info.plist
	cp ../build/*.dll* CocoaSharpFinder.app/Contents/Resources
	cp ../build/*Glue* CocoaSharpFinder.app/Contents/Resources

CocoaSharpBrowser: CocoaSharpBrowser.cs Loader
	rm -Rf CocoaSharpBrowser.app
	mkdir -p CocoaSharpBrowser.app/Contents/Resources
	mcs -g CocoaSharpBrowser.cs -r:../build/Apple.AppKit.dll -r:../build/Apple.WebKit.dll -r:../build/Apple.Foundation.dll -out:CocoaSharpBrowser.app/Contents/Resources/Test.exe
	mkdir -p CocoaSharpBrowser.app/Contents/MacOS
	cp Loader CocoaSharpBrowser.app/Contents/MacOS/CocoaSharpBrowser
	cp ../sample/SimpleWindow/*.icns CocoaSharpBrowser.app/Contents/Resources
	cp CocoaSharpBrowser.plist CocoaSharpBrowser.app/Contents/Info.plist
	cp ../build/*.dll* CocoaSharpBrowser.app/Contents/Resources
	cp ../build/*Glue* CocoaSharpBrowser.app/Contents/Resources

test2: test-2.cs CSTextControl.cs CSControl.cs Loader
	rm -Rf test2.app
	mkdir -p test2.app/Contents/Resources
	mcs -g test-2.cs CSTextControl.cs CSControl.cs -out:2ndTest.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll -out:test2.app/Contents/Resources/Test.exe
	mkdir -p test2.app/Contents/MacOS
	cp Loader test2.app/Contents/MacOS/Loader
	cp ../sample/SimpleWindow/*.icns test2.app/Contents/Resources
	cp Info.plist test2.app/Contents/
	cp ../build/*.dll* test2.app/Contents/Resources
	cp ../build/*Glue* test2.app/Contents/Resources

run2: test2
	./test2.app/Contents/MacOS/Loader

debug2: test2
	gdb --args ./test2.app/Contents/MacOS/Loader

test: test.cs
	rm -Rf test.app
	mkdir -p test.app/Contents/Resources
	mcs -g Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll -out:test.app/Contents/Resources/Test.exe
	mkdir -p test.app/Contents/MacOS
	cp Loader test.app/Contents/MacOS/Loader
	cp ../sample/SimpleWindow/*.icns test.app/Contents/Resources
	cp Info.plist test.app/Contents/
	cp ../build/*.dll* test.app/Contents/Resources
	cp ../build/*Glue* test.app/Contents/Resources

run: test
	./test.app/Contents/MacOS/Loader

