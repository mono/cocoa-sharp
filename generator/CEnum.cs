//
//  CEnum.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/CEnum.cs,v 1.3 2004/06/22 13:38:59 urs Exp $
//

using System;
using System.IO;

namespace ObjCManagedExporter 
{

	public class CEnum : Element
	{
		public CEnum(string _name, string _enum, string _framework) : base(_enum,_name,_framework) {}
        
		public override void WriteCS(TextWriter _cs)
		{
			_cs.WriteLine("using System;");
			_cs.WriteLine("namespace Apple.{0} {{",Framework);
			_cs.WriteLine("    public enum {0} {{",Name);
			_cs.Write(mOriginal);
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}
}

//	$Log: CEnum.cs,v $
//	Revision 1.3  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//
//	Revision 1.2  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
