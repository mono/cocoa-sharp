all: monodoc

monodoc:
	mcs -debug -out:browser.exe browser.cs Contributions.cs XmlNodeWriter.cs catalog.cs -resource:./monodoc.png,monodoc.png -r:System.Web.Services.dll -r:../build/Apple.Foundation.dll -r:../build/Apple.AppKit.dll -r:../build/Apple.WebKit.dll -r:./monodoc.dll
