//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Enum.cs,v 1.2 2004/09/09 03:32:22 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Enum : Type {
		public Enum(string name, string nameSpace, ICollection items) 
			: base(name, nameSpace,typeof(Enum),OCType.@int) {
			this.items = items;
		}

		// -- Public Properties --
		public ICollection Items { get { return items; } }

		// -- Members --
		private ICollection items;
	}

	public class EnumItem {
		public EnumItem(string name, string value) { this.name = name; this.value = value; }

		// -- Public Properties --
		public string Name { get { return name; } }
		public string Value { get { return value; } }

		// -- Members --
		private string name;
		private string value;
	}
}

//
// $Log: Enum.cs,v $
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
