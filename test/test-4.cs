using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Apple.Foundation;

delegate void ObjcDelegate(String method);
delegate void ObjcDelegateWithArgs(String method, IntPtr args);

class MainClass {
	[DllImport("test-4")]
	static extern void callManagedDelegate(string method, ObjcDelegate d);
	[DllImport("test-4")]
	static extern void callManagedDelegateWithArgs(string method, ObjcDelegateWithArgs d);
	
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
		Console.WriteLine("Creating delegateWithArgs.");
		ObjcDelegateWithArgs d2 = new ObjcDelegateWithArgs(c.MethodInvokerWithArgs);
		Console.WriteLine("Calling delegate: printWithArgs");
		callManagedDelegateWithArgs("printWithArgs", d2);
		Console.WriteLine("Calling delegate: incrementWithArgs");
		callManagedDelegateWithArgs("incrementWithArgs", d2);
		Console.WriteLine("Calling delegate: printWithArgs");
		callManagedDelegateWithArgs("printWithArgs", d2);
	}
}

public class CoolClass {

	public int i = 0;
	public void print() {
		Console.WriteLine("i={0}", i);
	}

	public void printWithArgs(IntPtr args) {
		ArgStruct realArgs = (ArgStruct)Marshal.PtrToStructure(args, typeof(ArgStruct));
		NSString string11 = new NSString(realArgs.string1,false);
		NSString string22 = new NSString(realArgs.string2,false);
		NSString string3 = new NSString("footest");
		Console.WriteLine("i={0}", i);
		Console.WriteLine("string3={0}", string3.ToString());
		Console.WriteLine("string22={0}", string22.ToString());
		Console.WriteLine("string11={0}", string11.ToString());
	}
	public void increment() {
		i++;
	}
	public void incrementWithArgs(IntPtr args) {
		i++;
	}

	public void MethodInvoker(String method) {
		this.GetType().InvokeMember(method, BindingFlags.Default | BindingFlags.InvokeMethod, null, this, null);
	}
	public void MethodInvokerWithArgs(String method, IntPtr args) {
		Console.WriteLine("MANAGED: Invoking {0}", method);
		this.GetType().InvokeMember(method, BindingFlags.Default | BindingFlags.InvokeMethod, null, this, new object[]{args});
	}
	public CoolClass()
	{
		i = 1;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ArgStruct {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=128)] public byte[] ppcstuff;
		[MarshalAs(UnmanagedType.U4)] public IntPtr id;
		[MarshalAs(UnmanagedType.U4)] public IntPtr sel;
		[MarshalAs(UnmanagedType.U4)] public IntPtr string1;
		[MarshalAs(UnmanagedType.U4)] public IntPtr string2;
	}
}
