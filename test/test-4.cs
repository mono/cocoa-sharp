using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

delegate void ObjcDelegate(String method);

class MainClass {
	[DllImport("test-4")]
	static extern void callManagedDelegate(string method, ObjcDelegate d);
	
	static void Main(string[] args) {
		Console.WriteLine("Creating class.");
		CoolClass c = new CoolClass();
		Console.WriteLine("Creating delegate.");
		ObjcDelegate d = new ObjcDelegate(c.MethodInvoker);
		Console.WriteLine("Calling delegate: print");
		callManagedDelegate("print", d);
		Console.WriteLine("Calling delegate: increment");
		callManagedDelegate("increment", d);
		Console.WriteLine("Calling delegate: print");
		callManagedDelegate("print", d);
	}
}

public class CoolClass {

	public int i = 0;
	public void print() {
		Console.WriteLine("i={0}", i);
	}

	public void increment() {
		i++;
	}

	public void MethodInvoker(String method) {
		this.GetType().InvokeMember(method, BindingFlags.Default | BindingFlags.InvokeMethod, null, this, null);
	}
	public CoolClass()
	{
		i = 1;
	}
}
