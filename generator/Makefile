all: generator.exe generator.run

generator.exe: Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs
	rm ../src/Apple.Foundation/*.cs
	rm ../src/Apple.WebKit/*.cs
	rm ../src/Apple.AppKit/*.cs
	mcs -g Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs -out:generator.exe

generator.run: generator.exe
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml) && touch generator.run

clean:
	rm generator.exe generator.run

maintainer-clean: clean

