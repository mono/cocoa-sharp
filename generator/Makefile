all: generator.exe generator.run

Foundation: /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation
	ln -s /System/Library/Frameworks/Foundation.framework/Versions/Current/Foundation Foundation

generator.exe: Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs
	rm -f ../src/Apple.Foundation/*.cs
	rm -f ../src/Apple.WebKit/*.cs
	rm -f ../src/Apple.AppKit/*.cs
	mcs -g Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs -out:generator.exe

generator.run: generator.exe Foundation
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml) && touch generator.run

cs: generator.exe Foundation
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml -out:CS)

clean:
	rm -f generator.exe generator.run Foundation

maintainer-clean: clean

