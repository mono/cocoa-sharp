prefix=/Library/Frameworks/Mono.framework/Versions/1.1.1
SUBDIRS=..

all: monodoc

monodoc:
	rm -Rf monodoc.app
	mkdir -p monodoc.app/Contents/Resources
	mcs -debug -out:monodoc.app/Contents/Resources/Monodoc.exe browser.cs Contributions.cs XmlNodeWriter.cs catalog.cs -resource:./monodoc.png,monodoc.png -r:System.Web.Services.dll -pkg:cocoa-sharp -r:tmp/monodoc.dll
	mkdir -p monodoc.app/Contents/MacOS
	cp ../sample/SimpleWindow/*.icns monodoc.app/Contents/Resources
	cp monodoc.plist monodoc.app/Contents/Info.plist
	cp -R monodoc.nib monodoc.app/Contents/Resources
	cp $(prefix)/lib/lib*Glue*.dylib monodoc.app/Contents/Resources
	cp $(prefix)/bin/CocoaSharpLoader monodoc.app/Contents/MacOS/monodoc
	cp tmp/monodoc.dll monodoc.app/Contents/Resources
	cp -r tmp/sources monodoc.app/Contents/Resources/sources
	cp tmp/monodoc.xml monodoc.app/Contents/Resources

run:
	./monodoc.app/Contents/MacOS/monodoc

debug:
	gdb --args ./monodoc.app/Contents/MacOS/monodoc --debug Test.exe

debug-dump:
	GC_DUMP_REGULARLY=1 GC_PRINT_STATS=1 gdb --args ./monodoc.app/Contents/MacOS/monodoc --debug Test.exe

clean:
	rm -rf monodoc.app
	rm -f *.exe
