//
//  Struct.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Struct.cs,v 1.3 2004/06/22 12:04:12 urs Exp $
//

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Struct 
	{
		private string mName;
		private string mFramework;
		private string mStruct;
        
		public Struct(string _name, string _struct, string _framework) 
		{
			mName = _name;
			mStruct = _struct;
			mFramework = _framework;
		}
        
		public string Name 
		{
			get { return mName; }
		}
		public string Framework 
		{
			get { return mFramework; }
		}
		public string CSStruct 
		{
			get 
			{
				string structVal = "using System;\n";
				structVal += "namespace Apple." + mFramework + " {\n";
				structVal += "public struct " + mName + " {\n" + mStruct + "}\n}";
				return structVal;
			}
		}
	}
}

//	$Log: Struct.cs,v $
//	Revision 1.3  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//
//
