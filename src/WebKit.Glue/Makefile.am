all: ../../build/libWebKitGlue.dylib

../../build/libWebKitGlue.dylib: WebBackForwardList_glue.m WebHistoryItem_glue.m WebView_glue.m WebDataSource_glue.m WebHistory_glue.m WebFrame_glue.m WebPreferences_glue.m NSObjectWebKitExtras_glue.m
	gcc -dynamiclib -flat_namespace -undefined suppress -framework Cocoa -o ../../build/libWebKitGlue.dylib WebBackForwardList_glue.m WebHistoryItem_glue.m WebView_glue.m WebDataSource_glue.m WebHistory_glue.m WebFrame_glue.m WebPreferences_glue.m NSObjectWebKitExtras_glue.m
clean:
	rm -f ../../build/libWebKitGlue.dylib

install-data-local:
	if test -n '@prefix@/lib/libWebKitGlue.dylib'; then cp ../../build/libWebKitGlue.dylib @prefix@/lib/libWebKitGlue.dylib; fi
