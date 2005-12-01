ASSEMBLY_NAME=cocoa-sharp
ASSEMBLY=$(ASSEMBLY_NAME).dll
TARGET=$(ASSEMBLY)
noinst_DATA=$(ASSEMBLY) cocoa-sharp.snk

CLEANFILES=$(ASSEMBLY) AssemblyInfo.cs

sources= \
	./ActionHandler.cs \
	./Application.cs \
	./Array.cs \
	./AssemblyInfo.cs \
	./AutoreleasePool.cs \
	./BackingStoreType.cs \
	./BezierPath.cs \
	./Browser.cs \
	./BrowserCell.cs \
	./Bundle.cs \
	./CachedObject.cs \
	./CalibratedRGBColor.cs \
	./Cell.cs \
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
	./ProgressIndicator.cs \
	./Rect.cs \
	./RegisterAttribute.cs \
	./Responder.cs \
	./RunLoop.cs \
	./Screen.cs \
	./SearchField.cs \
	./Size.cs \
	./StackPadding.cs \
	./TableColumn.cs \
	./TextField.cs \
	./Timer.cs \
	./String.cs \
	./URL.cs \
	./URLRequest.cs \
	./URLResponse.cs \
	./View.cs \
	./Window.cs

EXTRA_DIST=$(sources) \
	AssemblyInfo.cs \
	cocoa-sharp.snk

build_sources = $(addprefix $(srcdir)/, $(sources))

$(ASSEMBLY): $(build_sources)
	$(MCS) /unsafe /out:$(ASSEMBLY) /target:library $(build_sources)
