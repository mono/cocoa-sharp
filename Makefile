all: build build/Loader build/Test.exe build/CoreFoundationGlue

build:
	mkdir build

build/Apple.Cocoa.Foundation.dll: build src/Apple.Cocoa.Foundation/*.cs
	mcs -g /out:build/Apple.Cocoa.Foundation.dll src/Apple.Cocoa.Foundation/*.cs -target:library

build/CoreFoundationGlue: build src/*.m
	gcc -g -x objective-c src/*.m -o build/CoreFoundationGlue -dynamiclib -framework Cocoa

build/Test.exe: build src/test/*.cs build/Apple.Cocoa.Foundation.dll
	mcs -g src/test/*.cs -out:build/Test.exe /r:build/Apple.Cocoa.Foundation.dll

build/Loader: build src/test/Loader.c
	gcc src/test/Loader.c -o build/Loader `pkg-config --cflags --libs mono`

debug: build/Test.exe build/CoreFoundationGlue
	cd build; gdb --args ./Loader

run: build/Loader build/Test.exe build/CoreFoundationGlue
	cd build; ./Loader Test.exe

clean:
	rm -f build/*
