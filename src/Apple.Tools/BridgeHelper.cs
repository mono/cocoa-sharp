using System;
using System.Collections;
using System.Reflection;

namespace Apple.Tools {

	public abstract class BridgeHelper {

		public static MethodInfo GetMethodByTypeAndName(Type t, String n) {
			return t.GetMethod(n);
		}

		public static ParameterInfo[] GetParameterInfosByMethod(MethodInfo m) {
			return m.GetParameters();
		}

		public static object InvokeMethodByObject(Object s, String m, Object[] a) {
			return s.GetType().InvokeMember(m, BindingFlags.Default | BindingFlags.InvokeMethod, null, s, a);
		}

		public static string GenerateMethodSignature(Type t, String m) {
			string types = "";

			if(GetMethodByTypeAndName(t, m).ReturnType == typeof(void))
				types = "v";
			else
				types = "@";
			
			types += "@:";

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, m)))
				types += "@";
				
			return types;
		}

		public static ObjCClassRepresentation GenerateObjCRepresentation(Type t) {
			ObjCClassRepresentation r = new ObjCClassRepresentation();
			PopulateObjCClassRepresentationMethods(t, ref r);
			PopulateObjCMethodSignatures(t, ref r);
			return r;	
		}
		
		private static void PopulateObjCClassRepresentationMethods(Type t, ref ObjCClassRepresentation r) {
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms) 
				a.Add(m.Name);
			r.Methods = (String[])a.ToArray(typeof(String));
			r.NumMethods = r.Methods.Length;
		}
		
		private static void PopulateObjCMethodSignatures(Type t, ref ObjCClassRepresentation r) {
			ArrayList a = new ArrayList();
			for(int i = 0; i < r.Methods.Length; i++)
				a.Add(GenerateMethodSignature(t, r.Methods[i]));
			r.Signatures = (String[])a.ToArray(typeof(String));
		}
	}
}
