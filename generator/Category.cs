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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Category.cs,v 1.5 2004/06/22 13:38:59 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Category : Element
	{
		private IDictionary mMethods;
		private string mClass;
		private string[] mImports;
		private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
		public Category(string _name, string _class) : base(string.Empty,_name,"???")
		{
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

		public override void WriteCS(TextWriter _cs)
		{
		}
	}
}

//	$Log: Category.cs,v $
//	Revision 1.5  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
