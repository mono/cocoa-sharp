SUBDIRS=..

all: Test.exe Loader

Test.exe: Test.cs ../build/Apple.Foundation.dll ../build/Apple.AppKit.dll
	mcs -g Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .
	cp ../src/appkit/.libs/libAppKitGlue.dylib .
	cp ../src/foundation/.libs/libFoundationGlue.dylib .

Loader: Loader.c
	gcc -g Loader.c -o Loader `pkg-config --cflags --libs mono`
