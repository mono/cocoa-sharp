all: generator output

generator: Category.cs Interface.cs Main.cs Method.cs Protocol.cs
	mcs -g Category.cs Interface.cs Main.cs Method.cs Protocol.cs -out:generator.exe

output:
	(cd .. ; mono generator/generator.exe -xml:generator/generator.xml)
