using System;
using Apple.Foundation;
using Apple.AppKit;

class MainClass {
	private ConverterController currencyconverter;
	private Converter converter;

	public MainClass() {
		currencyconverter = new ConverterController();
		converter = new Converter();
	}

	public void Run() {
		if(!NSBundleAppKitExtras.LoadNibNamed_owner("step3-MainMenu.nib", NSApplication.SharedApplication)) {
			Console.WriteLine("Cant load nib");
		}
		NSApplication.SharedApplication.run();
	}
	static void Main(string[] args) {
		MainClass main = new MainClass();
		main.Run();
	}
}

public class ConverterController : NSObject {

	[ObjCExport("convert:")]
	public void convert(object sender) {
	}
}

public class Converter : NSObject {

}

