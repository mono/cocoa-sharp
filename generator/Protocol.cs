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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Protocol.cs,v 1.13 2004/09/07 20:51:21 urs Exp $
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
			get { return "{1}{0}I{2}.cs"; }
		}

		public override void WriteCS(TextWriter _cs, Configuration config)
		{
			IDictionary allMethods = new Hashtable();
			foreach (Method method in Methods.Values) 
			{
				if (method.IsUnsupported)
					continue;

				string _methodSig = method.Selector;
				if(!allMethods.Contains(_methodSig)) 
					allMethods[_methodSig] = method;
				else 
					Console.WriteLine("\t\t\tWARNING: Method {0} is duplicated.", (string)_methodSig);
			}
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.ClearCSAPIDone();

			_cs.WriteLine("using System;");
			_cs.WriteLine("using System.Runtime.InteropServices;");
			Framework frmwrk = config != null ? config.GetFramework(Framework) : null;
			if (frmwrk != null && frmwrk.Dependencies != null)
				foreach (string dependency in frmwrk.Dependencies)
					_cs.WriteLine("using Apple.{0};",dependency);
			_cs.WriteLine();

			_cs.WriteLine("namespace Apple.{0} {{", Framework);
			_cs.WriteLine("    public interface I{0} {{", Name);

			_cs.WriteLine("        #region -- Properties --");
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.CSInterfaceMethod(Name,allMethods, true, _cs);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Public API --");
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.CSInterfaceMethod(Name,allMethods, false, _cs);
			_cs.WriteLine("        #endregion");

			string _realName = Name;
			Name = "I" + Name;
			ProcessAddin(_cs, config);
			Name = _realName;
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}
}

//	$Log: Protocol.cs,v $
//	Revision 1.13  2004/09/07 20:51:21  urs
//	Fix line endings
//
//	Revision 1.12  2004/06/29 03:32:58  urs
//	Cleanup mapping usage: only one bug left
//	
//	Revision 1.11  2004/06/28 22:07:43  gnorton
//	Updates/bugfixes
//	
//	Revision 1.10  2004/06/28 19:18:31  urs
//	Implement latest name bindings changes, and using objective-c reflection to see is a type is a OC class
//	
//	Revision 1.9  2004/06/25 02:49:14  gnorton
//	Sample 2 now runs.
//	
//	Revision 1.8  2004/06/24 18:56:53  gnorton
//	AppKit compiles
//	Foundation compiles
//	Output setMethod() for protocols not just the property so Interfaces are met.
//	Ignore static protocol methods (.NET doesn't support static in interfaces).
//	Resolve compiler errors.
//	
//	Revision 1.7  2004/06/23 17:14:20  gnorton
//	Custom addins supported on a per file basis.
//	
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
