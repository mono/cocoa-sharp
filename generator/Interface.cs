//
//  Interface.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Interface.cs,v 1.12 2004/06/24 03:48:26 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ObjCManagedExporter 
{

	public class Interface : ElementWithMethods
	{
		private string mParent, mExtrasFor;
		private Interface mParentInterface;
		private string[] mProtos;
		private string[] mImports;
		private IDictionary mAllMethods;

		public Interface(string _name, string _parent, string _protos, string _framework) : base(_name,_framework) 
		{
			mParent = _parent;
			_protos = _protos.Replace(" ", "");		
			mProtos = _protos.Split(new char[]{','});
			mAllMethods = new Hashtable();
		}

		public string Parent 
		{
			get { return mParent; }
		}
		
		public void SetExtrasFor(Interface extrasFor)
		{
			mExtrasFor = extrasFor.Name;
		}

		public string ExtrasName
		{
			get { return mExtrasFor != null ? mExtrasFor : Name; }
		}

		public Interface ParentInterface
		{
			get { return mParentInterface; } set { mParentInterface = value; }
		}

		public string[] Protocols 
		{
			get { return mProtos; }
		}

		public string[] Imports 
		{
			get { return mImports; } set { mImports = value; }
		}

		public IDictionary AllMethods
		{
			get { return mAllMethods; }
		}

		public void AddAllMethods(ICollection methods)
		{
			foreach (Method method in methods)
			{
				if (method.IsUnsupported)
					continue;

				string _methodSig = method.GlueMethodName;
				if(!mAllMethods.Contains(_methodSig)) 
					mAllMethods[_methodSig] = method;
				else 
					Console.WriteLine("\t\t\tWARNING: Method {0} is duplicated.", (string)_methodSig);
			}
		}

		public override void WriteCS(TextWriter _cs, Configuration config)
		{
			// Load the overrides for this Interface
			Overrides _overrides = null;
			if(File.Exists(String.Format("{0}{1}{2}{1}{3}.override", config.OverridePath, Path.DirectorySeparatorChar, Framework, Name)))
                        {
				XmlSerializer _s = new XmlSerializer(typeof(Overrides));
				XmlTextReader _xmlreader = new XmlTextReader(String.Format("{0}{1}{2}{1}{3}.override", config.OverridePath, Path.DirectorySeparatorChar, Framework, Name));
                        	_overrides = (Overrides)_s.Deserialize(_xmlreader);
				_xmlreader.Close();
			}
			_cs.WriteLine("using System;");
			_cs.WriteLine("using System.Runtime.InteropServices;");

			Framework frmwrk = config != null ? config.GetFramework(Framework) : null;
			if (frmwrk != null && frmwrk.Dependencies != null)
				foreach (string dependency in frmwrk.Dependencies)
					_cs.WriteLine("using Apple.{0};",dependency);
			_cs.WriteLine();
			_cs.WriteLine("namespace Apple.{0} {{", Framework);

			_cs.Write("    public class {0}", Name);
			if(Parent.Length > 0)
				_cs.Write(" : {0}{1}", Parent, (string.Join(", I", Protocols).Trim() != "" ? ", I" + string.Join(", I", Protocols) : ""));
			if(Parent.Length == 0 && Protocols.Length > 0)
				_cs.Write(" : I{0}", string.Join(", I", Protocols));
			_cs.WriteLine(" {");

			if (mExtrasFor == null)
			{
				_cs.WriteLine("        #region -- Internal Members --");
				_cs.WriteLine("        protected internal static IntPtr _{0}_classPtr;",Name);
				_cs.WriteLine("        protected internal static IntPtr {0}_classPtr {{ get {{ if (_{0}_classPtr == IntPtr.Zero) _{0}_classPtr = Class.Get(\"{0}\"); return _{0}_classPtr; }} }}",Name);
				_cs.WriteLine("        #endregion");
				_cs.WriteLine();
			}

			_cs.WriteLine("        #region -- Properties --");
			foreach (Method _toOutput in AllMethods.Values)
				_toOutput.CSAPIMethod(ExtrasName,AllMethods, true, _cs, _overrides);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Constructors --");
			if (Name != "NSObject")
				_cs.WriteLine("        protected internal {0}(IntPtr raw,bool release) : base(raw,release) {{}}",Name);
			_cs.WriteLine();
			if (mExtrasFor != null)
				_cs.WriteLine("        public {0}({1} o) : base(o.Raw,false) {{}}",Name,mExtrasFor);
			_cs.WriteLine("        public {0}() : this(NSObject__alloc0({1}_classPtr),true) {{}}",Name,ExtrasName);
			Interface cur = this;
			IDictionary constructors = new Hashtable();
			while (cur != null)
			{
				foreach (Method _toOutput in cur.AllMethods.Values)
				{
					if (!_toOutput.IsConstructor)
						continue;
					string sig = _toOutput.CSConstructorSignature;
					if (!constructors.Contains(sig))
					{
						_toOutput.CSConstructor(Name,_cs);
						constructors[sig] = true;
					}
				}
				cur = cur.ParentInterface;
			}
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- Public API --");
			foreach (Method _toOutput in AllMethods.Values)
				_toOutput.CSAPIMethod(ExtrasName,AllMethods, false, _cs, _overrides);
			_cs.WriteLine("        #endregion");
			_cs.WriteLine();

			_cs.WriteLine("        #region -- PInvoke Glue API --");
			foreach (Method _toOutput in AllMethods.Values)
				_toOutput.CSGlueMethod(ExtrasName, Framework + "Glue", _cs);
			_cs.WriteLine("        #endregion");
			ProcessAddin(_cs, config);

			_cs.WriteLine("    }");
			_cs.WriteLine("}");
			_cs.Close();
		}
	}
}

//	$Log: Interface.cs,v $
//	Revision 1.12  2004/06/24 03:48:26  urs
//	minor fix for NSObject
//
//	Revision 1.11  2004/06/23 22:10:19  urs
//	Adding support for out of dependecy categories, generating a new class named $(class)$(categoryFramework)Extras with a the methods of all categories in same framework
//	
//	Revision 1.10  2004/06/23 18:31:51  urs
//	Add dependency for frameworks
//	
//	Revision 1.9  2004/06/23 17:55:41  urs
//	Make test compile with the lasted glue API name change
//	
//	Revision 1.8  2004/06/23 17:52:41  gnorton
//	Added ability to override what the generator outputs on a per-file/per-method basis
//	
//	Revision 1.7  2004/06/23 17:14:20  gnorton
//	Custom addins supported on a per file basis.
//	
//	Revision 1.6  2004/06/23 15:29:29  urs
//	Major refactor, allow inheriting parent constructors
//	
//	Revision 1.5  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//	
//	Revision 1.4  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//

