using System;

using Apple.Foundation;
using Apple.AppKit;

namespace Finder {
	class Driver {

		static int Main(string[] args) {
			Finder browser = new Finder();
			browser.Run();
			return 1;
		}
	}

	class Finder : NSObject {
		private NSApplication Application;
		private NSWindow Window;
		private NSBrowser Browser;
		private NSTextField URLBar;
		private NSButton GoButton;

		public Finder() {
			Application = NSApplication.SharedApplication();
		}

		public void Run() {
			Application.ApplicationIconImage = (NSImage)NSImage.ImageNamed("mono.icns");

			Window = new NSWindow(new NSRect(200, 180, 800, 600),
					      (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					      NSBackingStoreType.NSBackingStoreBuffered,
					      false);
			Window.Title = "Cocoa# WebBrowser";
			NSWindowController wc = new NSWindowController(Window);
			
			Browser = new NSBrowser(new NSRect(0, 0, 800, 575));
			Browser.Delegate = this;

			URLBar = new NSTextField(new NSRect(0, 575, 700, 25));
	                URLBar.Editable = true;
	                URLBar.Bezeled = true;
	                URLBar.StringValue = "http://www.slashdot.org/";
			URLBar.Delegate = this;

			((NSView)Window.contentView()).addSubview(Browser);
			((NSView)Window.contentView()).addSubview(URLBar);
			Window.center();
			//Window.makeKeyAndOrderFront(null);
			wc.showWindow(Application);
			Application.run();
		}
		
		[ObjCExport(Selector="browser:numberOfRowsInColumn:",Signature="i16@0:4@8i12")]
		public int NumberOfRowsInColumn(NSBrowser browser, int columnNumber) {
			return 20;
		}

		[ObjCExport(Selector="browser:willDisplayCell:atRow:column:", Signature="v24@0:4@8@12i16i20")]
		public void WillDisplayCell(NSBrowser browser, NSBrowserCell cell, int rowNumber, int columnNumber) {
			Console.WriteLine("DEBUG: Displaying Row={0} Column={1}", rowNumber, columnNumber); 
			cell.StringValue = "Row=" + rowNumber + " Col=" + columnNumber;
		}
		[ObjCExport("controlTextDidEndEditing:")]
		public void TextFinishedEditing(NSConcreteNotification aNotification) {
		}
		
	}
}
