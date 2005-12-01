ASSEMBLY_NAME=webkit-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY) cocoa-sharp.snk

CLEANFILES=$(ASSEMBLY) AssemblyInfo.cs

sources= \
	./AssemblyInfo.cs \
	./WebBackForwardList.cs \
	./WebDataSource.cs \
	./WebFrame.cs \
	./WebHistoryItem.cs \
	./WebView.cs

EXTRA_DIST=$(sources) \
	AssemblyInfo.cs \
	cocoa-sharp.snk

$(ASSEMBLY): $(build_soruces)
	$(MCS) /out:$(ASSEMBLY) /target:library $(build_sources)
