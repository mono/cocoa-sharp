all: ../../build/Apple.Tools.dll

../../build/Apple.Tools.dll: *.cs
	mcs -g *.cs -target:library -out:../../build/Apple.Tools.dll

maintainer-clean: clean

clean:
	rm ../../build/Apple.Tools.dll
