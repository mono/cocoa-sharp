using System;
using System.Runtime.InteropServices;

using Apple.Cocoa.Foundation;

class Test
{

	static void Main(string[] args)
	{
		NSAutoreleasePool _pool = new NSAutoreleasePool();
		Console.WriteLine("Initing pool");
		_pool.init();
		Console.WriteLine("Releasing pool");
		_pool.release();
	}
}
