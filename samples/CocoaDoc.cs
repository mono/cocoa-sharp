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
using System.Runtime.InteropServices;
using System.Threading;
using Cocoa;
using WebKit;

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
				RootTree help_tree = RootTree.LoadTree();
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
		DocumentBrowser browser = new DocumentBrowser ();
		browser.Run();
		return 0;
	}
}

[Register("Controller")]
public class Controller : Cocoa.Object {

	[Connect]
	public Drawer drawer;
	[Connect]
	public OutlineView outlineView;
	[Connect]
	public WebView webView;
	[Connect]
	public SearchField searchBox;
	[Connect]
	public Cocoa.Browser indexBrowser;
	[Connect]
	public MenuItem backMenuItem;
	[Connect]
	public MenuItem forwardMenuItem;
	[Connect]
	public Window mainWindow;

	static RootTree help_tree;
	


	static Controller() {
		help_tree = RootTree.LoadTree();
	}
	
	public Controller (IntPtr native_object) : base (native_object) {}

	[Export("userDidSearch:")]
	public void UserDidSearch(Cocoa.Object sender) {
		string value = searchBox.Value;
		int index=indexBrowser.SelectedRowInColumn (0);
		if (value != null) {
			Console.WriteLine ("Searching for {0}", value);
			index = IndexDataSource.FindClosest(value);
		}
		indexBrowser.SelectRowInColumn (index, 0);
	}

	[Export("timerTick:")]
	public void TimerTick (Cocoa.Object sender) {
		Console.WriteLine ("Timer tick");
		RunLoop.Current.Stop ();
	}

	[Export("applicationWillFinishLaunching:")]
	public void FinishLoading(Notification aNotification) {

		drawer.Open();
		mainWindow.Show ();
		indexBrowser.DoubleAction += new EventHandler (OnBrowserDoubleAction);
		outlineView.DoubleAction += new EventHandler (OnOutlineViewDoubleAction);
		// use history.
		webView.HasBackForwardList = true;
		webView.BackForwardList.Capacity = 100;
		forwardMenuItem.Click += new ActionHandler (goForward);
		backMenuItem.Click += new ActionHandler (goBack);
		Node match;
		string content = help_tree.RenderUrl("root:", out match);
		content=content.Replace("a href='", "a href='http://monodoc/load?");
		content=content.Replace("a href=\"", "a href=\"http://monodoc/load?");
		webView.Render (content);
		addHistoryItem("root:");
	}
	
	private void OnBrowserDoubleAction(System.Object sender, EventArgs e) {
		IndexEntry entry = IndexDataSource.GetEntry(indexBrowser.SelectedRowInColumn(0));
		if(entry != null) {
			Topic t = entry[0];
			Node match;
			string content = help_tree.RenderUrl(t.Url, out match);
			content=content.Replace("a href='", "a href='http://monodoc/load?");
			content=content.Replace("a href=\"", "a href=\"http://monodoc/load?");
			webView.Render (content);
			addHistoryItem(t.Url);
		}
	}
	private void OnOutlineViewDoubleAction(System.Object sender, EventArgs e) {
		BrowserItem bi = outlineView.SelectedItem as BrowserItem;
		try {
			if(bi.node.URL != null)
			{
				Node n;
				string content = "";
				if(bi.node.tree != null && bi.node.tree.HelpSource != null)
					content = bi.node.tree.HelpSource.GetText(bi.node.URL, out n);
				if(content == null || content.Equals("") )
						content = help_tree.RenderUrl(bi.node.URL, out n);
				content=content.Replace("a href='", "a href='http://monodoc/load?");
				content=content.Replace("a href=\"", "a href=\"http://monodoc/load?");
				webView.Render (content);
				addHistoryItem(bi.node.URL);

				outlineView.ExpandItem(bi);

			}
		} catch (Exception ex) { Console.WriteLine("ERROR: " + ex); }
	}

	[Export("webView:resource:willSendRequest:redirectResponse:fromDataSource:")]
	public URLRequest RequestHandler(WebView sender, Cocoa.Object identifier, URLRequest initialRequest, URLResponse urlResponse, WebDataSource datasource) {
//		if ( ((URL)(initialRequest.urL)).relativeString.ToString().IndexOf("http://monodoc/load?") == 0) {
		// FIXME
		if (initialRequest.URL.AbsoluteString.IndexOf ("http://monodoc/load?") == 0) {
			string url = initialRequest.URL.AbsoluteString.Replace("http://monodoc/load?", "");
			string content = "";
			if (url.StartsWith ("edit:")) {
//				XmlNode edit_node = EditingUtils.GetNodeFromUrl (url, help_tree);
//				Console.WriteLine (edit_node.InnerXml);
			}

			Node n;
			try {
				content = help_tree.RenderUrl(url, out n);
			} catch (Exception e) {
				content = "Exception Rendering the requested URL: " + e;
			}
			if(content != null && !content.Equals("")) {
				content=content.Replace("a href='", "a href='http://monodoc/load?");
				content=content.Replace("a href=\"", "a href=\"http://monodoc/load?");
				webView.Render (content);
				addHistoryItem(url);
			}
			return null;
		}
		return initialRequest;
	}
	
	private void addHistoryItem(string url) {
		webView.BackForwardList.Add (url);
	}
	
	private void loadHistoryItem(WebHistoryItem item) {
		string url = item.URL;
		string content = "";
		Node n;
		try {
			content = help_tree.RenderUrl(url, out n);
		} catch (Exception e) {
			content = "Exception Rendering the requested URL: " + e;
		}
		if (content != null && !content.Equals("")) {
			content=content.Replace("a href='", "a href='http://monodoc/load?");
			content=content.Replace("a href=\"", "a href=\"http://monodoc/load?");
			webView.Render (content);
		}
	}
	
	[Export("validateMenuItem:")]
	public bool validateMenuItem(Cocoa.Object sender) {
		MenuItem item = (MenuItem) sender;
//		if (item.Action.Equals("goBack:")) return webView.canGoBack;
//		if (item.Action.Equals("goForward:")) return webView.canGoForward;
		return true;
	}
	
	[Export("goBack:")]
	public void goBack(Cocoa.Object sender) {
		WebBackForwardList history = webView.BackForwardList;
		if (history.BackListCount > 0) {
			history.GoBack();
			loadHistoryItem(history.CurrentItem);
		}
	}
	
	[Export("goForward:")]
	public void goForward(Cocoa.Object sender) {
		WebBackForwardList history = webView.BackForwardList;
		if (history.ForwardListCount > 0) {
			history.GoForward();
			loadHistoryItem(history.CurrentItem);
		}
	}
}

class DocumentBrowser {
	public DocumentBrowser() {}
	public void Run() {
		Application.Init ();
		Application.LoadFramework ("WebKit");
		Application.LoadNib ("MonoDoc.nib");
		if (File.Exists ("monodoc.index")) {
			Rect rect;
			Image image = new Image ("mono.png");
			image.BackgroundColor = Color.Red;

			rect.Size = image.Size;
			rect.Origin.X = (Screen.Main.Frame.Size.Width-400)/2;
			rect.Origin.Y = (Screen.Main.Frame.Size.Height-300)/2;
			rect.Size.Height += 20;
			
			Window window = new Window (rect, 0, BackingStoreType.Buffered, false);
			rect.Origin.X = 0;
			rect.Origin.Y = 0;
			rect.Size.Height = 20;

			ProgressIndicator p_indicator = new ProgressIndicator (rect);
			window.View.AddSubview (p_indicator);

			rect.Origin.X = 0;
			rect.Origin.Y = 20;
			rect.Size = image.Size;

			ImageView imageview = new ImageView (rect);
			imageview.Image = image;
			window.View.AddSubview (imageview);

			p_indicator.StartAnimation ();
			System.Threading.Thread t = new System.Threading.Thread (new ThreadStart (MakeIndex));
			t.Start ();
			IntPtr session = Application.SharedApplication.ModalSessionForWindow (window);
			window.Show ();
			while (t.IsAlive) { 
				Application.SharedApplication.RunModalSession (session);
				System.Threading.Thread.Sleep (50);
			}
			Application.SharedApplication.EndModalSession (session);
			p_indicator.StopAnimation ();
			window.Close ();
		}
		Application.Run ();
	}
	static void MakeIndex ()
	{
		RootTree.MakeIndex ();
	}
}

class BrowserItem : Cocoa.Object {
	internal Node node;
	internal IList items = null;
	internal Cocoa.String caption;

	public BrowserItem (IntPtr native_object) : base (native_object) {}

	public BrowserItem(Node _node) : base () {
		node = _node;
		caption = new Cocoa.String (node.Caption);
		caption.Retain ();
	}
	
	public int Count { 
		get { 
			if(node == null || node.Nodes == null)
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
	public Cocoa.Object ValueAt(object identifier)
	{
		return caption;
	}
	public override string ToString()
	{
		return "BrowserItem: " + (node != null ? node.Caption : "null");
	}
}

[Register("IndexDataSource")]
class IndexDataSource : Cocoa.Object {
	static IndexReader index_reader;
	IndexEntry current_entry = null;

	static IndexDataSource() {
		index_reader = RootTree.LoadTree().GetIndex();
	}

	public IndexDataSource (IntPtr native_object) : base (native_object) {}

	public static IndexEntry GetEntry(int entry) {
		if(index_reader != null)
			return index_reader.GetIndexEntry(entry);
		else
			return null;
	}

	[Export("browser:numberOfRowsInColumn:")]
	public int NumberOfRowsInColumn(Browser browser, int columnNumber) {
		if(index_reader == null)
			return 1;
		return index_reader.Rows;
	}
	[Export("browser:willDisplayCell:atRow:column:")]
	public void DisplayCell(Browser browser, BrowserCell cell, int rowNumber, int columnNumber) {
		if(index_reader == null) 
			cell.Value = "Index Not Created";
		else
			cell.Value = index_reader.GetValue(rowNumber);
		cell.LeafNode = true;
	}

	public static int FindClosest (string text)
        {
		if(index_reader == null)
			return 1;

                int low = 0;
                int top = index_reader.Rows-1;
                int high = top;
                bool found = false;
                int best_rate_idx = Int32.MaxValue, best_rate = -1;

                while (low <= high){
                        int mid = (high + low) / 2;


                        string s;
                        int p = mid;
                        for (s = index_reader.GetValue (mid); s [0] == ' ';){
                                if (p == high){
                                        if (p == low){
                                                if (best_rate_idx != Int32.MaxValue){
                                                        return best_rate_idx;
                                                } else {
                                                        return p;
                                                }
                                        }

                                        high = mid;
                                        break;
                                }

                                if (p < 0)
                                        return 0;

                                s = index_reader.GetValue (++p);
                        }
                        if (s [0] == ' ')
                                continue;
                        int c, rate;
                        c = Rate (text, s, out rate);
                        if (rate >= best_rate){
                                best_rate = rate;
                                best_rate_idx = p;
                        }
                        if (c == 0)
                                return mid;

                        if (low == high){
                                if (best_rate_idx != Int32.MaxValue)
                                        return best_rate_idx;
                                else
                                        return low;
                        }

                        if (c < 0){
                                high = mid;
                        } else {
                                if (low == mid)
                                        low = high;
                                else
                                        low = mid;
                        }
                }

                if (best_rate_idx != Int32.MaxValue)
                        return best_rate_idx;
                else
                        return high;

        }
	public static int Rate (string user_text, string db_text, out int rate)
        {
                int c = System.String.Compare (user_text, db_text, true);
                if (c == 0){
                        rate = 0;
                        return 0;
                }

                int i;
                for (i = 0; i < user_text.Length; i++){
                        if (db_text [i] != user_text [i]){
                                rate = i;
                                return c;
                        }
                }
                rate = i;
                return c;
        }
}
[Register("BrowserDataSource")]
class BrowserDataSource : Cocoa.Object {

	internal RootTree help_tree;
	internal IList items = new ArrayList();

	public static BrowserItem BrowserItemForNode(Node n) {
		//WE NEED TO FIND A WAY TO DO THIS THAT ISN'T THIS EXPEIVE
		/*
		foreach (BrowserItem bi in items) {
			if(bi.node == n)
				return bi;
			else
				BrowserItemForNode(bi.node);
		}*/
		return null;
	}

	public BrowserDataSource(RootTree _tree) {
		help_tree = _tree;
		foreach (Node node in help_tree.Nodes)
			items.Add(new BrowserItem(node));
	}

	public BrowserDataSource (IntPtr native_object) : base (native_object) {
		help_tree = RootTree.LoadTree();
		foreach (Node node in help_tree.Nodes)
			items.Add(new BrowserItem(node));

	}
	~ BrowserDataSource () {
	}

	[Export("outlineView:numberOfChildrenOfItem:")]
	public int OutlineViewNumberOfChildrenOfItem(OutlineView outlineView, Cocoa.Object item)
	{
		BrowserItem bi = item as BrowserItem;
		int count = bi != null ? bi.Count : help_tree.Nodes.Count;
		return count;
	}

	[Export("outlineView:isItemExpandable:")]
	public bool OutlineViewIsItemExpandable(OutlineView outlineView, Cocoa.Object item)
	{
		return OutlineViewNumberOfChildrenOfItem(outlineView,item) > 0;
	}

	[Export("outlineView:child:ofItem:")]
	public BrowserItem OutlineViewChildOfItem(OutlineView outlineView, int index, Cocoa.Object item)
	{
		BrowserItem bi = item as BrowserItem;
		if (bi != null)
			bi = bi.ItemAt(index);
		else
			bi = (BrowserItem)items[index];
		return bi;
	}

	[Export("outlineView:objectValueForTableColumn:byItem:")]
	public Cocoa.Object OutlineViewObjectValueForTableColumnByItem(OutlineView outlineView, TableColumn tableColumn, Cocoa.Object item)
	{
		BrowserItem bi = item as BrowserItem;
		
		return bi == null ? null : bi.ValueAt(tableColumn.Identifier);
	}
	
   }
}
