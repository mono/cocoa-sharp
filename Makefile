all: build/Test.exe build/CoreFoundationGlue

build/Apple.Cocoa.Foundation.dll: src/Apple.Cocoa.Foundation/*.cs
	mcs -g /out:build/Apple.Cocoa.Foundation.dll src/Apple.Cocoa.Foundation/*.cs -target:library

build/CoreFoundationGlue: src/*.m
	gcc -g -x objective-c src/*.m -o build/CoreFoundationGlue -dynamiclib -framework Cocoa

build/Test.exe: src/test/*.cs build/Apple.Cocoa.Foundation.dll
	mcs -g src/test/*.cs -out:build/Test.exe /r:build/Apple.Cocoa.Foundation.dll

debug: build/Test.exe build/CoreFoundationGlue
	cd build; gdb --args mono Test.exe

run: build/Test.exe build/CoreFoundationGlue
	cd build; mono Test.exe
