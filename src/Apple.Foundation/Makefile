all: ../../build/Apple.Foundation.dll

../../build/Apple.Foundation.dll: *.cs
	mcs -nowarn:0108 -g *.cs -target:library -out:../../build/Apple.Foundation.dll

maintainer-clean: clean

clean:
	touch ../../build/Apple.Foundation.dll
	rm ../../build/Apple.Foundation.dll
