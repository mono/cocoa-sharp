//
//  Category.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Category.cs,v 1.4 2004/06/22 12:04:12 urs Exp $
//

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Category 
	{
		private IDictionary mMethods;
		private string mName;
		private string mClass;
		private string[] mImports;
		private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
		public Category(string _name, string _class) 
		{
			mName = _name;
			mClass = _class;
			mMethods = new Hashtable();
		}
        
		public string[] Imports 
		{
			get { return mImports; } set { mImports = value; }
		}
		public string Class 
		{
			get { return mClass; } set { mClass = value; }
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

//	$Log: Category.cs,v $
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//
//
