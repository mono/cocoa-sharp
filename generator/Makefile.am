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
	header-gen/Element.cs \
	header-gen/HeaderCategory.cs \
	header-gen/HeaderEnum.cs \
	header-gen/HeaderInterface.cs \
	header-gen/HeaderMethod.cs \
	header-gen/HeaderProtocol.cs \
	header-gen/HeaderStruct.cs \
	macho-gen/DylibCommand.cs \
	macho-gen/ICommand.cs \
	macho-gen/LoadCommand.cs \
	macho-gen/MachOCategory.cs \
	macho-gen/MachOClass.cs \
	macho-gen/MachOFile.cs \
	macho-gen/MachOIvar.cs \
	macho-gen/MachOMethod.cs \
	macho-gen/MachOProtocol.cs \
	macho-gen/MachOType.cs \
	macho-gen/Module.cs \
	macho-gen/Section.cs \
	macho-gen/SegmentCommand.cs \
	macho-gen/SymbolTable.cs \
	gen-out/Array.cs \
	gen-out/BitField.cs \
	gen-out/Category.cs \
	gen-out/Class.cs \
	gen-out/Configuration.cs \
	gen-out/Const.cs \
	gen-out/Enum.cs \
	gen-out/ExternalFunction.cs \
	gen-out/ExternalGlobal.cs \
	gen-out/Ivar.cs \
	gen-out/Method.cs \
	gen-out/ObjCClassInspector.cs \
	gen-out/OutputElement.cs \
	gen-out/Pointer.cs \
	gen-out/Protocol.cs \
	gen-out/Struct.cs \
	gen-out/Type.cs \
	gen-out/TypeUsage.cs \
	gen-out/WriteCS.cs \
	Generator.cs 

build_sources = $(addprefix $(srcdir)/, $(sources)) 

generator-run.stamp: $(ASSEMBLY)
	(cd .. ; mono --debug generator/generator.exe -xml:generator/generator.xml) && touch $(srcdir)/generator-run.stamp

$(ASSEMBLY): $(build_sources) 
	$(CSC) /unsafe /out:$(ASSEMBLY) $(build_sources)

