//
// browser.cs: Mono documentation browser
//
// Author:
//   Miguel de Icaza
//
// (C) 2003 Ximian, Inc.
//
// TODO:
//   Add support for printing.
//   Add search facility
//
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Web.Services.Protocols;
using System.Xml;
using Apple.Foundation;
using Apple.AppKit;
using Apple.WebKit;

namespace Monodoc {
class Driver {
	static int Main (string [] args)
	{
		string topic = null;
		
		for (int i = 0; i < args.Length; i++){
			switch (args [i]){
			case "--html":
				if (i+1 == args.Length){
					Console.WriteLine ("--html needed argument");
					return 1; 
				}

				Node n;
				RootTree help_tree = RootTree.LoadTree ();
				string res = help_tree.RenderUrl (args [i+1], out n);
				if (res != null){
					Console.WriteLine (res);
					return 0;
				} else {
					return 1;
				}
			case "--make-index":
				RootTree.MakeIndex ();
				return 0;
				
			case "--help":
				Console.WriteLine ("Options are:\n"+
						   "browser [--html TOPIC] [--make-index] [TOPIC] [--merge-changes CHANGE_FILE TARGET_DIR+]");
				return 0;
			
			case "--merge-changes":
				if (i+2 == args.Length) {
					Console.WriteLine ("--merge-changes 2+ args");
					return 1; 
				}
				
				ArrayList targetDirs = new ArrayList ();
				
				for (int j = i+2; j < args.Length; j++)
					targetDirs.Add (args [j]);
				
				EditMerger e = new EditMerger (
					GlobalChangeset.LoadFromFile (args [i+1]),
					targetDirs
				);

				e.Merge ();
				
				return 0;
			default:
				topic = args [i];
				break;
			}
			
		}
		Settings.RunningGUI = true;
		Browser browser = new Browser ();

		NSApplication.SharedApplication.run();
		return 0;
	}
}

class Browser : NSObject {
	// The window controller	
	NSWindowController Windowcontroller;
	// The main window
	NSWindow Window;
	// Where we render the contents
	WebView Webview;
	WebFrame mainFrame;
	NSOutlineView ov;

	//
	// Left-hand side Browsers
	//
	string CurrentUrl;
	
	internal RootTree help_tree;

	// For the status bar.
	uint context_id;
	BrowserController browserController;

	public Browser ()
	{
	    const uint NSViewNotSizable                    =  0;
    	const uint NSViewMinXMargin                    =  1;
    	const uint NSViewWidthSizable                  =  2;
    	const uint NSViewMaxXMargin                    =  4;
    	const uint NSViewMinYMargin                    =  8;
    	const uint NSViewHeightSizable                 = 16;
    	const uint NSViewMaxYMargin                    = 32;
	
		// Do the monodoc stuff
		help_tree = RootTree.LoadTree ();
		browserController = new BrowserController(help_tree);

		Window = new NSWindow(new NSRect(200, 180, 600, 480),
			(uint)(NSWindowMask.NSMiniaturizableWindowMask | NSWindowMask.NSClosableWindowMask 
			| NSWindowMask.NSTitledWindowMask | NSWindowMask.NSResizableWindowMask),
			NSBackingStoreType.NSBackingStoreBuffered,
			false);
		Window.title = "Monodoc";
		Windowcontroller = new NSWindowController(Window);

		NSView drawerView = new NSView(new NSRect(0,0,250,480));
		drawerView.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;

		NSDrawer drawer = new NSDrawer(new NSSize(250,480), NSRectEdge.NSMaxXEdge);
		drawer.parentWindow = Window;
		drawer.contentView = drawerView;

		NSTabView tabView = new NSTabView(new NSRect(new NSPoint(0,0),drawer.contentSize));
		tabView.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;
	
		drawerView.addSubview(tabView);
		
		NSTabViewItem tabViewItem = new NSTabViewItem("Contents");
		tabViewItem.label = "Contents";
		tabView.addTabViewItem(tabViewItem);
		NSScrollView sv = new NSScrollView(tabView.contentRect);
		sv.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;
		sv.hasVerticalScroller = true;
		sv.hasHorizontalScroller = true;
		sv.autohidesScrollers = true;
		tabViewItem.view = sv;

		ov = new NSOutlineView(sv.documentVisibleRect);
		ov.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;
		sv.documentView = ov;
		NSTableColumn c = new NSTableColumn("Caption");
		c.width = sv.contentSize.width;
		c.editable = false;
		c.resizable = true;
		((NSCell)c.headerCell).stringValue = "";
		ov.addTableColumn(c);
		ov.outlineTableColumn = c;

		ov.dataSource = browserController;
		ov.target = this;
		ov.doubleAction = "selectedItem";
 		
		tabViewItem = new NSTabViewItem("Index");
		tabViewItem.label = "Index";
		tabView.addTabViewItem(tabViewItem);
		tabViewItem = new NSTabViewItem("Search");
		tabViewItem.label = "Search";
		tabView.addTabViewItem(tabViewItem);
	
		NSBundle.BundleWithPath("/System/Library/Frameworks/WebKit.framework").load();
		Webview = new WebView(new NSRect(0, 0, 600, 480));
		Webview.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;
		mainFrame = (WebFrame)Webview.mainFrame;

		((NSView)Window.contentView).addSubview(Webview);
		
		drawer.open();
		
		Windowcontroller.showWindow(NSApplication.SharedApplication);
	}

	[ObjCExport("selectedItem")]
	public void DoubleClick() {
		BrowserItem bi = ov.itemAtRow(ov.selectedRow) as BrowserItem;
		Console.WriteLine("Going to load {0}", bi);
		if(bi.node.URL != null)
		{
			string url = help_tree.RenderUrl(bi.node.URL, out bi.node);
			if (url != null)
				mainFrame.loadHTMLString_baseURL(url, null);
		}
	}

/*
	//
	// Renders the HTML text in `text' which was computed from `url'.
	// The Node matching is `matched_node'.
	//
	// `url' is only used for debugging purposes
	//
	[ObjCExport("doRender:")]
	public void Render (string text, Node matched_node, string url)
	{
		CurrentUrl = url;

		Gtk.HTMLStream stream = html.Begin ("text/html");

		stream.Write ("<html><body>");
		stream.Write (text);
		stream.Write ("</body></html>");
		html.End (stream, HTMLStreamStatus.Ok);
		if (matched_node != null) {
			if (tree_browser.SelectedNode != matched_node)
				tree_browser.ShowNode (matched_node);

			title_label.Text = matched_node.Caption;

			if (matched_node.Nodes != null) {
				int count = matched_node.Nodes.Count;
				string term;

				if (count == 1)
					term = "subpage";
				else
					term = "subpages";

				subtitle_label.Text = count + " " + term;
			} else
				subtitle_label.Text = "";
		} else {
			title_label.Text = "Error";
			subtitle_label.Text = "";
		}
	}
*/
}

class BrowserItem : NSObject {
	internal Node node;
	internal IList items = null;
	internal NSString caption;

	protected BrowserItem(IntPtr _ptr,bool release) : base(_ptr,release) {
		Console.WriteLine("ERROR: BrowserItem.ctor(IntPtr,bool) is called: bad: Raw={0,8:x}", (int)_ptr);
	}
	public BrowserItem(Node _node) {
		node = _node;
		caption = new NSString(node.Caption);
		caption.retain();
		Console.WriteLine("DEBUG: BrowserItem.ctor(" + node.Caption + ") is called: Raw{0,8:x}=", (int)Raw);
	}
	~ BrowserItem() {
		Console.WriteLine("DEBUG: ~" + this + " Raw={0,8:x}", (int)Raw);
		SetRaw(IntPtr.Zero,false);
	}
	
	public int Count { 
		get { 
			if(node.Nodes == null)
				return 0;
			return node != null ? node.Nodes.Count : 0; 
		} 
	}
	public BrowserItem ItemAt(int ndx)
	{
		if (items == null && !node.IsLeaf) {
			items = new ArrayList();
			foreach (Node n in node.Nodes) 
				if (n != null) 
					items.Add(new BrowserItem(n));
		}
		return (BrowserItem)items[ndx];
	}
	public object ValueAt(object identifier)
	{
Console.WriteLine("DEBUG: ValueAt: " + identifier + " for " + this);
		return caption;
	}
	public override string ToString()
	{
		return "BrowserItem: " + (node != null ? node.Caption : "null");
	}
}

class BrowserController : NSObject {
	internal RootTree help_tree;
	internal IList items = new ArrayList();

	public BrowserController(RootTree _tree) {
		help_tree = _tree;
		foreach (Node node in help_tree.Nodes)
			items.Add(new BrowserItem(node));
		Console.WriteLine("DEBUG: " + this + ".ctor Raw={0,8:x}", (int)Raw);
	}
	~ BrowserController () {
		Console.WriteLine("DEBUG: ~" + this + " Raw={0,8:x}", (int)Raw);
	}

	[ObjCExport("outlineView:numberOfChildrenOfItem:")]
	public int OutlineViewNumberOfChildrenOfItem(NSOutlineView outlineView, object item)
	{
		BrowserItem bi = item as BrowserItem;
		int count = bi != null ? bi.Count : help_tree.Nodes.Count;
Console.WriteLine("DEBUG: OutlineViewNumberOfChildrenOfItem: " + item + " --> " + count);
		return count;
	}

	[ObjCExport("outlineView:isItemExpandable:")]
	public bool OutlineViewIsItemExpandable(NSOutlineView outlineView, object item)
	{
		return OutlineViewNumberOfChildrenOfItem(outlineView,item) > 0;
	}

	[ObjCExport("outlineView:child:ofItem:")]
	public object OutlineViewChildOfItem(NSOutlineView outlineView, int index, object item)
	{
Console.WriteLine("DEBUG: OutlineViewChildOfItem: " + index + ", item: " + item);
		BrowserItem bi = item as BrowserItem;
		if (bi != null)
			bi = bi.ItemAt(index);
		else
			bi = (BrowserItem)items[index];
		return bi;
	}

	[ObjCExport("outlineView:objectValueForTableColumn:byItem:")]
	public object OutlineViewObjectValueForTableColumnByItem(NSOutlineView outlineView, NSTableColumn tableColumn, object item)
	{
Console.WriteLine("DEBUG: OutlineViewObjectValueForTableColumnByItem: " + item + ", for column: " + tableColumn.identifier);
		BrowserItem bi = item as BrowserItem;
		
		return bi == null ? null : bi.ValueAt(tableColumn.identifier);
	}
	
   }
}
