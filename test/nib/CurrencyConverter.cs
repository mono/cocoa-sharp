/*
 This file is used to create a simple CurrencyConverter 
 application that should demostrate where Cocoa# can go

 While not required, it is recommended that you create create nib files using InterfaceBuilder.
 This is because it is much easier to use a nib file to create a UI than it is 
 to programmatical create the widgets and locate them correctly on the screen.

 The annotations in this code assume knowledge of C# and are based on Cocoa# 0.1.  Many changes may
 occur for 0.2 and beyond.
 
 While advanced features of Cocoa like delegates and notification are availbe, they are not
 covered in this class.

 Note, this example based on the PyObjC example 
 http://pyobjc.sourceforge.net/doc/step3-MainMenu.nib.zip
 
*/

using System;
using System.Runtime.InteropServices;

//get the Foundation and AppKit dlls
using Apple.Foundation;
using Apple.AppKit;

class MainClass {
	public void Run() {
		//This attempts to load the nib file.  Nibs are not required for Cocoa UIs
		//but they do make life much easier.  If the nib can't load then the program is pretty useless.
		if(!NSBundleAppKitExtras.LoadNibNamed_owner("CurrencyConverter.nib", NSApplication.SharedApplication)) {
			Console.WriteLine("Cant load nib");
			return;
		}
		//This is the one way to start the Cocoa run-loop and its probably the simpliest.  More advanced
		//ways to interact with the user are available and documented at http://developer.apple.com.
		//At this point the nib has loaded and the application is ready to interact with the user.
		NSApplication.SharedApplication.run();
	}
	static void Main(string[] args) {
		MainClass main = new MainClass();
		main.Run();
	}
}

// Example of a registered class overriding the objc name->.net name mapping
[Register("ConverterController")]
public class MyController : NSObject {
	
    // Example of a Connect with full definition
    [Connect(Name="converter", Type="@", Size=4)]
    public Converter _converter; 
	
    // Example of a Connect detecting
    [Connect]
		//In the step3-MainMenu.nib there is an dollarField Outlet that is defined as an NSTextField.  Here we declare that object
		//in the source so that dollarField can be used in the gui.  A quick diversion on actions, and outlets in nib files.
		//An action is a method that has been bound to a UI widget that is called by interacting with widget in some way.
		//An outlet is an object that is used to hold values that can be used by actions.
		//You can see a nib's actions and outlets in InterfaceBuilder or int *.nib/classes.nib
		public NSTextField dollarField; 
	
    [Connect(Name="rateField", Type="@", Size=4)]
		//same as for dallarField
		public NSTextField _rateField; 
    [Connect(Name="totalField", Type="@", Size=4)]
		//same as for dallarField
		public NSTextField _totalField; 
	
	protected MyController(IntPtr raw, bool release) : base(raw,release) {}
	
/* This is a required constructor for any extension class to register 
   with the objc system; if you dont implement this your subclass will not work.
   Since nib files actual fire ObjC methods, this allows the convert method to be "caught"
   and mapped to the C# method that we actually want to fire.
   The easiest way to think of this is a mapping from the ObjC method in the nib file
   to the actual C# method that you want to call.
*/
	[Export("convert:")]
		public void convert(object sender) {
			
			Console.WriteLine("ConverterController: convert");
			Console.WriteLine("converter=" + _converter);
			//Here the stringValue method of the NSTextField object is being accessed.
			//the dot seperator can be used to access any method on a Cocoa object.
			Console.WriteLine("dollarField=" + dollarField.stringValue);
			Console.WriteLine("rateField=" + _rateField.stringValue);
			
			_totalField.stringValue = _converter.convert(dollarField.stringValue, _rateField.stringValue);
			
			Console.WriteLine("totalField=" + _totalField.stringValue);
		}
}

//The Converter class extends NSObject which is the base class for all things Cocoa/Foundation.  
public class Converter : NSObject {
	protected Converter(IntPtr raw, bool release) : base(raw,release) {}
	
	[Export("convert:rate:")]
	
	//In the nib file the convert method is bound to the convert button, so convert gets called when the button is pressed
	public string convert(string dollar,string rate) {
        try
	{
		Console.WriteLine("Converter: convert({0},{1})",dollar,rate);
		return (float.Parse(dollar) * float.Parse(rate)).ToString();
	}
        catch (Exception e) {
            return e.ToString();
        }
	}
}
