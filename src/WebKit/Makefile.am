ASSEMBLY_NAME=webkit-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY)

CLEANFILES=$(ASSEMBLY)

sources= \
	./AssemblyInfo.cs \
	./WebBackForwardList.cs \
	./WebDataSource.cs \
	./WebFrame.cs \
	./WebHistoryItem.cs \
	./WebView.cs

EXTRA_DIST=$(sources)

build_sources = $(addprefix $(srcdir)/, $(sources))

$(ASSEMBLY): $(build_soruces)
	$(MCS) /out:$(ASSEMBLY) /r:../Cocoa/cocoa-sharp.dll /target:library $(build_sources)
