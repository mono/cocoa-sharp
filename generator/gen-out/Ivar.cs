//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Ivar.cs,v 1.1 2004/09/09 01:16:03 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Ivar {
		public Ivar(string name, Type type) { this.name = name; this.type = type; }

		// -- Public Properties --
		public string Name { get { return name; } }
		public Type Type { get { return type; } }

		// -- Members --
		private string name;
		private Type type;
	}
}

//
// $Log: Ivar.cs,v $
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
