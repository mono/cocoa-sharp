using System;

using Apple.Foundation;
using Apple.AppKit;

class Test
{
	static void Main(string[] args)
	{
		NSAutoreleasePool pool = new NSAutoreleasePool();
		pool.init();

		CSControl control = new CSControl();
		Console.WriteLine("When you click the button the console should say: "); control._stop();
		control.displayWindow();

		pool.release();
	}
}
