using System;
using System.Runtime.InteropServices;

using Apple.Foundation;
using Apple.AppKit;

class MainClass {
	public void Run() {
		if(!NSBundleAppKitExtras.LoadNibNamed_owner("step3-MainMenu.nib", NSApplication.SharedApplication)) {
			Console.WriteLine("Cant load nib");
			return;
		}
		NSApplication.SharedApplication.run();
	}
	static void Main(string[] args) {
		MainClass main = new MainClass();
		main.Run();
	}
}

[ObjCRegister("ConverterController")]
public class MyController : NSObject {

    [ObjCConnect(Name="converter", Type="@", Size=4)]
    public Converter _converter; 
    [ObjCConnect(Name="dollarField", Type="@", Size=4)]
    public NSTextField _dollarField; 
    [ObjCConnect(Name="rateField", Type="@", Size=4)]
    public NSTextField _rateField; 
    [ObjCConnect(Name="totalField", Type="@", Size=4)]
    public NSTextField _totalField; 

	protected MyController(IntPtr raw, bool release) : base(raw,release) {}

	[ObjCExport("convert:")]
	public void convert(object sender) {
	    _UpdateMembers();
        
Console.WriteLine("ConverterController: convert");
Console.WriteLine("converter=" + _converter);
Console.WriteLine("dollarField=" + _dollarField.stringValue);
Console.WriteLine("rateField=" + _rateField.stringValue);

        _totalField.stringValue = _converter.convert(_dollarField.stringValue, _rateField.stringValue);

Console.WriteLine("totalField=" + _totalField.stringValue);
	}
}

public class Converter : NSObject {
	protected Converter(IntPtr raw, bool release) : base(raw,release) {}

	[ObjCExport("convert:rate:")]
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
