//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Struct.cs,v 1.2 2004/09/09 03:32:22 urs Exp $
//

using System;
using System.Collections;

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
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
