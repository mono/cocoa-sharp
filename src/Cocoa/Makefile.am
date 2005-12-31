ASSEMBLY_NAME=cocoa-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY)

pkg=cocoa
pkgconfigdir=$(libdir)/pkgconfig
pkgconfig_DATA=cocoa-sharp.pc

sources= \
	./ActionHandler.cs \
	./Application.cs \
	./Alert.cs \
	./AlertStyle.cs \
	./Array.cs \
	./AssemblyInfo.cs \
	./AutoreleasePool.cs \
	./BackingStoreType.cs \
	./BezelStyle.cs \
	./BezierPath.cs \
	./Browser.cs \
	./BrowserCell.cs \
	./Bundle.cs \
	./Button.cs \
	./CachedObject.cs \
	./CalibratedRGBColor.cs \
	./Cell.cs \
	./CellStateValue.cs \
	./Color.cs \
	./ConnectAttribute.cs \
	./Control.cs \
	./CFArray.cs \
	./CFString.cs \
	./CFTimer.cs \
	./DeviceRGBColor.cs \
	./Dictionary.cs \
	./Drawer.cs \
	./DragDestination.cs \
	./DragOperation.cs \
	./ExportAttribute.cs \
	./FileWrapper.cs \
	./Image.cs \
	./ImageView.cs \
	./Mach.cs \
	./MethodSignature.cs \
	./MenuItem.cs \
	./MutableArray.cs \
	./MutableString.cs \
	./Native.cs \
	./NativeMember.cs \
	./NativeRepresentation.cs \
	./Notification.cs \
	./ObjCMessaging.cs \
	./Object.cs \
	./OpenGLView.cs \
	./OutlineView.cs \
	./Pasteboard.cs \
	./Point.cs \
	./PopUpButton.cs \
	./ProgressIndicator.cs \
	./Rect.cs \
	./RegisterAttribute.cs \
	./Responder.cs \
	./RunLoop.cs \
	./Screen.cs \
	./SearchField.cs \
	./ScrollView.cs \
	./Size.cs \
	./StackPadding.cs \
	./TableColumn.cs \
	./TableView.cs \
	./Text.cs \
	./TextField.cs \
	./TextView.cs \
	./Timer.cs \
	./String.cs \
	./URL.cs \
	./URLRequest.cs \
	./URLResponse.cs \
	./View.cs \
	./Window.cs \
	./WindowStyle.cs

EXTRA_DIST=$(sources) cocoa-sharp.pc.in

build_sources = $(addprefix $(srcdir)/, $(sources))

$(ASSEMBLY): $(SNK) $(build_sources)
	$(MCS) /nowarn:0169 /unsafe /out:$(ASSEMBLY) /target:library $(build_sources)

include ../../Makefile.include
