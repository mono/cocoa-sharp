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
			Application = NSApplication.SharedApplication;
		}

		public void Run() {
			Application.applicationIconImage = (NSImage)NSImage.ImageNamed("applemono.icns");

			Window = new NSWindow(new NSRect(200, 180, 800, 600),
					      (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					      NSBackingStoreType.NSBackingStoreBuffered,
					      false);
			Window.title = "Cocoa# WebBrowser";
			NSWindowController wc = new NSWindowController(Window);

			Browser = new NSBrowser(new NSRect(0, 0, 800, 575));
			Browser.delegate_ = this;

			URLBar = new NSTextField(new NSRect(0, 575, 700, 25));
	        URLBar.editable = true;
	        URLBar.bezeled = true;
	        URLBar.stringValue = "http://www.slashdot.org/";
			URLBar.delegate_ = this;

			((NSView)Window.contentView).addSubview(Browser);
			((NSView)Window.contentView).addSubview(URLBar);
			Window.center();
			//Window.makeKeyAndOrderFront(null);
			wc.showWindow(Application);
			Application.run();
		}
		
		[Export(Selector="browser:numberOfRowsInColumn:",Signature="i16@0:4@8i12")]
		public int NumberOfRowsInColumn(NSBrowser browser, int columnNumber) {
			return 20;
		}

		[Export(Selector="browser:willDisplayCell:atRow:column:", Signature="v24@0:4@8@12i16i20")]
		public void WillDisplayCell(NSBrowser browser, NSBrowserCell cell, int rowNumber, int columnNumber) {
			Console.WriteLine("DEBUG: Displaying Row={0} Column={1}", rowNumber, columnNumber); 
			cell.stringValue = "Row=" + rowNumber + " Col=" + columnNumber;
		}

		[Export("controlTextDidEndEditing:")]
		public void TextFinishedEditing(NSConcreteNotification aNotification) {
		}
	}
}
