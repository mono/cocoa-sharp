using System;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;   
using System.Runtime.InteropServices;

namespace Apple.Tools {
	public class ObjCMessaging {
		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		static AssemblyBuilder builder = null;
		static AssemblyName an = null;
		static ModuleBuilder module = null;
		static Hashtable types = new Hashtable();
		static void GenerateAssembly(string type) {
			string[] argstypes = type.Split('_');
			Type rettype = System.Type.GetType(argstypes[0]);
			Type[] args = new Type[argstypes.Length-1]; 
			for (int i = 0; i < argstypes.Length-1; ++i)
				args[i] = System.Type.GetType(argstypes[i+1]);

			if (an == null) {
				an = new AssemblyName();
				an.Name = "Apple.ObjCMessaging";
			}
			if (builder == null) 
				builder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
			if (module == null)
				module = builder.DefineDynamicModule(an.Name);

			TypeBuilder tb = module.DefineType(type, TypeAttributes.Public);
			MethodBuilder method = tb.DefinePInvokeMethod("objc_msgSend", "libobjc.dylib", MethodAttributes.PinvokeImpl|MethodAttributes.Static|MethodAttributes.Public, CallingConventions.Standard, rettype, args, CallingConvention.Winapi, CharSet.Auto);
			tb.CreateType();
			types.Add(type, 1);
		}
		static Assembly TypeResolve(string type)
		{
			Console.WriteLine ("TypeResolve fired for: {0}", type);
			if (types[type] == null)
				GenerateAssembly(type);
			foreach (Type t in module.Assembly.GetTypes ())
				Console.WriteLine ("AssemblyType:={0}", t);
			return module.Assembly;
		}

		public static object objc_msgSend (IntPtr receiver, string selector, Type rettype) {
			string type = rettype.ToString() + "_System.IntPtr_System.IntPtr";
			Console.WriteLine ("Looking for {0}", type);
			Type t = TypeResolve(type).GetType(type);
			object[] realArgs = new object[2];
			realArgs[0] = receiver;
			realArgs[1] = sel_registerName(selector);
			return t.InvokeMember("objc_msgSend", BindingFlags.InvokeMethod|BindingFlags.Public|BindingFlags.Static, null, null, realArgs);

		}
		public static object objc_msgSend (IntPtr receiver, string selector, Type rettype, params object[] args) {
			StringBuilder type = new StringBuilder();
			type.AppendFormat("{0}_System.IntPtr_System.IntPtr", rettype);
			for (int i = 0; i < args.Length; i+=2) {
				type.AppendFormat("_{0}", args[i]);
			}
			Type t = TypeResolve(type.ToString()).GetType(type.ToString());
			object[] realArgs = new object[(args.Length/2)+2];
			realArgs[0] = receiver;
			realArgs[1] = sel_registerName(selector);
			for (int i = 1; i < args.Length; i+=2) 
		                realArgs[i+2] = args[i];
			object o = Activator.CreateInstance(t);
			return t.InvokeMember("objc_msgSend", BindingFlags.InvokeMethod|BindingFlags.Public|BindingFlags.Static, null, o, realArgs);

		}
	}
}

