//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Struct.cs,v 1.3 2004/09/11 00:41:22 urs Exp $
//

using System;
using System.Collections;
using System.IO;

namespace CocoaSharp {
	public class Struct : Type {
		public Struct(string name,string nameSpace,ICollection items)
			: base(name,nameSpace,typeof(System.ValueType),OCType.structure) {
			this.items = items;
		}

		// -- Public Properties --
		public ICollection Items { get { return items; } }

		// -- Members --
		private ICollection items;

		// -- Methods --
		public override void WriteCS(TextWriter _cs, Configuration config) {
			_cs.WriteLine("using System;");
			_cs.WriteLine("namespace {0} {{",Namespace);
			_cs.WriteLine("    public struct {0} {{",Name);
			//_cs.WriteLine("/*" + mOriginal + "*/");
			ProcessAddin(_cs, config);
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}

	public class StructItem {
		public StructItem(TypeUsage type, string name) { this.type = type; this.name = name; }

		// -- Public Properties --
		public TypeUsage Type { get { return type; } }
		public string Name { get { return name; } }

		// -- Members --
		private TypeUsage type;
		private string name;
	}
}

//
// $Log: Struct.cs,v $
// Revision 1.3  2004/09/11 00:41:22  urs
// Move Output to gen-out
//
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
