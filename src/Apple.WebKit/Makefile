all: ../../build/Apple.WebKit.dll

../../build/Apple.WebKit.dll: *.cs ../../build/Apple.Foundation.dll ../../build/Apple.AppKit.dll
	mcs -nowarn:0108 -g *.cs -target:library -out:../../build/Apple.WebKit.dll -r:../../build/Apple.AppKit.dll -r:../../build/Apple.Foundation.dll

maintainer-clean: clean

clean:
	touch ../../build/Apple.AppKit.dll
	rm ../../build/Apple.AppKit.dll

