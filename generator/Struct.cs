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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Struct.cs,v 1.6 2004/06/23 15:29:29 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Struct : Element
	{
		public Struct(string _name, string _struct, string _framework) : base(_struct,_name,_framework) {}
        
		public override void WriteCS(TextWriter _cs)
		{
			_cs.WriteLine("using System;");
			_cs.WriteLine("namespace Apple.{0} {{",Framework);
			_cs.WriteLine("    public struct {0} {{",Name);
			_cs.WriteLine("/*" + mOriginal + "*/");
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}
}

//	$Log: Struct.cs,v $
//	Revision 1.6  2004/06/23 15:29:29  urs
//	Major refactor, allow inheriting parent constructors
//
//	Revision 1.5  2004/06/22 15:13:18  urs
//	New fixing
//	
//	Revision 1.4  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//	
//	Revision 1.3  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
