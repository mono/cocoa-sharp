ASSEMBLY_NAME=webkit-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY)

pkg=webkit
pkgconfigdir=$(libdir)/pkgconfig
pkgconfig_DATA=webkit-sharp.pc

sources= \
	./AssemblyInfo.cs \
	./WebBackForwardList.cs \
	./WebDataSource.cs \
	./WebFrame.cs \
	./WebHistoryItem.cs \
	./WebView.cs

EXTRA_DIST=$(sources) webkit-sharp.pc.in

build_sources = $(addprefix $(srcdir)/, $(sources))

$(ASSEMBLY): $(SNK) $(build_soruces)
	$(MCS) /nowarn:0169 /out:$(ASSEMBLY) /r:../Cocoa/cocoa-sharp.dll /target:library $(build_sources)

include ../../Makefile.include
