//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Class.cs,v 1.5 2004/09/20 20:18:23 gnorton Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CocoaSharp {
	public class Class : Type {
		static public Class GetClass(string name) {
		    if (name == null)
		        return null;
			Class ret = (Class)Classes[name];
			if (ret != null)
				return ret;
			foreach (Class cls in Classes.Values)
				if (cls.Name == name)
					return cls;
			return null;
		}

		public Class(string name, string nameSpace, Class parent, ICollection protocols, ICollection variables, ICollection instanceMethods, ICollection classMethod)
			: base(name, nameSpace, nameSpace + "." + name, null, OCType.id) {
			Classes[nameSpace + "." + name] = this;
			this.parent = parent;
			this.protocols = protocols != null ? protocols : new ArrayList();
			this.variables = variables != null ? variables : new ArrayList();
			this.instanceMethods = instanceMethods != null ? instanceMethods : new ArrayList();
			this.classMethods = classMethods != null ? classMethods : new ArrayList();
		}

		// -- Public Properties --
		public Class Parent { get { return parent; } }
		public ICollection Protocols { get { return protocols; } }
		public ICollection Variables { get { return variables; } }
		public ICollection InstanceMethods { get { return instanceMethods; } }
		public ICollection ClassMethods { get { return classMethods; } }
		public ICollection AllMethods {
			get {
			  	ArrayList ret = new ArrayList(InstanceMethods);
                ret.AddRange(ClassMethods);
				return ret;
			}
		}
		public string[] ProtocolNames {
			get {
				ArrayList ret = new ArrayList();
				foreach (Protocol p in Protocols)
					ret.Add(p.Name);
				return (string[])ret.ToArray(typeof(string));
			}
		}
		internal static IDictionary MethodsToDictionary(ICollection methods) {
            IDictionary ret = new Hashtable();
            foreach (Method m in methods)
                ret[m.Selector] = m;
            return ret;
		}

		// -- Members --
		private Class parent;
		private ICollection protocols;
		private ICollection variables;
		private ICollection instanceMethods;
		private ICollection classMethods;

		static private IDictionary Classes = new Hashtable();

		// -- Methods --
		public override void WriteCS(TextWriter _cs, Configuration config) {
			foreach (Method _toOutput in AllMethods)
				_toOutput.ClearCSAPIDone();

			// Load the overrides for this Interface
			Overrides _overrides = null;
			if(File.Exists(String.Format("{0}{1}{2}{1}{3}.override", config.OverridePath, Path.DirectorySeparatorChar, Namespace, Name))) {
				XmlSerializer _s = new XmlSerializer(typeof(Overrides));
				XmlTextReader _xmlreader = new XmlTextReader(String.Format("{0}{1}{2}{1}{3}.override", config.OverridePath, Path.DirectorySeparatorChar, Namespace, Name));
				_overrides = (Overrides)_s.Deserialize(_xmlreader);
				_xmlreader.Close();
			}
			_cs.WriteLine("using System;");
			_cs.WriteLine("using System.Collections;");
			_cs.WriteLine("using System.Runtime.InteropServices;");
			_cs.WriteLine("using Apple.Tools;");

			Framework frmwrk = config != null ? config.GetFramework(Namespace) : null;
			if (frmwrk != null && frmwrk.Dependencies != null)
				foreach (string dependency in frmwrk.Dependencies)
				    // TODO: remove hard coding of Apple.
					_cs.WriteLine("using Apple.{0};",dependency);
			_cs.WriteLine();
			_cs.WriteLine("namespace {0} {{", Namespace);

			_cs.Write("    public class {0}", Name);

			if(Parent != null)
				_cs.Write(" : {0}{1}", Parent.Namespace + "." + Parent.Name, string.Join(", I", ProtocolNames).Trim());
			if(Parent == null && Protocols.Count > 0)
				_cs.Write(" : {1}I{0}", string.Join(", I", ProtocolNames), (Name != "NSObject" ? "NSObject," : ""));
			_cs.WriteLine(" {");

#if !CAT
			_cs.WriteLine("        #region -- Internal Members --");
			_cs.WriteLine("        protected internal static IntPtr _{0}_classPtr;",Name);
			_cs.WriteLine("        protected internal static IntPtr {0}_classPtr {{ get {{ if (_{0}_classPtr == IntPtr.Zero) _{0}_classPtr = Apple.Foundation.Class.Get(\"{0}\"); return _{0}_classPtr; }} }}",Name);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();
#endif

			_cs.WriteLine("        #region -- Properties --");
			IDictionary instMethods = MethodsToDictionary(InstanceMethods);
			IDictionary clsMethods = MethodsToDictionary(ClassMethods);
			foreach (Method _toOutput in InstanceMethods)
				_toOutput.CSAPIMethod(false, Name, instMethods, true, _cs, _overrides);
			foreach (Method _toOutput in ClassMethods)
				_toOutput.CSAPIMethod(true, Name, clsMethods, true, _cs, _overrides);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Constructors --");
			if (Name != "NSObject" && Name != "NSProxy")
				_cs.WriteLine("        protected internal {0}(IntPtr raw,bool release) : base(raw,release) {{}}",Name);
			if (Name != "NSObject")
				_cs.WriteLine("        public {0}() : base() {{}}",Name);
			if (Name == "NSString")
				_cs.WriteLine("        public NSString(string str) : this(NSString__stringWithCString1(IntPtr.Zero,str),false) {}");
			_cs.WriteLine();
#if CAT
			if (mExtrasFor != null)
				_cs.WriteLine("        public {0}({1} o) : base(o.Raw,false) {{}}",Name,mExtrasFor);
#endif
			Class cur = this;
			IDictionary constructors = new Hashtable();
			constructors["IntPtr,bool"] = true;
			if (Name == "NSString")
				constructors["string"] = true;
			while (cur != null) {
				foreach (Method _toOutput in cur.InstanceMethods) {
					if (!_toOutput.IsConstructor)
						continue;
					string sig = _toOutput.CSConstructorSignature;
					if (!constructors.Contains(sig)) {
						_toOutput.CSConstructor(Name,_cs);
						constructors[sig] = true;
					}
				}
				cur = cur.Parent;
			}
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Public API --");
			foreach (Method _toOutput in InstanceMethods)
				_toOutput.CSAPIMethod(false, Name, instMethods, false, _cs, _overrides);
			foreach (Method _toOutput in ClassMethods)
				_toOutput.CSAPIMethod(true, Name, clsMethods, false, _cs, _overrides);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			ProcessAddin(_cs, config);

			_cs.WriteLine("    }");
			_cs.WriteLine("}");
			_cs.Close();
		}
	}
}

//
// $Log: Class.cs,v $
// Revision 1.5  2004/09/20 20:18:23  gnorton
// More refactoring; Foundation almost gens properly now.
//
// Revision 1.4  2004/09/20 16:42:52  gnorton
// More generator refactoring.  Start using the MachOGen for our classes.
//
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
