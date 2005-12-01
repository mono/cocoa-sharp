all: clean GraphicsView.app View.app Cube.app Rectangle.app DragNDrop HelloWorld MonoDoc

MonoDoc: MonoDoc.nib MonoDoc.cs
	mcs -out:browser.exe MonoDoc.cs -r:System.Web.Services.dll -r:../src/Cocoa/cocoa-sharp.dll -r:../src/WebKit/webkit-sharp.dll -r:/Library/Frameworks/Mono.framework/Versions/Current/lib/mono/monodoc/monodoc.dll
	macpack -m:2 -n:MonoDoc -i:applemono.icns -r:mono.png -r:../src/Cocoa/cocoa-sharp.dll -r:../src/WebKit/webkit-sharp.dll -r:/Library/Frameworks/Mono.framework/Versions/Current/lib/monodoc/sources -r:./monodoc.xml -r:/Library/Frameworks/Mono.framework/Versions/Current/lib/mono/monodoc/monodoc.dll -r:../doc/cocoa-sharp.source -r:../doc/cocoa-sharp.tree -r:../doc/cocoa-sharp.zip -r:MonoDoc.nib -o:. -a:browser.exe
	mv MonoDoc.app/Contents/Resources/cocoa-sharp.source MonoDoc.app/Contents/Resources/sources
	mv MonoDoc.app/Contents/Resources/cocoa-sharp.tree MonoDoc.app/Contents/Resources/sources
	mv MonoDoc.app/Contents/Resources/cocoa-sharp.zip MonoDoc.app/Contents/Resources/sources
	rm browser.exe

DragNDrop: DragNDrop.nib DragNDrop.cs
	mcs DragNDrop.cs -r:../src/Cocoa/cocoa-sharp.dll
	macpack -m:2 -n:DragNDrop -r:../src/Cocoa/cocoa-sharp.dll -r:DragNDrop.nib -o:. -a:DragNDrop.exe
	rm DragNDrop.exe

HelloWorld: HelloWorld.nib HelloWorld.cs
	mcs HelloWorld.cs -r:../src/Cocoa/cocoa-sharp.dll
	macpack -m:2 -n:HelloWorld -r:../src/Cocoa/cocoa-sharp.dll -r:HelloWorld.nib -o:. -a:HelloWorld.exe
	rm HelloWorld.exe

GraphicsView.app: GraphicsView.exe
	macpack -n:GraphicsView -a:./GraphicsView.exe -r:View.nib -m:2 -o:. -r:../src/Cocoa/cocoa-sharp.dll

View.app: View.exe
	macpack -n:View -a:./View.exe -r:View.nib -m:2 -o:. -r:../src/Cocoa/cocoa-sharp.dll

Cube.app: Cube.exe
	macpack -n:Cube -a:./Cube.exe -r:OpenGLView.nib -m:2 -o:. -r:../src/Cocoa/cocoa-sharp.dll

Rectangle.app: Rectangle.exe
	macpack -n:Rectangle -a:./Rectangle.exe -r:OpenGLView.nib -m:2 -o:. -r:../src/Cocoa/cocoa-sharp.dll

GraphicsView.exe:
	mcs -out:GraphicsView.exe -define:SYSD ViewSample.cs -r:System.Drawing -r:../src/Cocoa/cocoa-sharp.dll

View.exe:
	mcs -out:View.exe -r:System.Drawing ViewSample.cs -r:../src/Cocoa/cocoa-sharp.dll

Rectangle.exe:
	mcs -out:Rectangle.exe -define:RECTANGLE OpenGLViewSample.cs -r:System.Drawing -r:../src/Cocoa/cocoa-sharp.dll

Cube.exe:
	mcs -out:Cube.exe -define:CUBE OpenGLViewSample.cs -r:System.Drawing -r:../src/Cocoa/cocoa-sharp.dll
	
clean:
	find . -maxdepth 1 -name '*.exe' -exec rm {} \;
	rm -Rf HelloWorld.app
	rm -Rf DragNDrop.app
	rm -Rf MonoDoc.app
	rm -Rf ./GraphicsView.app/ 
	rm -Rf ./View.app/ 
	rm -Rf ./Cube.app/ 
	rm -Rf ./Rectangle.app/ 

