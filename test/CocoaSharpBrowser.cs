using System;

using Apple.Foundation;
using Apple.AppKit;
using Apple.WebKit;

namespace Webbrowser {
	class Driver {

		static int Main(string[] args) {
			Webbrowser browser = new Webbrowser();
			browser.Run();
			return 1;
		}
	}

	class Webbrowser : NSObject {
		private NSApplication Application;
		private NSWindow Window;
		private NSBrowser Browser;
		private WebView Webview;
		private WebFrame Webframe;
		private NSTextField URLBar;
		private NSButton GoButton;

		public Webbrowser() {
			Application = NSApplication.SharedApplication;
		}

		public void Run() {
			Application.applicationIconImage = (NSImage)NSImage.ImageNamed("mono.icns");

			Window = new NSWindow(new NSRect(200, 180, 800, 600),
					      (uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask | NSWindowMask.NSTitledWindowMask),
					      NSBackingStoreType.NSBackingStoreBuffered,
					      false);
			Window.title = "Cocoa# WebBrowser";
			NSWindowController wc = new NSWindowController(Window);
			
			//Browser = new NSBrowser(new NSRect(0, 0, 250, 600));
			// Load the WebKit bundle
			NSBundle.BundleWithPath("/System/Library/Frameworks/WebKit.framework").load();
			Webview = new WebView(new NSRect(0, 0, 800, 575));
			//Webview.bezeled = true;
			LoadURL("http://www.slashdot.org/");

			URLBar = new NSTextField(new NSRect(0, 575, 700, 25));
			URLBar.editable = true;
			URLBar.bezeled = true;
			URLBar.stringValue = "http://www.slashdot.org/";
			URLBar.delegate_ = this;

			GoButton = new NSButton(new NSRect(700, 575, 100, 25));
			GoButton.bezelStyle = NSBezelStyle.NSRoundedBezelStyle;
			GoButton.title = "Load";
			GoButton.target = this;
			GoButton.action = "ButtonClick";

			((NSView)Window.contentView).addSubview(Webview);
			((NSView)Window.contentView).addSubview(URLBar);
			((NSView)Window.contentView).addSubview(GoButton);
			Window.center();
			Window.makeKeyAndOrderFront(null);
			wc.showWindow(Application);
			Application.run();
		}
		
		[ObjCExport("controlTextDidEndEditing:")]
		public void EnterHit(NSConcreteNotification aNotification) {
			LoadURL(((NSTextField)aNotification.object_).stringValue);
		}
		
		public void ButtonClick() {
			LoadURL(URLBar.stringValue);
		}
		public void LoadURL(string url) {
			((WebFrame)Webview.mainFrame).loadRequest((NSURLRequest)NSURLRequest.RequestWithURL((NSURL)NSURL.URLWithString(url)));
		}
	}
}
