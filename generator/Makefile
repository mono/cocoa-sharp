all: generator.exe generator.run

generator.exe: Category.cs Interface.cs Main.cs Method.cs Protocol.cs
	mcs -g Category.cs Interface.cs Main.cs Method.cs Protocol.cs -out:generator.exe

generator.run: generator.exe
	(cd .. ; mono generator/generator.exe -xml:generator/generator.xml) && touch generator.run
