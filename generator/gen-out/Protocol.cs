//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Protocol.cs,v 1.3 2004/09/11 00:41:22 urs Exp $
//

using System;
using System.Collections;
using System.IO;

namespace CocoaSharp {
	public class Protocol : Type {
		static public Protocol GetProtocol(string name) {
			Protocol ret = (Protocol)Protocols[name];
			if (ret != null)
				return ret;
			foreach (Protocol protocol in Protocols.Values)
				if (protocol.Name == name)
					return protocol;
			return null;
		}

		public Protocol(string name, string nameSpace, ICollection instanceMethods, ICollection classMethods)
			: base(name, nameSpace,null,OCType.id) {
			Protocols[nameSpace + "." + name] = this;
			this.instanceMethods = instanceMethods;
			this.classMethods = classMethods;
		}

		// -- Public Properties --
		public ICollection InstanceMethods { get { return instanceMethods; } }
		public ICollection ClassMethods { get { return classMethods; } }

		// -- Members --
		private ICollection instanceMethods;
		private ICollection classMethods;

		static private IDictionary Protocols = new Hashtable();

		// -- Methods --
		public override string FileNameFormat {
			get { return "{1}{0}I{2}.cs"; }
		}

		public override void WriteCS(TextWriter _cs, Configuration config) {
			IDictionary allMethods = new Hashtable();
			foreach (Method method in instanceMethods) {
				if (method.IsUnsupported)
					continue;

				string _methodSig = method.Selector;
				if(!allMethods.Contains(_methodSig)) 
					allMethods[_methodSig] = method;
				else 
					Console.WriteLine("\t\t\tWARNING: Method {0} is duplicated.", (string)_methodSig);
			}
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.ClearCSAPIDone();

			_cs.WriteLine("using System;");
			_cs.WriteLine("using System.Runtime.InteropServices;");
			Framework frmwrk = config != null ? config.GetFramework(Namespace) : null;
			if (frmwrk != null && frmwrk.Dependencies != null)
				foreach (string dependency in frmwrk.Dependencies)
					_cs.WriteLine("using {0};",dependency);
			_cs.WriteLine();

			_cs.WriteLine("namespace {0} {{", Namespace);
			_cs.WriteLine("    public interface I{0} {{", Name);

			_cs.WriteLine("        #region -- Properties --");
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.CSInterfaceMethod(false,Name,allMethods, true, _cs);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Public API --");
			foreach (Method _toOutput in allMethods.Values)
				_toOutput.CSInterfaceMethod(false,Name,allMethods, false, _cs);
			_cs.WriteLine("        #endregion");

			ProcessAddin("I" + Name,_cs, config);
			_cs.WriteLine("    }");
			_cs.WriteLine("}");
		}
	}
}

//
// $Log: Protocol.cs,v $
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
