//
//  Protocol.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Protocol.cs,v 1.4 2004/06/22 12:04:12 urs Exp $
//

using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Protocol 
	{
		private IDictionary mMethods;
		private string mName;
		private string mFramework;
		private string[] mChildren;
		private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
		public Protocol(string _name, string _children, string _framework) 
		{
			mName = _name;
			mFramework = _framework;
			mChildren = _children.Split(new char[]{' ', ','});
			mMethods = new Hashtable();
		}
        
		public string[] Children 
		{
			get { return mChildren; } set { mChildren = value; }
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

//	$Log: Protocol.cs,v $
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//
//
