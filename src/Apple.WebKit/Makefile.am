ASSEMBLY_NAME=Apple.WebKit
ASSEMBLY=$(ASSEMBLY_NAME).dll
CLEANFILES=$(ASSEMBLY) cocoa-sharp.snk
DISTCLEANFILES=AssemblyInfo.cs
noinst_DATA=$(ASSEMBLY) $(ASSEMBLY).config
EXTRA_DIST= \
	$(ASSEMBLY).config \
	AssemblyInfo.cs.in

references = /r:../Apple.Foundation/Apple.Foundation.dll /r:../Apple.AppKit/Apple.AppKit.dll

sources= \
	AssemblyInfo.cs \
	IWebDocumentRepresentation.cs \
	IWebDocumentSearching.cs \
	IWebDocumentText.cs \
	IWebDocumentView.cs \
	IWebOpenPanelResultListener.cs \
	IWebPolicyDecisionListener.cs \
	NSObjectWebKitExtras.cs \
	WebBackForwardList.cs \
	WebDataSource.cs \
	WebDownload.cs \
	WebFrame.cs \
	WebFrameView.cs \
	WebHistory.cs \
	WebHistoryItem.cs \
	WebNavigationType.cs \
	WebPreferences.cs \
	WebView.cs

$(ASSEMBLY): $(sources) cocoa-sharp.snk
	$(CSC) /nowarn:0114,0108 /out:$(ASSEMBLY) /target:library $(references) $(sources)

cocoa-sharp.snk: $(top_srcdir)/cocoa-sharp.snk
	cp $(top_srcdir)/cocoa-sharp.snk .

install-data-local:
	@if test -n '$(TARGET)'; then                                           \
	  echo "$(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS)"; \
	  $(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS) || exit 1;      \
	fi

uninstall-local:
	@if test -n '$(TARGET)'; then                                           \
	  echo "$(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS)";       \
	  $(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS) || exit 1;    \
	fi
