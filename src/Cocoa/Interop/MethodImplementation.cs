using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace Cocoa {
	public class MethodImplementation {
		private static AssemblyBuilder builder;
		private static ModuleBuilder module;
		private static Hashtable implementations;
		
		static MethodImplementation () {
			AssemblyName an = new AssemblyName ();
			an.Name = "MethodImplementation";
			builder = AppDomain.CurrentDomain.DefineDynamicAssembly (an, AssemblyBuilderAccess.Run, null, null, null, null, null, true);
			module = builder.DefineDynamicModule ("Implementations", true);
			implementations = new Hashtable ();
		}

		public static Type FromMethodInfo (MethodInfo target) {
			return FromMethodBase ((MethodBase) target);
		}
		
		public static Type FromConstructorInfo (ConstructorInfo target) {
			return FromMethodBase ((MethodBase) target);
		}

		public static Type FromMethodBase (MethodBase target) {
			if (target == null)
				throw new ArgumentNullException ("target cannot be null");

			if (implementations [target] != null)
				return (Type) implementations [target];

			TypeBuilder type = module.DefineType (Guid.NewGuid ().ToString (), TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.AutoClass, typeof (MulticastDelegate));
			type.SetCustomAttribute (new CustomAttributeBuilder (typeof (MarshalAsAttribute).GetConstructor (new Type [] { typeof (UnmanagedType) }), new object [] { UnmanagedType.FunctionPtr }));

			ConstructorBuilder constructor = type.DefineConstructor (MethodAttributes.Public, CallingConventions.Standard, new Type [] { typeof (object), typeof (int) });

			constructor.SetImplementationFlags (MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

			ParameterInfo [] parameters = target.GetParameters ();
			Type [] param_types = new Type [parameters.Length];

			for (int i = 0; i < parameters.Length; i++) {
				param_types [i] = parameters [i].ParameterType;
			}
			
			MethodBuilder method = null;

			if (target is ConstructorInfo)
				method = type.DefineMethod ("Invoke", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, null, param_types);
			if (target is MethodInfo)
				method = type.DefineMethod ("Invoke", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, ((MethodInfo)target).ReturnType, param_types);
			if (method == null)
				throw new ArgumentException ("target must be a Constructor or a Method");

			method.SetImplementationFlags (MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

			implementations [target] = type.CreateType ();

			return (Type) implementations [target];
		}
	}
}
