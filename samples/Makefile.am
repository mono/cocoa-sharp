APP_TARGETS=HelloWorld.app DragNDrop.app Rectangle.app Cube.app ViewDraw.app ViewSysDraw.app

if ENABLE_MONODOC
MONODOCDIR=$(prefix)/lib/monodoc
TARGETS=CocoaDoc.app $(APP_TARGETS)
else
TARGETS=$(APP_TARGETS)
endif

noinst_SCRIPTS=$(TARGETS)

DragNDrop.exe: DragNDrop.cs
	$(MCS) $(top_srcdir)/samples/DragNDrop.cs -out:$(top_builddir)/samples/DragNDrop.exe -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

DragNDrop.app: DragNDrop.exe DragNDrop.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/DragNDrop.nib -i:$(top_srcdir)/samples/applemono.icns -n:DragNDrop -o:$(top_builddir)/samples -a:$(top_builddir)/samples/DragNDrop.exe

HelloWorld.exe: HelloWorld.cs
	$(MCS) $(top_srcdir)/samples/HelloWorld.cs -out:$(top_builddir)/samples/HelloWorld.exe -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

HelloWorld.app: HelloWorld.exe HelloWorld.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/HelloWorld.nib -i:$(top_srcdir)/samples/applemono.icns -n:HelloWorld -o:$(top_builddir)/samples -a:$(top_builddir)/samples/HelloWorld.exe

Rectangle.exe: OpenGLViewSample.cs
	$(MCS) $(top_srcdir)/samples/OpenGLViewSample.cs -d:RECTANGLE -out:$(top_builddir)/samples/Rectangle.exe -r:System.Drawing -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

Rectangle.app: Rectangle.exe OpenGLView.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/OpenGLView.nib -i:$(top_srcdir)/samples/applemono.icns -n:Rectangle -o:$(top_builddir)/samples -a:$(top_builddir)/samples/Rectangle.exe

Cube.exe: OpenGLViewSample.cs
	$(MCS) $(top_srcdir)/samples/OpenGLViewSample.cs -d:CUBE -out:$(top_builddir)/samples/Cube.exe -r:System.Drawing -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

Cube.app: Cube.exe OpenGLView.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/OpenGLView.nib -i:$(top_srcdir)/samples/applemono.icns -n:Cube -o:$(top_builddir)/samples -a:$(top_builddir)/samples/Cube.exe

ViewDraw.exe: ViewSample.cs
	$(MCS) $(top_srcdir)/samples/ViewSample.cs -out:$(top_builddir)/samples/ViewDraw.exe -r:System.Drawing -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

ViewDraw.app: ViewDraw.exe View.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/View.nib -i:$(top_srcdir)/samples/applemono.icns -n:ViewDraw -o:$(top_builddir)/samples -a:$(top_builddir)/samples/ViewDraw.exe

ViewSysDraw.exe: ViewSample.cs
	$(MCS) $(top_srcdir)/samples/ViewSample.cs -d:SYSD -out:$(top_builddir)/samples/ViewSysDraw.exe -r:System.Drawing -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

ViewSysDraw.app: ViewSysDraw.exe View.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_srcdir)/samples/View.nib -i:$(top_srcdir)/samples/applemono.icns -n:ViewSysDraw -o:$(top_builddir)/samples -a:$(top_builddir)/samples/ViewSysDraw.exe

if ENABLE_MONODOC
CocoaDoc.exe: CocoaDoc.cs
	$(MCS) $(top_srcdir)/samples/CocoaDoc.cs -d:SYSD -out:$(top_builddir)/samples/CocoaDoc.exe -pkg:monodoc -r:System.Web.Services -r:$(top_builddir)/src/WebKit/webkit-sharp.dll -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll

CocoaDoc.app: CocoaDoc.exe CocoaDoc.nib
	macpack -m:2 -r:$(top_builddir)/src/Cocoa/cocoa-sharp.dll -r:$(top_builddir)/src/WebKit/webkit-sharp.dll -r:$(top_srcdir)/samples/monodoc.xml -r:$(MONODOCDIR)/sources -r:$(top_srcdir)/samples/CocoaDoc.nib -r:$(top_srcdir)/samples/mono.png -i:$(top_srcdir)/samples/applemono.icns -n:CocoaDoc -o:$(top_builddir)/samples -a:$(top_builddir)/samples/CocoaDoc.exe
	cp $(top_builddir)/doc/cocoa-sharp.source $(top_builddir)/samples/CocoaDoc.app/Contents/Resources/sources
	cp $(top_builddir)/doc/cocoa-sharp.tree $(top_builddir)/samples/CocoaDoc.app/Contents/Resources/sources
	cp $(top_builddir)/doc/cocoa-sharp.zip $(top_builddir)/samples/CocoaDoc.app/Contents/Resources/sources
endif

APP_CLEANFILES=HelloWorld.exe CocoaDoc.exe Rectangle.exe DragNDrop.exe Cube.exe ViewDraw.exe ViewSysDraw.exe

if ENABLE_MONODOC
CLEANFILES=CocoaDoc.exe $(APP_CLEANFILES)
else
CLEANFILES=$(APP_CLEANFILES)
endif

EXTRA_DIST= \
	DragNDrop.cs \
	DragNDrop.nib \
	HelloWorld.cs \
	HelloWorld.nib \
	CocoaDoc.cs \
	CocoaDoc.nib \
	OpenGLViewSample.cs \
	OpenGLView.nib \
	ViewSample.cs \
	View.nib \
	applemono.icns \
	monodoc.xml \
	mono.png

clean-local:
	rm -Rf HelloWorld.app CocoaDoc.app Rectangle.app DragNDrop.app Cube.app ViewDraw.app ViewSysDraw.app
