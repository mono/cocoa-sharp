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

[ObjCExport]
public class ConverterController : NSObject {
	[ObjCExport]
    public Converter converter; 
	[ObjCExport]
    public NSTextField dollarField; 
	[ObjCExport]
    public NSTextField rateField; 
	[ObjCExport]
    public NSTextField totalField; 

	protected ConverterController(IntPtr raw, bool release) : base(raw,release) {}

	[ObjCExport("convert:")]
	public void convert(object sender) {
	    _UpdateMembers();
        
Console.WriteLine("ConverterController: convert");
Console.WriteLine("converter=" + converter);
Console.WriteLine("dollarField=" + dollarField.stringValue);
Console.WriteLine("rateField=" + rateField.stringValue);

        totalField.stringValue = converter.convert(dollarField.stringValue, rateField.stringValue);

Console.WriteLine("totalField=" + totalField.stringValue);
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
