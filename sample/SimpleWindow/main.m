#import <Cocoa/Cocoa.h>
#import <WebKit/WebKit.h>
#import "CSApplication.h"

#if true
@interface BrowserItem : NSObject {
	NSString* node;
	int count;
}

-(void) initWithString: (NSString*) _node count: (int) _count;
-(int) count;
-(id) itemAt: (int) ndx;
-(id) valueAt: (id) identifier;
@end

@implementation BrowserItem

-(void) initWithString: (NSString*) _node count: (int) _count;
{
	node = _node;
	count = _count;
}

-(int) count
{
	return count;
}

-(id) itemAt: (int) ndx
{
	return nil;
}

- (id) valueAt: (id) identifier
{
	return node;
}
@end

@interface BrowserController : NSObject {
}

@end

@implementation BrowserController

- (int)outlineView: (NSOutlineView*) outlineView numberOfChildrenOfItem: (id) item
{
	BrowserItem* bi = (BrowserItem*)item;
	int count = bi != nil ? [bi count] : 10;
NSLog(@"DEBUG: OutlineViewNumberOfChildrenOfItem: %@ --> %i",item,count);
	return count;
}
	
- (BOOL)outlineView: (NSOutlineView*) outlineView isItemExpandable: (id) item
{
	return [self outlineView: outlineView numberOfChildrenOfItem: item] > 0;
}
	
- (id) outlineView: (NSOutlineView*) outlineView child: (int)index ofItem: (id) item
{
NSLog(@"DEBUG: OutlineViewChildOfItem: %i, item: %@", index, item);
	BrowserItem* bi = (BrowserItem*)item;
	if (bi != nil)
		bi = [bi itemAt: index];
	else
		bi = [[BrowserItem alloc] initWithString: @"bla" count: index % 3];
	return bi;
}
	
- (id) outlineView: (NSOutlineView*) outlineView objectValueForTableColumn: (NSTableColumn*) tableColumn byItem: (id) item
{
NSLog(@"DEBUG: OutlineViewObjectValueForTableColumnByItem: %@, for column: %@", item, [tableColumn identifier]);
	BrowserItem* bi = (BrowserItem*)item;
	
	return bi == nil ? nil : [bi valueAt: [tableColumn identifier]];
}
	
@end

@interface Browser : NSObject {
	// The window controller
	NSWindowController* Windowcontroller;
	// The main window
	NSWindow* Window;
	// Where we render the contents
	WebView* Webview;
	WebFrame* mainFrame;
}

@end

@implementation Browser
-(void) init
{
	Window = [[NSWindow alloc] initWithContentRect: NSMakeRect(200, 180, 800, 600)
		 styleMask: NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask | NSResizableWindowMask
										   backing: NSBackingStoreBuffered defer: NO];
	[Window setTitle: @"Monodoc"];
	Windowcontroller = [[NSWindowController alloc] initWithWindow: Window];
	NSView* drawerView = [[NSView alloc] initWithFrame: NSMakeRect(0,0,250,480)];
	[drawerView setAutoresizingMask: NSViewWidthSizable | NSViewHeightSizable];
	NSDrawer* drawer = [[NSDrawer alloc] initWithContentSize: NSMakeSize(250,480) preferredEdge: NSMaxXEdge];
	[drawer setParentWindow: Window];
	[drawer setContentView: drawerView];
		
	NSTabView* tabView = [[NSTabView alloc] initWithFrame: NSMakeRect(0,0,250,480)];
	[tabView setAutoresizingMask: NSViewWidthSizable | NSViewHeightSizable];
	[drawerView addSubview: tabView];

	NSTabViewItem* tabViewItem = [[NSTabViewItem alloc] initWithIdentifier: @"Contents"];
	[tabViewItem setLabel: @"Contents"];
	[tabView addTabViewItem: tabViewItem];
//		NSScrollView sv = new NSScrollView(tabView.contentRect);
//		sv.autoresizingMask = NSViewWidthSizable | NSViewHeightSizable;
//		tabViewItem.view = sv;
		
	NSOutlineView* ov = [[NSOutlineView alloc] initWithFrame: [tabView contentRect]];
	[ov setAutoresizingMask: NSViewWidthSizable | NSViewHeightSizable];
//		sv.documentView = ov;
	NSTableColumn* c = [[NSTableColumn alloc] initWithIdentifier: @"Caption"];
	[c setWidth: 100];
	[c setEditable: NO];
	[c setResizable: TRUE];
	[[c headerCell] setStringValue: @"Caption"];
	[ov addTableColumn: c];
	[ov setOutlineTableColumn: c];
	
	[tabViewItem setView: ov];

	BrowserController* browserController = [[BrowserController alloc] init];
	[ov setDataSource: browserController];

/*
	tabViewItem = new NSTabViewItem("Index");
	tabViewItem.label = "Index";
	tabView.addTabViewItem(tabViewItem);
	tabViewItem = new NSTabViewItem("Search");
	tabViewItem.label = "Search";
	tabView.addTabViewItem(tabViewItem);
*/	
	//Webview = [[WebView alloc] initWithFrame: NSMakeRect(250, 0, 550, 600)];
	//mainFrame = (WebFrame*)[Webview mainFrame];
	
	//[[Window contentView] addSubview: Webview];
		
	[drawer open];
	
	[Windowcontroller showWindow: [NSApplication sharedApplication]];
}
	
- (void)didDoubleClick {
}

@end

int main (int argc, const char * argv[]) {
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	[NSApplication sharedApplication];
	[[NSApplication sharedApplication] setApplicationIconImage: [NSImage imageNamed: @"mono.icns"]];

	Browser *browser = [Browser new];

	[[NSApplication sharedApplication] run];
	
	[browser release];
	[pool release];
	return 0;
}
#else
int main (int argc, const char * argv[])
{
	//	we want to get OOed ASAP so lets call a static method that will get us out of main
	[CSApplication csApplicationMain];
	return 0;
}
#endif