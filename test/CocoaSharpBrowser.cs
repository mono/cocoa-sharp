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
		static IntPtr Browser_class = NSRegisterClass(typeof(Webbrowser), "NSObject");

		private NSApplication Application;
		private NSWindow Window;
		private NSBrowser Browser;
		private WebView Webview;
		private WebFrame Webframe;
		private NSTextField URLBar;
		private NSButton GoButton;
		private BridgeDelegate BrowserDelegate;

		public Webbrowser() : this(NSObject__alloc0(Browser_class), true) {}

		public Webbrowser(IntPtr raw, bool release) : base(raw, release) {
			BrowserDelegate = new BridgeDelegate(this.MethodInvoker);
                	SetRaw(DotNetForwarding_initWithManagedDelegate(Raw,BrowserDelegate),release);
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
			
			//Browser = new NSBrowser(new NSRect(0, 0, 250, 600));
			// Load the WebKit bundle
			NSBundle.BundleWithPath("/System/Library/Frameworks/WebKit.framework").load();
			Webview = new WebView(new NSRect(0, 0, 800, 575));
			//Webview.Bezeled = true;
			( (WebFrame)NS2Net(Webview.mainFrame()) ).loadRequest ( (NSURLRequest)NSURLRequest.RequestWithURL ( (NSURL)NSURL.URLWithString("http://www.slashdot.org/") ) );

			URLBar = new NSTextField(new NSRect(0, 575, 700, 25));
	                URLBar.Editable = true;
	                URLBar.Bezeled = true;
	                URLBar.StringValue = "http://www.slashdot.org/";
			URLBar.Delegate = this;

			GoButton = new NSButton(new NSRect(700, 575, 100, 25));
			GoButton.BezelStyle = NSBezelStyle.NSRoundedBezelStyle;
			GoButton.Title = "Load";
			GoButton.Target = this;
			GoButton.Action = "ButtonClick";

			((NSView)Window.contentView()).addSubview(Webview);
			((NSView)Window.contentView()).addSubview(URLBar);
			((NSView)Window.contentView()).addSubview(GoButton);
			Window.center();
			Window.makeKeyAndOrderFront(null);
			wc.showWindow(Application);
			Application.run();
		}
		
		[ObjCExport("controlTextDidEndEditing:")]
		public void EnterHit(NSConcreteNotification aNotification) {
			LoadURL( ((NSTextField)aNotification.object_()).StringValue );
		}
		
		public void ButtonClick() {
			LoadURL (URLBar.StringValue);
		}
		public void LoadURL(String url) {
			( (WebFrame)NS2Net(Webview.mainFrame()) ).loadRequest ( (NSURLRequest)NSURLRequest.RequestWithURL ( (NSURL)NSURL.URLWithString(url) ) );
		}
			
	}
}
