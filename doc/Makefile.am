ASSEMBLIES= \
	cocoa-sharp.dll \
	webkit-sharp.dll

UPDATE_ASSEMBLIES=$(addprefix -assembly:lib/, $ASSEMBLIES))

UPDATER=$(MONODOCER) -path:en $(UPDATE_ASSEMBLIES)

if ENABLE_MONODOC
SOURCESDIR=$(prefix)/lib/monodoc/sources
TARGETS=cocos-sharp.zip cocoa-sharp.tree cocoa-sharp.source
else
SOURCESDIR=$(datadir)
TARGETS=
endif

monodocdir=$(SOURCESDIR)
monodocDATA=$(TARGETS)

assemble: cocoa-sharp.zip cocoa-sharp.tree

cocoa-sharp.zip cocoa-sharp.tree: $(srcdir)/en/*/*.xml $(srcdir)/en/*.xml
	$(MDASSEMBLER) --ecma $(srcdir)/en -o cocoa-sharp

get-assemblies:
	mkdir -p lib
	cp $(top_builddir)/*/*.dll lib
	cp $(top_builddir)/*/*.dll.config lib

update: get-assemblies
	$(UPDATER)

update-delete: get-assemblies
	$(UPDATER) --delete

CLEANFILES=cocoa-sharp.zip cocoa-sharp.tree lib

EXTRA_DIST= \
	cocoa-sharp.source

NAMESPACES=Cocoa WebKit

dist-hook:
	mkdir -p $(distdir)/en
	cp $(srcdir)/en/*.xml $(distdir)/en/
	for i in $(NAMESPACES); do				\
		mkdir -p $(distdir)/en/$$i;			\
		cp $(srcdir)/en/$$i/*.xml $(distdir)/en/$$i;	\
	done
