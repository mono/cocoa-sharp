all: ../../build/Apple.AppKit.dll

../../build/Apple.AppKit.dll: *.cs ../../build/Apple.Foundation.dll
	mcs -nowarn:0108 -g *.cs -target:library -out:../../build/Apple.AppKit.dll -r:../../build/Apple.Foundation.dll

maintainer-clean: clean

clean:
	touch ../../build/Apple.AppKit.dll
	rm ../../build/Apple.AppKit.dll

