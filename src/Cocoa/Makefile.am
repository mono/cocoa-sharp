ASSEMBLY_NAME=cocoa-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY)

pkg=cocoa
pkgconfigdir=$(libdir)/pkgconfig
pkgconfig_DATA=cocoa-sharp.pc

sources= \
	./Interop/ConnectAttribute.cs \
	./Interop/ExportAttribute.cs \
	./Interop/MethodImplementation.cs \
	./Interop/ObjCClass.cs \
	./Interop/ObjCInterop.cs \
	./Interop/ObjCMessaging.cs \
	./Interop/ObjCMethod.cs \
	./Interop/ObjCMethods.cs \
	./Interop/ObjCStructs.cs \
	./Interop/ObjCTypes.cs \
	./Interop/RegisterAttribute.cs \
	./ActionHandler.cs \
	./Application.cs \
	./Alert.cs \
	./AlertHandler.cs \
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
	./CalibratedRGBColor.cs \
	./Cell.cs \
	./CellStateValue.cs \
	./Color.cs \
	./Control.cs \
	./CFArray.cs \
	./CFString.cs \
	./CFTimer.cs \
	./DeviceRGBColor.cs \
	./Dictionary.cs \
	./Drawer.cs \
	./DragDestination.cs \
	./DragOperation.cs \
	./FileWrapper.cs \
	./Image.cs \
	./ImageView.cs \
	./Menu.cs \
	./MenuItem.cs \
	./MethodSignature.cs \
	./MutableArray.cs \
	./MutableString.cs \
	./Notification.cs \
	./Object.cs \
	./OpenGLView.cs \
	./OpenPanel.cs \
	./OpenPanelHandler.cs \
	./OutlineView.cs \
	./Panel.cs \
	./Pasteboard.cs \
	./Point.cs \
	./PopUpButton.cs \
	./ProgressIndicator.cs \
	./Rect.cs \
	./Responder.cs \
	./RunLoop.cs \
	./SavePanel.cs \
	./SavePanelHandler.cs \
	./Screen.cs \
	./SearchField.cs \
	./SheetHandler.cs \
	./ScrollView.cs \
	./Size.cs \
	./StatusBar.cs \
	./StatusItem.cs \
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
	$(MCS) /nowarn:0169 /out:$(ASSEMBLY) /target:library $(build_sources)

include ../../Makefile.include
