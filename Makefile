all:
	mcs -g /out:build/Apple.Cocoa.Foundation.dll src/Apple.Cocoa.Foundation/*.cs -target:library
	gcc -g -x objective-c src/*.m -o build/CoreFoundationGlue -dynamiclib -framework Cocoa
	mcs -g src/test/*.cs -out:build/Test.exe /r:build/Apple.Cocoa.Foundation.dll

build/Apple.Cocoa.Foundation.dll:
build/CoreFoundationGlue:
build/Test.exe: build/Apple.Cocoa.Foundation.dll

debug: build/Test.exe build/CoreFoundationGlue
	cd build; gdb --args mono Test.exe

run: build/Test.exe build/CoreFoundationGlue
	cd build; mono Test.exe
