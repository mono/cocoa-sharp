//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Method.cs,v 1.2 2004/09/09 03:32:22 urs Exp $
//

using System;
using System.Collections;

namespace CocoaSharp {
	public class Method {
		public Method(string name,TypeUsage returnType, ICollection parameters) {
			this.name = name;
			this.returnType = returnType;
			this.parameters = parameters;
		}

		// -- Public Properties --
		public string Name { get { return name; } }
		public TypeUsage ReturnType { get { return returnType; } }
		public ICollection Parameters { get { return parameters; } }

		// -- Members --
		private string name;
		private TypeUsage returnType;
		private ICollection parameters;
	}

	public class ParameterInfo {
		public ParameterInfo(string name, TypeUsage type) { this.name = name; this.type = type; }

		// -- Public Properties --
		public TypeUsage Type { get { return type; } }
		public string Name { get { return name; } }

		// -- Members --
		private string name;
		private TypeUsage type;
	}
}

//
// $Log: Method.cs,v $
// Revision 1.2  2004/09/09 03:32:22  urs
// Convert methods from mach-o to out format
//
// Revision 1.1  2004/09/09 01:16:03  urs
// 1st draft of out module of 2nd generation generator
//
//
