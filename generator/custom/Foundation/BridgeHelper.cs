//
//  BridgeHelper.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/custom/Foundation/BridgeHelper.cs,v 1.4 2004/06/28 19:18:31 urs Exp $
//

using System;
using System.Collections;
using System.Reflection;

namespace Apple.Tools 
{
	using Apple.Foundation;

	public abstract class BridgeHelper 
	{
		public static MethodInfo GetMethodByTypeAndName(Type t, String n) 
		{
			return t.GetMethod(n);
		}

		public static ParameterInfo[] GetParameterInfosByMethod(MethodInfo m) 
		{
			return m.GetParameters();
		}

		public static string SelectorToMethodName(Type t, string selector)
		{
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                        foreach(MethodInfo m in ms) 
                                foreach (Attribute attr in Attribute.GetCustomAttributes(m))
                                        if (attr.GetType() == typeof(ObjCExportAttribute)) 
						if ( ((ObjCExportAttribute)attr).Selector == selector )
							return m.Name;
			string methodName = selector;

			if(methodName.IndexOf(":") > 0)
				methodName = methodName.Substring(0, methodName.IndexOf(":"));
			return methodName;
		}

		public static object[] ProcessInvocation(Type type, NSInvocation invocation) 
		{
			string method = SelectorToMethodName(type, invocation.selector);

			ArrayList retArgs = new ArrayList();
			int i = 0;
			foreach(ParameterInfo pi in  GetParameterInfosByMethod(GetMethodByTypeAndName(type, method))) {
				retArgs.Add(invocation.getArgument(i, pi.ParameterType));
				i++;
			}

			return (object[])retArgs.ToArray(typeof(object));
		}

		public static object InvokeMethodByObject(Object self, String sel, Object[] args) 
		{
			string method = SelectorToMethodName(self.GetType(), sel);

			return self.GetType().InvokeMember(method, 
				BindingFlags.Default | BindingFlags.InvokeMethod, null, 
				self, args);
		}

		public static string GenerateMethodSignature(Type t, String sel) 
		{
			string method = SelectorToMethodName(t, sel);
			foreach (Attribute attr in Attribute.GetCustomAttributes(GetMethodByTypeAndName(t, method)))
				if (attr.GetType() == typeof(ObjCExportAttribute)) 
					if ( ((ObjCExportAttribute)attr).Signature != null )
						return ((ObjCExportAttribute)attr).Signature;

			int totalSize = 8;
			int curSize = 8;
			string types = "";

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, method)))
				totalSize += 4;

			if(GetMethodByTypeAndName(t, method).ReturnType == typeof(void))
				types = "v";
			else if(GetMethodByTypeAndName(t, method).ReturnType == typeof(int))
				types = "i";
			else
				types = "@";
			
			types += totalSize;
			types += "@0:4";

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, method)))
			{
				if(p.ParameterType == typeof(int)) 
					types += "i" + curSize;
				else
					types += "@" + curSize;
				curSize += 4;
			}
				
			return types;
		}

		public static ObjCClassRepresentation GenerateObjCRepresentation(Type t) 
		{
			ObjCClassRepresentation r = new ObjCClassRepresentation();
			PopulateObjCClassRepresentationMethods(t, r);
			PopulateObjCMethodSignatures(t, r);
			return r;	
		}
		
		private static void PopulateObjCClassRepresentationMethods(Type t, ObjCClassRepresentation r) 
		{
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms) 
			{
				bool addedByAttribute = false;
				foreach (Attribute attr in Attribute.GetCustomAttributes(m)) 
					if (attr.GetType() == typeof(ObjCExportAttribute)) {
						a.Add( ((ObjCExportAttribute)attr).Selector );
						addedByAttribute = true;
					}

				if(!addedByAttribute) { 
					string name = m.Name;
					ParameterInfo[] parms = GetParameterInfosByMethod(m);
					if(parms.Length >= 1)
						name += ":";
					for(int i = 1; i < parms.Length; i++)
						name += parms[i].Name + ":";
	
					a.Add(name);
				}
			}
			r.Methods = (String[])a.ToArray(typeof(String));
		}
		
		private static void PopulateObjCMethodSignatures(Type t, ObjCClassRepresentation r) 
		{
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms) {
				bool addedByAttribute = false;
				foreach (Attribute attr in Attribute.GetCustomAttributes(m)) 
					if (attr.GetType() == typeof(ObjCExportAttribute) && ((ObjCExportAttribute)attr).Signature != null) {
						a.Add( ((ObjCExportAttribute)attr).Signature );
						addedByAttribute = true;
					}
				if(!addedByAttribute)
					a.Add(GenerateMethodSignature(t, m.Name));
			}
			r.Signatures = (String[])a.ToArray(typeof(String));
		}
	}
}

//***************************************************************************
//
// $Log: BridgeHelper.cs,v $
// Revision 1.4  2004/06/28 19:18:31  urs
// Implement latest name bindings changes, and using objective-c reflection to see is a type is a OC class
//
// Revision 1.3  2004/06/27 20:41:45  gnorton
// Support for NSBrowser and int args/rets
//
// Revision 1.2  2004/06/25 18:43:27  gnorton
// Added ObjCExport attribute for subclassing registering selectors
//
// Revision 1.1  2004/06/24 03:47:30  urs
// initial custom stuff
//
// Revision 1.1  2004/06/20 02:07:25  urs
// Clean up, move Apple.Tools into Foundation since it will need it
// No need to allocate memory for getArgumentAtIndex of NSInvocation
//
//***************************************************************************
