using System;
using System.Reflection;

using Apple.Foundation;
using Apple.AppKit;

class CSControl : NSObject {
	
	static IntPtr CSControl_class;

	NSButton swap1;
	public CSControl() {
		NSRegisterClass(this);
		CSControl_class = Apple.Foundation.NSString.NSClass("CSControl");
		Raw = NSObject__alloc(CSControl_class);
	}

	protected internal CSControl(IntPtr raw) : base(raw) {}

/*	public void NSRegisterClass(Object toRegister) {
		Type objIndType = toRegister.GetType();

		Console.WriteLine("NAME: {0}", objIndType.Name);
		MethodInfo[] objAllMethods = objIndType.GetMethods();
		foreach(MethodInfo objIndMethodInfo in objAllMethods)
		{
			Console.WriteLine("METH: {0}", objIndMethodInfo.Name);
		}

		//Get the property info
		PropertyInfo[] objAllProps = objIndType.GetProperties();
		foreach(PropertyInfo objIndPropInfo in objAllProps)
		{
			Console.WriteLine("PROP: {0}", objIndPropInfo.Name);
		}

		//Get the Constructor info...
		ConstructorInfo[] objAllCons = objIndType.GetConstructors();
		foreach(ConstructorInfo objIndCons in objAllCons)
		{
			Console.WriteLine("CON: {0}", objIndCons.Name);
		}
	} */

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
		monoButton.setTarget(CSControl_class);
		monoButton.setAction(NSString.NSSelector("_stop"));
		
		swap1 = new NSButton();
		swap1.initWithFrame(new NSRect(40, 40, 104, 17));
		swap1.setTitle(new NSString("Tickle me"));
		swap1.setTarget(CSControl_class);
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
