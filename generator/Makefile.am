ASSEMBLY_NAME=generator
ASSEMBLY=$(ASSEMBLY_NAME).exe
CLEANFILES=$(ASSEMBLY) 
DISTCLEANFILES=
noinst_DATA=generator-run.stamp
EXTRA_DIST= \
	OldCategory.cs \
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
	OldCategory.cs \
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

macho-reader.exe: Ivar.cs Category.cs LoadCommand.cs Class.cs MachOFile.cs Section.cs DylibCommand.cs SegmentCommand.cs ICommand.cs Module.cs Stub.cs SymbolTable.cs
	$(CSC) /unsafe /out:macho-reader.exe Ivar.cs Category.cs LoadCommand.cs Class.cs MachOFile.cs Section.cs DylibCommand.cs SegmentCommand.cs ICommand.cs Module.cs Stub.cs SymbolTable.cs
