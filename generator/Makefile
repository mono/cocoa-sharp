all: generator.exe generator.run

generator.exe: Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs
	mcs -g Category.cs CEnum.cs Struct.cs Interface.cs Main.cs Method.cs Protocol.cs Element.cs -out:generator.exe

generator.run: generator.exe
	(cd .. ; mono generator/generator.exe -xml:generator/generator.xml -out:CS) && touch generator.run
