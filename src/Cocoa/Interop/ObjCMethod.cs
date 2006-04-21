using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Cocoa {
	public class ObjCMethod {
		private static AssemblyBuilder builder;
		private static ModuleBuilder module;

		static ObjCMethod () {
			AssemblyName name = new AssemblyName ();
			name.Name = "MethodProxies";
			builder = AppDomain.CurrentDomain.DefineDynamicAssembly (name, AssemblyBuilderAccess.Run, null, null, null, null, null, true);
			module = builder.DefineDynamicModule ("Proxies");
		}

		public static objc_method FromConstructorInfo (ConstructorInfo constructor) {
			objc_method objc_method = new objc_method ();
			string name = SelectorFromConstructorInfo (constructor);
			string types = SignatureFromConstructorInfo (constructor); 

			foreach (ExportAttribute exattr in Attribute.GetCustomAttributes (constructor, typeof (ExportAttribute))) {
				name = exattr.Selector == null ? name : exattr.Selector;
				types = exattr.Signature == null ? types : exattr.Signature;
			}

			objc_method.name = Marshal.StringToHGlobalAnsi (name);
			objc_method.types = Marshal.StringToHGlobalAnsi (types);
			// FIXME
			ConstructorInfo constructor_proxy = BuildConstructorProxy (constructor);
			objc_method.imp = Delegate.CreateDelegate (MethodImplementation.FromConstructorInfo (constructor_proxy), null);

			return objc_method;
		}

		public static objc_method FromMethodInfo (MethodInfo method) {
			objc_method objc_method = new objc_method ();
			string name = SelectorFromMethodInfo (method);
			string types = SignatureFromMethodInfo (method); 

			foreach (ExportAttribute exattr in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
				name = exattr.Selector == null ? name : exattr.Selector;
				types = exattr.Signature == null ? types : exattr.Signature;
			}

			objc_method.name = Marshal.StringToHGlobalAnsi (name);
			objc_method.types = Marshal.StringToHGlobalAnsi (types);
			MethodInfo method_proxy = BuildMethodProxy (method);
			objc_method.imp = Delegate.CreateDelegate (MethodImplementation.FromMethodInfo (method_proxy), method_proxy);

			return objc_method;
		}

		private static string SignatureFromConstructorInfo (ConstructorInfo constructor) {
			string signature = "";
			int constructorsize = 0;
			int returnsize = 0;
			int typesize = 0;

			foreach (ParameterInfo param in constructor.GetParameters ()) {
				if (param.ParameterType.IsPrimitive)
					returnsize += Marshal.SizeOf (param.ParameterType);
				else
					returnsize += Marshal.SizeOf (typeof (IntPtr));
			}
		
			signature += ObjCTypes.FromType (typeof (IntPtr), out typesize);
			signature += returnsize;
			signature += "@0:" + Marshal.SizeOf (typeof (IntPtr)).ToString ();

			constructorsize = Marshal.SizeOf (typeof (IntPtr));

			foreach (ParameterInfo param in constructor.GetParameters ()) {
				signature += ObjCTypes.FromType (param.ParameterType, out typesize);
				constructorsize += typesize;
				signature += constructorsize;
			}

			return signature;
		}

		private static string SelectorFromConstructorInfo (ConstructorInfo method) {
			// FIXME
			return "initWithFrame:";
/*
			string selector = method.Name;
			ParameterInfo [] parameters = method.GetParameters ();

			if (parameters.Length > 0)
				selector += ":";
			
			for (int i = 1; i < parameters.Length; i++)
				selector += parameters [i].Name + ":";

			return selector;
*/
		}

		private static string SignatureFromMethodInfo (MethodInfo method) {
			string signature = "";
			int methodsize = 0;
			int returnsize = 0;
			int typesize = 0;

			foreach (ParameterInfo param in method.GetParameters ()) {
				if (param.ParameterType.IsPrimitive)
					returnsize += Marshal.SizeOf (param.ParameterType);
				else
					returnsize += Marshal.SizeOf (typeof (IntPtr));
			}
		
			signature += ObjCTypes.FromType (method.ReturnType, out typesize);
			signature += returnsize;
			signature += "@0:" + Marshal.SizeOf (typeof (IntPtr)).ToString ();

			methodsize = Marshal.SizeOf (typeof (IntPtr));

			foreach (ParameterInfo param in method.GetParameters ()) {
				signature += ObjCTypes.FromType (param.ParameterType, out typesize);
				methodsize += typesize;
				signature += methodsize;
			}

			return signature;
		}

		private static string SelectorFromMethodInfo (MethodInfo method) {
			string selector = method.Name;
			ParameterInfo [] parameters = method.GetParameters ();

			if (parameters.Length > 0)
				selector += ":";
			
			for (int i = 1; i < parameters.Length; i++)
				selector += parameters [i].Name + ":";

			return selector;
		}

		private static ConstructorInfo BuildConstructorProxy (ConstructorInfo constructor) {
			throw new Exception ("FIXME");
		}

		private static MethodInfo BuildMethodProxy (MethodInfo target) {
			TypeBuilder type = module.DefineType (Guid.NewGuid ().ToString (), TypeAttributes.Public | TypeAttributes.Class);

			ParameterInfo [] parameters = target.GetParameters ();
			Type [] parameter_types = new Type [parameters.Length + 2];

			parameter_types [0] = parameter_types [1] = typeof (IntPtr);

			for (int i = 0; i < parameters.Length; i++) {
				Type parameter_type = parameters [i].ParameterType;

				if (parameter_type.IsSubclassOf (typeof (Object)) || parameter_type == typeof (Object)) 
					parameter_types [2 + i] = typeof (IntPtr);
				else if (parameter_type == typeof (System.Object))
					parameter_types [2 + i] = typeof (IntPtr);
				else
					parameter_types [2 + i] = parameter_type;
			}

			MethodBuilder method = type.DefineMethod ("Dispatch", MethodAttributes.Public | MethodAttributes.Static, typeof (IntPtr), parameter_types);
			ILGenerator ilg = method.GetILGenerator ();
			LocalBuilder args = ilg.DeclareLocal (typeof (object []));
			
			args.SetLocalSymInfo ("args");

			ilg.Emit (OpCodes.Ldc_I4, parameters.Length);
			ilg.Emit (OpCodes.Newarr, typeof (object));
			ilg.Emit (OpCodes.Stloc_0);

			for (int i = 2; i < parameter_types.Length; i++) {
				ilg.Emit (OpCodes.Ldloc_0);
				ilg.Emit (OpCodes.Ldc_I4, i-2);
				ilg.Emit (OpCodes.Ldarg, i);
				if (parameter_types [i].IsValueType)
					ilg.Emit (OpCodes.Box, parameter_types [i]);

				ilg.Emit (OpCodes.Stelem_I4);
			}

			ilg.Emit (OpCodes.Ldarg_0);
			ilg.Emit (OpCodes.Ldarg_1);
			ilg.Emit (OpCodes.Ldloc_0);

			ilg.Emit (OpCodes.Call, typeof (ObjCInterop).GetMethod ("Dispatch", BindingFlags.Public | BindingFlags.Static, null, new Type [] {typeof (IntPtr), typeof (IntPtr), typeof (object [])}, null));
			ilg.Emit (OpCodes.Ret);

			return type.CreateType ().GetMethod ("Dispatch", BindingFlags.Public | BindingFlags.Static);
		}
	}
}
