SUBDIRS=..

all: Test.exe Loader

Test.exe:
	mcs Test.cs -out:Test.exe -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll
	cp ../build/*.dll .
	cp ../src/appkit/.libs/libAppKitGlue.dylib .
	cp ../src/appkit/.libs/libFoundationGlue.dylib .

Loader:
	gcc Loader.c -o Loader `pkg-config --cflags --libs mono`
