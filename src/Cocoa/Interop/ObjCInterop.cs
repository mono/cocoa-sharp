using System;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;

namespace Cocoa {

	public class ObjCInterop {
		public static IntPtr Dispatch (IntPtr objc_class_ptr, IntPtr objc_selector, object [] arguments) {
			Object target = Object.FromIntPtr (objc_class_ptr);
			string selector = Marshal.PtrToStringAuto (objc_selector);
			MethodInfo method = MethodInfoFromSelector (selector, target.GetType ());
			IntPtr return_value = IntPtr.Zero;

			arguments = ArgumentsFromNative (arguments, method);

			target.ImportMembers ();

			try {
				return_value = ReturnToNative (method.Invoke (target, arguments));
			} catch (ArgumentException) {
				Console.WriteLine ("Dispatch.Invoke caused an argument exception:");
				ParameterInfo [] parameters = method.GetParameters ();
				for (int i = 0; i < parameters.Length; i++)
					Console.WriteLine ("\t{0} {1} {2}", (arguments [i] != null ? arguments [i].GetType ().ToString () : "null"), ((arguments [i] != null ? arguments [i].GetType () : typeof (void)) == parameters [i].ParameterType ? "==" : "!="), parameters [i].ParameterType.ToString ());
			}
			target.ExportMembers ();

			return return_value;
		}

		private static MethodInfo MethodInfoFromSelector (string selector, Type type) {
			MethodInfo [] methods = type.GetMethods (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

			foreach (MethodInfo method in methods) {
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
					if (export_attribute.Selector != null && export_attribute.Selector == selector)
						return method;
				}
			}

			string methodname = selector;

			if (methodname.IndexOf (":") > 0)
				methodname = methodname.Substring (0, methodname.IndexOf (":"));

			foreach (MethodInfo method in methods) {
				if (method.Name == methodname)
					return method;
			}

			throw new InvalidOperationException ("Couldn't find a method to bind for " + selector);
                }  

		private static object [] ArgumentsFromNative (object [] arguments, MethodInfo method) {
			object [] new_arguments = new object [arguments.Length];
			ParameterInfo [] parameters = method.GetParameters ();

			if (new_arguments.Length != parameters.Length)
				throw new ArgumentException ("The number of provided arguments does not match the expected count for the method");
			for (int i = 0; i < arguments.Length; i++) {
				if (parameters [i].ParameterType.IsSubclassOf (typeof (Object)) || parameters [i].ParameterType == typeof (Object)) {
					if (arguments [i].GetType () != typeof (IntPtr))
						throw new ArgumentException ("Argument mismatch.  Attempted to convert a " + arguments  [i].GetType () + " to a " + parameters [i].ParameterType);
					
					new_arguments [i] = Object.FromIntPtr ((IntPtr) arguments [i]);
				} else {
					new_arguments [i] = Convert.ChangeType (arguments [i], parameters [i].ParameterType);
				}
			}

			return new_arguments;
		}

		private static IntPtr ReturnToNative (object toconvert) {
			if (toconvert == null)
				return IntPtr.Zero;
			if (toconvert is Cocoa.Object)
				return ((Cocoa.Object)toconvert).NativeObject;
			if (toconvert is System.Int32)
				return (IntPtr)(int)toconvert;
			if (toconvert is System.Boolean)
				return ((bool)toconvert ? (IntPtr)1 : (IntPtr)0);
			if (toconvert is System.IntPtr)
				return (IntPtr)toconvert;

			throw new Exception ("Unhandled return type: " + toconvert.GetType ().FullName);
		}
	}
}

