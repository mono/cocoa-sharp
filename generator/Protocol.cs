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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Protocol.cs,v 1.6 2004/06/23 15:29:29 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Protocol : ElementWithMethods
	{
		private string[] mChildren;
        
		public Protocol(string _name, string _children, string _framework) : base(_name,_framework)
		{
			mChildren = _children.Split(new char[]{' ', ','});
		}
        
		public string[] Children 
		{
			get { return mChildren; } set { mChildren = value; }
		}

		public override string FileNameFormat
		{
			get { return "{1}{0}I{2}.gen.cs"; }
		}

		public override void WriteCS(TextWriter _cs)
		{
			IDictionary _addedMethods = new Hashtable();
			_cs.WriteLine("using System;");
			_cs.WriteLine("using System.Runtime.InteropServices;");

			if(Framework != "Foundation") 
				_cs.WriteLine("using Apple.Foundation;");

			_cs.WriteLine("namespace Apple.{0} {{", Framework);
			_cs.WriteLine("    public interface I{0} {{", Name);

			foreach (Method _toOutput in Methods.Values) 
			{
				if (_toOutput.IsUnsupported)
					continue;

				string _methodSig = _toOutput.GlueMethodName;
				if(!_addedMethods.Contains(_methodSig)) 
				{
					_addedMethods[_methodSig] = true;
					_toOutput.CSInterfaceMethod(Name, _cs);
				}
			}
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}
}

//	$Log: Protocol.cs,v $
//	Revision 1.6  2004/06/23 15:29:29  urs
//	Major refactor, allow inheriting parent constructors
//
//	Revision 1.5  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//	
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
