//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: OutputElement.cs,v 1.1 2004/09/09 01:16:03 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public abstract class OutputElement {
		public OutputElement(string name, string nameSpace) { this.name = name; this.namespace_ = nameSpace; }

		// -- Public Properties --
		public string Namespace { get { return namespace_; } }
		public string Name { get { return name; } }

		// -- Members --
		private string namespace_;
		private string name;
	}
}

//
// $Log: OutputElement.cs,v $
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
