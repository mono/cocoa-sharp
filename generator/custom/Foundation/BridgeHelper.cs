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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/custom/Foundation/BridgeHelper.cs,v 1.8 2004/06/29 20:32:05 urs Exp $
//

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

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
                                        if (attr is ObjCExportAttribute) 
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

		public static void GetSignatureCode(ref string signatureString, ref int size, Type type)
		{
			//TODO: unsigned char, class object, selector, array, structure, union, bnum, ^type, ?
			if(type == typeof(Char)) {
				signatureString += "c";
				size += Marshal.SizeOf(typeof(Char));
				return;
			}
			if(type == typeof(Int32)) {
				signatureString += "i";
				size += Marshal.SizeOf(typeof(Int32));
				return;
			}
			if(type == typeof(short)) {
				signatureString += "s";
				size += Marshal.SizeOf(typeof(Int16));
				return;
			}
			if(type == typeof(long)) {
				signatureString += "l";
				size += Marshal.SizeOf(typeof(Int32));
				return;
			}
			if(type == typeof(Int64)) {
				signatureString += "q";
				size += Marshal.SizeOf(typeof(Int64));
				return;
			}
			if(type == typeof(UInt32)) {
				signatureString += "I";
				size += Marshal.SizeOf(typeof(UInt32));
				return;
			}
			if(type == typeof(ushort)) {
				signatureString += "S";
				size += Marshal.SizeOf(typeof(UInt16));
				return;
			}
			if(type == typeof(ulong)) {
				signatureString += "L";
				size += Marshal.SizeOf(typeof(UInt32));
				return;
			}
			if(type == typeof(UInt64)) {
				signatureString += "Q";
				size += Marshal.SizeOf(typeof(UInt64));
				return;
			}
			if(type == typeof(float)) {
				signatureString += "f";
				size += Marshal.SizeOf(typeof(Single));
				return;
			}
			if(type == typeof(double)) {
				signatureString += "d";
				size += Marshal.SizeOf(typeof(Double));
				return;
			}
			if(type == typeof(bool)) {
				signatureString += "B";
				size += Marshal.SizeOf(typeof(Boolean));
				return;
			}
			if(type == typeof(void)) {
				signatureString += "v";
				return;
			}
			if(type == typeof(String)) {
				signatureString += "*";
				size += 4; // Marshal.SizeOf(typeof(String));
				return;
			}
			signatureString += "@";
			// This always seems to be 4 regardless of 64/32bitness
			size += 4;
			return;
		}
		
		public static string GenerateMethodSignature(Type t, String sel) 
		{
			string method = SelectorToMethodName(t, sel);
			foreach (Attribute attr in Attribute.GetCustomAttributes(GetMethodByTypeAndName(t, method)))
			{
				ObjCExportAttribute exprtAttr = attr as ObjCExportAttribute;
				if (exprtAttr != null) {
					if (exprtAttr.Signature != null)
						return exprtAttr.Signature;
					break;
				}
			}

			// We need to detect and generate the method signature according to:
			// http://developer.apple.com/documentation/Cocoa/Conceptual/ObjectiveC/4objc_runtime_overview/chapter_4_section_6.html
			// We need to convert primitive types to the corresponding letter code and use Marshal.SizeOf()
			// to get the correct size.
			
			// ID and SEL take the size of 8 bytes
			int totalSize = 8;
			int curSize = 8;
			string types = "";

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, method))) {
				if(p.ParameterType.IsPrimitive)
					totalSize += Marshal.SizeOf(p.ParameterType);
				else 
					totalSize += 4;
			}

			GetSignatureCode(ref types, ref curSize, GetMethodByTypeAndName(t, method).ReturnType);
			types += totalSize;
			types += "@0:4";
			curSize = 4;

			foreach(ParameterInfo p in GetParameterInfosByMethod(GetMethodByTypeAndName(t, method)))
			{
				GetSignatureCode(ref types, ref curSize, p.ParameterType);
				types += curSize;
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
					if (attr is ObjCExportAttribute) {
						a.Add(((ObjCExportAttribute)attr).Selector);
						addedByAttribute = true;
						break;
					}

#if REGISTER_ALL_METHODS
				if(!addedByAttribute) { 
					string name = m.Name;
					ParameterInfo[] parms = GetParameterInfosByMethod(m);
					if(parms.Length >= 1)
						name += ":";
					for(int i = 1; i < parms.Length; i++)
						name += parms[i].Name + ":";
	
					a.Add(name);
				}
#endif
			}
			r.Methods = (String[])a.ToArray(typeof(String));
		}
		
		private static void PopulateObjCMethodSignatures(Type t, ObjCClassRepresentation r) 
		{
			ArrayList a = new ArrayList();
			MethodInfo[] ms = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			foreach(MethodInfo m in ms) {
				bool addedByAttribute = false;
				foreach (Attribute attr in Attribute.GetCustomAttributes(m)) {
					ObjCExportAttribute exprtAttr = attr as ObjCExportAttribute;
					if (exprtAttr != null) {
						a.Add(exprtAttr.Signature != null ? exprtAttr.Signature : GenerateMethodSignature(t, m.Name));
						addedByAttribute = true;
						break;
					}
				}
#if REGISTER_ALL_METHODS
				if(!addedByAttribute)
					a.Add(GenerateMethodSignature(t, m.Name));
#endif
			}
			r.Signatures = (String[])a.ToArray(typeof(String));
		}
	}
}

//***************************************************************************
//
// $Log: BridgeHelper.cs,v $
// Revision 1.8  2004/06/29 20:32:05  urs
// More cleanup
//
// Revision 1.7  2004/06/29 18:28:46  gnorton
// Remove the ptr from the hashtable when we're DToring it.
// Remove some debugging WriteLines from NSO
//
// Revision 1.6  2004/06/29 16:42:34  gnorton
// Much better signature generator
//
// Revision 1.5  2004/06/29 15:24:25  gnorton
// Better support for different argument type (PtrTrStructure/StructureToPtr/SizeOf usage)
//
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
