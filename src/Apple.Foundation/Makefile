all: ../../build/Apple.Foundation.dll

../../build/Apple.Foundation.dll: *.cs
	mcs --unsafe -g *.cs -target:library -out:../../build/Apple.Foundation.dll

maintainer-clean: clean

clean:
	rm ../../build/Apple.Foundation.dll
