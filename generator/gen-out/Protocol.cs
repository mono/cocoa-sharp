//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Protocol.cs,v 1.1 2004/09/09 01:16:03 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Protocol : Type {
		public Protocol(string name, string nameSpace, ICollection instanceMethods, ICollection classMethods)
			: base(name, nameSpace,null,OCType.id) {
			this.instanceMethods = instanceMethods;
			this.classMethods = classMethods;
		}

		// -- Public Properties --
		public ICollection InstanceMethods { get { return instanceMethods; } }
		public ICollection ClassMethods { get { return classMethods; } }

		// -- Members --
		private ICollection instanceMethods;
		private ICollection classMethods;
	}
}

//
// $Log: Protocol.cs,v $
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
