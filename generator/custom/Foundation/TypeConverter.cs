//
//  TypeConverter.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/custom/Foundation/TypeConverter.cs,v 1.3 2004/06/25 13:37:52 urs Exp $
//

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class TypeConverter {
		public static object NS2Net(IntPtr raw) {
			NSObject ret = new NSObject(raw,false);
			string className = ret.ClassName;
			Type type = Type.GetType("Apple.Foundation." + className + ", Apple.Foundation");
			if (type == null)
				type = Type.GetType("Apple.AppKit." + className + ", Apple.AppKit");
			if (type != null) {
				Console.WriteLine("<Using type: " + type.FullName + ", for Objective-C class: " + className);
				ConstructorInfo c = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,null,
					new Type[] {typeof(IntPtr),typeof(bool)},null);
				if (c != null)
					ret = (NSObject)c.Invoke(new object[]{raw,false});
				else
					Console.WriteLine("No constructor for " + type.FullName + " with (IntPtr,bool) found");
			}
			else
				Console.WriteLine(className + " not in Foundation or AppKit");
			if((ret as Apple.Foundation.NSString) != null)
				return ret.ToString();

			return ret;
		}
		
		public static IntPtr Net2NS(object obj) {
			if (obj == null) return IntPtr.Zero;
			if (obj is IntPtr) return (IntPtr)obj;
			NSObject nsObj = obj as NSObject;
			if (nsObj != null) return nsObj.Raw;
			string str = obj as string;
			if (str != null) return new NSString(str).Raw;
			throw new Exception("Net2NS: not handled type of object: " + obj.GetType());
		}
	}
}

//***************************************************************************
//
// $Log: TypeConverter.cs,v $
// Revision 1.3  2004/06/25 13:37:52  urs
// NS2Net string fix
//
// Revision 1.2  2004/06/25 03:10:27  gnorton
// Missed one file; sorry
//
// Revision 1.1  2004/06/24 03:47:30  urs
// initial custom stuff
//
// Revision 1.3  2004/06/20 02:07:25  urs
// Clean up, move Apple.Tools into Foundation since it will need it
// No need to allocate memory for getArgumentAtIndex of NSInvocation
//
// Revision 1.2  2004/06/19 20:42:59  gnorton
// Code cleanup (remove some old methods/clean some console.writelines)
// Modify NS2Net and NSObject destructor to be able to FreeCoTaskMem that we allocate in our argument parser.
//
// Revision 1.1  2004/06/17 16:10:45  gnorton
// Cleanup
//
// Revision 1.12  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.11  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.10  2004/06/17 05:48:00  gnorton
// Modified to move non apple stuff out of NSObject
//
// Revision 1.9  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
