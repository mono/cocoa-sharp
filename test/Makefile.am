prefix=/Library/Frameworks/Mono.framework/Versions/Current
SUBDIRS=..

all: test test2 test-4 test-4.exe CocoaSharpBrowser CocoaSharpFinder

clean:
	rm -f *.exe
	rm -rf *.app
	rm -f test-4

test-4: test-4.c
	gcc -g -x objective-c test-4.c -o test-4 -dynamiclib -framework Cocoa

test-4.exe: test-4.cs
	mcs -g test-4.cs -pkg:cocoa-sharp

CocoaSharpFinder: CocoaSharpFinder.cs
	rm -Rf CocoaSharpFinder.app
	mkdir -p CocoaSharpFinder.app/Contents/Resources
	mcs -g CocoaSharpFinder.cs -pkg:cocoa-sharp -out:CocoaSharpFinder.app/Contents/Resources/Test.exe
	mkdir -p CocoaSharpFinder.app/Contents/MacOS
	cp $(prefix)/bin/CocoaSharpLoader CocoaSharpFinder.app/Contents/MacOS/CocoaSharpFinder
	cp ../sample/SimpleWindow/*.icns CocoaSharpFinder.app/Contents/Resources
	cp CocoaSharpFinder.plist CocoaSharpFinder.app/Contents/Info.plist
	cp $(prefix)/lib/lib*Glue*.dylib CocoaSharpFinder.app/Contents/Resources

CocoaSharpBrowser: CocoaSharpBrowser.cs
	rm -Rf CocoaSharpBrowser.app
	mkdir -p CocoaSharpBrowser.app/Contents/Resources
	mcs -g CocoaSharpBrowser.cs -pkg:cocoa-sharp -out:CocoaSharpBrowser.app/Contents/Resources/Test.exe
	mkdir -p CocoaSharpBrowser.app/Contents/MacOS
	cp $(prefix)/bin/CocoaSharpLoader CocoaSharpBrowser.app/Contents/MacOS/CocoaSharpBrowser
	cp ../sample/SimpleWindow/*.icns CocoaSharpBrowser.app/Contents/Resources
	cp CocoaSharpBrowser.plist CocoaSharpBrowser.app/Contents/Info.plist
	cp $(prefix)/lib/lib*Glue*.dylib CocoaSharpBrowser.app/Contents/Resources

test2: test-2.cs CSTextControl.cs CSControl.cs
	rm -Rf test2.app
	mkdir -p test2.app/Contents/Resources
	mcs -g test-2.cs CSTextControl.cs CSControl.cs -out:2ndTest.exe -pkg:cocoa-sharp -out:test2.app/Contents/Resources/Test.exe
	mkdir -p test2.app/Contents/MacOS
	cp $(prefix)/CocoaSharpLoader test2.app/Contents/MacOS/Loader
	cp ../sample/SimpleWindow/*.icns test2.app/Contents/Resources
	cp Info.plist test2.app/Contents/
	cp $(prefix)/lib/lib*Glue*.dylib test2.app/Contents/Resources

test: test.cs
	rm -Rf test.app
	mkdir -p test.app/Contents/Resources
	mcs -g Test.cs -out:Test.exe -pkg:cocoa-sharp -out:test.app/Contents/Resources/Test.exe
	mkdir -p test.app/Contents/MacOS
	cp $(prefix)/CocoaSharpLoader test.app/Contents/MacOS/Loader
	cp ../sample/SimpleWindow/*.icns test.app/Contents/Resources
	cp Info.plist test.app/Contents/
	cp $(prefix)/lib/lib*Glue*.dylib test.app/Contents/Resources

