ASSEMBLY_NAME=generator
ASSEMBLY=$(ASSEMBLY_NAME).exe
CLEANFILES=$(ASSEMBLY) 
DISTCLEANFILES=
noinst_DATA=generator-run.stamp
EXTRA_DIST= 

sources= \
	Category.cs \
	CEnum.cs \
	Element.cs \
	Interface.cs \
	Main.cs \
	Method.cs \
	ObjCClassInspector.cs \
	Protocol.cs \
	Struct.cs 

generator-run.stamp: $(ASSEMBLY)
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml) && touch generator-run.stamp

$(ASSEMBLY): $(sources) 
	$(CSC) /out:$(ASSEMBLY) $(sources)
