ASSEMBLY_NAME=generator
ASSEMBLY=$(ASSEMBLY_NAME).exe
CLEANFILES=$(ASSEMBLY) 
DISTCLEANFILES=
EXTRA_DIST= \
	Category.cs \
	CEnum.cs \
	Element.cs \
	Interface.cs \
	Main.cs \
	Method.cs \
	ObjCClassInspector.cs \
	Protocol.cs \
	Struct.cs \
	generator-run.stamp

sources = \
	Category.cs \
	CEnum.cs \
	Element.cs \
	Interface.cs \
	Main.cs \
	Method.cs \
	ObjCClassInspector.cs \
	Protocol.cs \
	Struct.cs 

build_sources = $(addprefix $(srcdir)/, $(sources)) 

generator-run.stamp: $(ASSEMBLY)
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml) && touch $(srcdir)/generator-run.stamp

$(ASSEMBLY): $(build_sources) 
	$(CSC) /out:$(ASSEMBLY) $(build_sources)
