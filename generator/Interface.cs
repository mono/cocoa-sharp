//
//  Interface.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Interface.cs,v 1.4 2004/06/22 12:04:12 urs Exp $
//

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Interface 
	{
		private IDictionary mMethods;
		private string mFramework;
		private string mName;
		private string mChild;
		private string[] mProtos;
		private string[] mImports;
		private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
		public Interface(string _name, string _child, string _protos, string _framework) 
		{
			mName = _name;
			mChild = _child;
			mFramework = _framework;
			_protos = _protos.Replace(" ", "");		
			mProtos = _protos.Split(new char[]{','});
			mMethods = new Hashtable();
		}
        
		public string Child 
		{
			get { return mChild; } set { mChild = value; }
		}
        
		public string[] Protocols 
		{
			get { return mProtos; } set { mProtos = value; }
		}
        
		public string[] Imports 
		{
			get { return mImports; } set { mImports = value; }
		}
		public string Framework 
		{
			get { return mFramework; } set { mFramework = value; }
		}

		public string Name 
		{
			get { return mName; } set { mName = value; }
		}
        
		public IDictionary Methods 
		{
			get { return mMethods; }
		}
        
		public void AddMethods(string methods) 
		{
			string[] splitMethods = methods.Split('\n');
			foreach(string method in splitMethods) 
				if(mMethodRegex.IsMatch(method) && mMethods[method] == null)
					mMethods.Add(method, new Method(method));
		}
	}
}

//	$Log: Interface.cs,v $
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//
//

