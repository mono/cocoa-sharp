//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id$
//

using System;
using System.Collections;
using System.IO;

namespace CocoaSharp {
	public abstract class OutputElement {
		public OutputElement(string name, string nameSpace) { this.name = name; this.namespace_ = nameSpace; }

		// -- Public Properties --
		public string Namespace { get { return namespace_; } }
		public string Name { get { return name; } }

		// -- Members --
		private string namespace_;
		private string name;

		// -- Methods --
		public virtual string FileNameFormat {
			get { return "{1}{0}{2}.cs"; }
		}

		internal static TextWriter OpenFile(string pathFormat,string fileFormat,string nameSpace,string file) {
			string path = string.Format(pathFormat, Path.DirectorySeparatorChar, nameSpace);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
            string fileName = string.Format(fileFormat, Path.DirectorySeparatorChar, path, file);
            TextWriter ret = new StreamWriter(File.Create(fileName));
			ret.WriteLine("// Generated by Cocoa# Glue Generator @ " + DateTime.Now);
			ret.WriteLine();
			return ret;
		}

		public TextWriter OpenFile() {
            return OpenFile("src{0}{1}", this.FileNameFormat, Namespace, Name);
		}

		public void WriteFile(Configuration config) {
			//Console.WriteLine("Output " + Type.FullName(this.Name, this.Namespace) + ", " + this.GetType().Name);
			TextWriter _cs = OpenFile();
			WriteCS(_cs, config);
			_cs.Close();
		}

		public virtual void WriteCS(TextWriter _cs, Configuration config) {}

		protected void ProcessAddin(TextWriter _cs, Configuration config) {
			ProcessAddin(Name,_cs,config);
		}
		protected void ProcessAddin(string name, TextWriter _cs, Configuration config) {
			if(File.Exists(String.Format("{0}{1}{2}{1}{3}.addin", config.AddinPath, Path.DirectorySeparatorChar, Namespace.Replace("Apple.", "")/*FIXME*/, name))) {
				_cs.WriteLine("\t\t#region -- Generator Addins --");
				StreamReader _addinReader = new StreamReader(String.Format("{0}{1}{2}{1}{3}.addin", config.AddinPath, Path.DirectorySeparatorChar, Namespace.Replace("Apple.", "")/*FIXME*/, name));
				String _addinLine;
				while((_addinLine = _addinReader.ReadLine()) != null)
					_cs.WriteLine(_addinLine);

				_addinReader.Close();
				_cs.WriteLine("\t\t#endregion");
			}
		}
	}
}

//
// $Log: OutputElement.cs,v $
// Revision 1.6  2004/09/21 04:28:54  urs
// Shut up generator
// Add namespace to generator.xml
// Search for framework
// Fix path issues
// Fix static methods
//
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
