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
			// We need to chop back to the real .Net Method Name
			if(m.IndexOf(":") >= 0) 
				m = m.Substring(0, m.IndexOf(":"));
			return s.GetType().InvokeMember(m, BindingFlags.Default | BindingFlags.InvokeMethod, null, s, a);
		}

		public static string GenerateMethodSignature(Type t, String m) {
			
			// We need to chop back to the real .Net Method Name
			if(m.IndexOf(":") >= 0)
				m = m.Substring(0, m.IndexOf(":"));

			int totalSize = 8;
			int curSize = 8;
			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, m)))
				totalSize += 4;

			string types = "";

			if(GetMethodByTypeAndName(t, m).ReturnType == typeof(void))
				types = "v";
			else
				types = "@";
			
			types += totalSize;
			types += "@0:4";

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, m)))
			{
				types += "@" + curSize;
				curSize += 4;
			}
				
			return types;
		}

		public static ObjCClassRepresentation GenerateObjCRepresentation(Type t) {
			ObjCClassRepresentation r = new ObjCClassRepresentation();
			PopulateObjCClassRepresentationMethods(t, r);
			PopulateObjCMethodSignatures(t, r);
			return r;	
		}
		
		private static void PopulateObjCClassRepresentationMethods(Type t, ObjCClassRepresentation r) {
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms) {
				string name = m.Name;
				ParameterInfo[] parms = GetParameterInfosByMethod(m);
				if(parms.Length >= 1)
					name += ":";
				for(int i = 1; i < parms.Length; i++)
					name += parms[i].Name + ":";

				a.Add(name);
			}
			r.Methods = (String[])a.ToArray(typeof(String));
		}
		
		private static void PopulateObjCMethodSignatures(Type t, ObjCClassRepresentation r) {
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms)
				a.Add(GenerateMethodSignature(t, m.Name));
			r.Signatures = (String[])a.ToArray(typeof(String));
		}
	}
}
