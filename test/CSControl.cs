using System;
using System.Runtime.InteropServices;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSControl : NSObject {
	static IntPtr CSControl_class = NSRegisterClass(typeof(CSControl));

    [DllImport("Glue")]
    static extern IntPtr CreateClassDefinition(string name, string superclassName);

	NSButton swap1;
	public CSControl() : base(NSObject__alloc(CSControl_class)) {}

	protected internal CSControl(IntPtr raw) : base(raw) {}

	public static IntPtr NSRegisterClass(Type objIndType) {
		Console.WriteLine("NAME: {0}", objIndType.Name);

		foreach(MethodInfo objIndMethodInfo in objIndType.GetMethods())
			Console.WriteLine("METH: {0}", objIndMethodInfo.Name);

		foreach(PropertyInfo objIndPropInfo in objIndType.GetProperties())
			Console.WriteLine("PROP: {0}", objIndPropInfo.Name);

		foreach(ConstructorInfo objIndCons in objIndType.GetConstructors())
			Console.WriteLine("CON: {0}", objIndCons.Name);

		IntPtr cls = CreateClassDefinition(objIndType.Name,"NSObject");
		return cls;
	}

	public void displayWindow() {
		const int NSBorderlessWindowMask	= 0;
		const int NSTitledWindowMask		= 1 << 0;
		const int NSClosableWindowMask		= 1 << 1;
		const int NSMiniaturizableWindowMask	= 1 << 2;
		const int NSResizableWindowMask		= 1 << 3;
		
		const int NSBackingStoreBuffered	= 2;

		NSAutoreleasePool pool = new NSAutoreleasePool();
		pool.init();

		NSApplication.sharedApplication();
		NSRect contentRect = new NSRect(200, 180, 300, 300);

		NSWindow window = new NSWindow();
		window.initWithContentRect_styleMask_backing_defer(contentRect, 
			NSMiniaturizableWindowMask | NSClosableWindowMask | NSTitledWindowMask,
			NSBackingStoreBuffered, false);
		window.setTitle(new NSString("Hi Mono"));

		NSButton monoButton = new NSButton();
		monoButton.initWithFrame(new NSRect(20, 20, 74, 17));
		monoButton.setTitle(new NSString("Dismiss"));
		monoButton.setTarget(this);
		monoButton.setAction(NSString.NSSelector("_stop"));
		
		swap1 = new NSButton();
		swap1.initWithFrame(new NSRect(40, 40, 104, 17));
		swap1.setTitle(new NSString("Tickle me"));
		swap1.setTarget(this);
		swap1.setAction(NSString.NSSelector("_swap"));

		NSTextField text = new NSTextField();
		text.initWithFrame(new NSRect(100, 200, 78, 20));
		text.setEditable(false);
		text.setBezeled(true);
		text.setStringValue(new NSString("Hello, Mono"));

		((NSView)window.contentView()).addSubview(monoButton);
		((NSView)window.contentView()).addSubview(swap1);
		((NSView)window.contentView()).addSubview(text);
		window.center();
		window.makeKeyAndOrderFront(null);

		NSApplication.sharedApplication().runModalForWindow(window);

		monoButton.release();
		pool.release();
	}

	public void _stop() {
		Console.WriteLine("Cool ass SHIT!");
	}

	public void _swap() {
		swap1.setTitle(new NSString("I got tickled"));
	}
}
