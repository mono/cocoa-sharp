//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: MachOProtocol.cs,v 1.1 2004/09/09 01:18:47 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	internal class MachOProtocol {
		internal string Name;
		internal ArrayList instanceMethods = new ArrayList();
		internal ArrayList classMethods = new ArrayList();
		private IDictionary protocols = new Hashtable();

		internal MachOProtocol(string name) { Name = name; }

		internal void AddProtocolsFromArray(IList protocols) {
			foreach (MachOProtocol p in protocols)
				AddProtocol(p);
		}

		internal void AddProtocol(MachOProtocol p) {
			if (protocols.Contains(p.Name))
				protocols[p.Name] = p;
		}
	}

	internal struct objc_protocol_list {
		internal uint next;
		internal uint count;
	}

	internal struct objc_protocol {
		internal uint isa;
		internal uint protocol_name;
		internal uint protocol_list;
		internal uint instance_methods;
		internal uint class_methods;
	};

	internal struct objc_protocol_method_list {
		internal uint method_count;
		// Followed by methods
	};

	internal struct objc_protocol_method {
		internal uint name;
		internal uint types;
	};
}

