SUBDIRS=

all: monodoc

monodoc:
	mcs -debug -out:browser.exe browser.cs Contributions.cs XmlNodeWriter.cs catalog.cs -resource:./monodoc.png,monodoc.png -r:System.Web.Services.dll -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll -r:../build/Apple.WebKit.dll -r:./monodoc.dll
	rm -Rf monodoc.app
	mkdir -p monodoc.app/Contents/MacOS
	mkdir -p monodoc.app/Contents/Resources
	cp ../sample/SimpleWindow/*.icns monodoc.app/Contents/Resources
	cp monodoc.plist monodoc.app/Contents/Info.plist
	cp browser.exe monodoc.app/Contents/Resources/Test.exe
	cp ../test/*.dll* monodoc.app/Contents/Resources
	cp ../test/*Glue* monodoc.app/Contents/Resources
	cp ../test/Loader monodoc.app/Contents/MacOS/monodoc
	cp monodoc.dll monodoc.app/Contents/Resources
	cp -r /usr/local/mono/lib/monodoc/sources monodoc.app/Contents/Resources/sources
	cp /usr/local/mono/lib/monodoc/monodoc.xml monodoc.app/Contents/Resources

run:
	GC_DONT_GC=1 ./monodoc.app/Contents/MacOS/monodoc

run-gc:
	./monodoc.app/Contents/MacOS/monodoc

debug:
	gdb --args ./monodoc.app/Contents/MacOS/monodoc

clean:
	rm -rf monodoc.app
	rm -f *.exe
