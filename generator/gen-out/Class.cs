//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Class.cs,v 1.2 2004/09/09 03:32:22 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Class : Type {
		static public Class GetClass(string name) {
			Class ret = (Class)Classes[name];
			if (ret != null)
				return ret;
			foreach (Class cls in Classes.Values)
				if (cls.Name == name)
					return cls;
			return null;
		}

		public Class(string name, string nameSpace, Class parent, ICollection protocols, ICollection variables, ICollection instanceMethods, ICollection classMethod)
			: base(name, nameSpace,null,OCType.id) {
			Classes[nameSpace + "." + name] = this;
			this.parent = parent;
			this.protocols = protocols;
			this.variables = variables;
			this.instanceMethods = instanceMethods;
			this.classMethods = classMethods;
		}

		// -- Public Properties --
		public Class Parent { get { return parent; } }
		public ICollection Protocols { get { return protocols; } }
		public ICollection Variables { get { return variables; } }
		public ICollection InstanceMethods { get { return instanceMethods; } }
		public ICollection ClassMethods { get { return classMethods; } }

		// -- Members --
		private Class parent;
		private ICollection protocols;
		private ICollection variables;
		private ICollection instanceMethods;
		private ICollection classMethods;

		static private IDictionary Classes = new Hashtable();
	}
}

//
// $Log: Class.cs,v $
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
