all: ../../build/Apple.AppKit.dll

../../build/Apple.AppKit.dll: *.cs ../../build/Apple.Foundation.dll
	mcs -g *.cs -target:library -out:../../build/Apple.AppKit.dll -r:../../build/Apple.Foundation.dll

clean:
	rm ../../build/Apple.AppKit.dll
