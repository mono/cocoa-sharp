using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

class Test {
	[DllImport("test")]
	static extern void printAddr(IntPtr THIS);

	static IntPtr getObjAddress(Object toGet) {
		Type t = toGet.GetType();
		return (IntPtr)t.InvokeMember("obj_address", BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, toGet, null);
	}
	static void Main(string[] args) {
		Bar test = new Bar();
		Baz test2 = new Baz();
		IntPtr testptr = getObjAddress(test);
		Console.WriteLine("{0}", testptr);
		IntPtr testptr2 = getObjAddress(test2);
		Console.WriteLine("{0}", testptr2);
		printAddr(getObjAddress(test));
		printAddr(getObjAddress(test2));
	}
}

public class Bar {

	public int i = 2;
	public void print() {
		Console.WriteLine("i={0} - I got called from unmanaged", i);
	}
	public Bar()
	{
		i = 1;
	}
}
public class Baz {

	public int i = 2;
	public void myprint() {
		Console.WriteLine("i={0} - I got called from unmanaged", i);
	}
	public void print() {
		Console.WriteLine("i={0} - I got called from unmanaged", i);
	}
	public Baz()
	{
		i = 1;
	}
}
