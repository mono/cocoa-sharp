//
//  Main.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/header-gen/Main.cs,v 1.3 2004/09/18 17:30:17 urs Exp $
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text.RegularExpressions;

namespace CocoaSharp {

	public class ObjCManagedExporter {
		private string mOutputFlag = string.Empty;
		private Configuration mConfig;

		public IDictionary Interfaces;
		public IDictionary Protocols;
		public IDictionary Categories;
		public IDictionary Enums;
		public IDictionary Structs;

		public ObjCManagedExporter(string[] args) {
			Interfaces = new Hashtable();
			Protocols = new Hashtable();
			Categories = new Hashtable();
			Enums = new Hashtable();
			Structs = new Hashtable();

			try {
				ObjCClassInspector.IsObjCClass("NSString");
			}
			catch {}

			foreach (string arg in args) 
#if XML_FILE
				if(arg.IndexOf("-xml:") >= 0)
					mXmlFile = arg.Substring("-xml:".Length);
				else
#endif
				if(arg.IndexOf("-out:") >= 0)
					mOutputFlag = arg.Substring("-out:".Length);
		}

		public ObjCManagedExporter() : this(new string[] {string.Empty}) {}
		public ObjCManagedExporter(Configuration config) : this(new string[] {string.Empty})
		{
			this.mConfig = config;
		}

		private void ParseFile(FileSystemInfo _toParse, Framework f) {
			IDictionary _imports = new Hashtable();
			_imports[string.Format("{0}/{1}", f.Name, _toParse.Name)] = true;
			_imports["Foundation/NSString.h"] = true;
			if(!_toParse.Name.EndsWith(".h")) 
				return;

			Regex _importRegex = new Regex(@"#import <(.+?)>");
			Regex _commentRegex = new Regex(@"(/\*([^\*]+)\*/$)|(//.+$)", RegexOptions.Multiline);
			Regex _interfaceRegex = new Regex(@"@interface\s+(\w+)(\s*:\s*(\w+))?(\s*<([,\w\s]+)>\s*)?(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _protocolRegex = new Regex(@"@protocol\s+(\w+)\s*(<([\w,\s]+)>)?[^;](.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _categoryRegex = new Regex(@"@interface\s+(\w+)\s*\((\w+)\)(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _enumRegex = new Regex(@"typedef\s+enum\s+(\w+?\s+)?{(\w+?^\s*\w+\s*=\s*\d+,.+?|.+?^\s*\w+,.+?)}\s+(\w+)", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _structRegex = new Regex(@"typedef\s+struct\s+(\w+?\s+)?{(.+?)}\s+(\w+)", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _classForwardDeclRegex = new Regex(@"@class\s+\w+;");
			Regex _protocolForwardDeclRegex = new Regex(@"@protocol\s+\w+;");

			TextReader _fileReader = new StreamReader(_toParse.FullName);
			string _headerData = _fileReader.ReadToEnd();

			// Strip out the comments
			foreach(Match m in _commentRegex.Matches(_headerData))
				_headerData = _headerData.Replace(m.Value, "");

			foreach(Match m in _classForwardDeclRegex.Matches(_headerData)) {
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": class: " + m.Value);
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach(Match m in _protocolForwardDeclRegex.Matches(_headerData)) {
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": protocol: " + m.Value);
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach(Match m in _importRegex.Matches(_headerData)) {
				_imports[m.Groups[1].Value] = true;
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": import: " + m.Groups[1].Value);
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach (Match m in _protocolRegex.Matches(_headerData)) {
				HeaderProtocol _p = new HeaderProtocol(m.Groups[1].Value, m.Groups[3].Value, f.Name);
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": @protocol: " + _p.Name + ", " + f.Name + ", value=" + m.Value.Replace("\n","\\n"));
				_p.AddMethods(m.Groups[4].Value);
				if (Protocols.Contains(_p.Name))
					Console.WriteLine("ERROR: " + f.Name + "/" + _toParse.Name + ": @protocol: " + _p.Name + " is already defined.");
				else
					Protocols[_p.Name] = _p;
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach (Match m in _categoryRegex.Matches(_headerData)) {
				HeaderCategory _c = new HeaderCategory(m.Groups[2].Value, m.Groups[1].Value, f.Name);
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": @category: " + _c.Name + ", " + f.Name);
				_c.AddMethods(m.Groups[3].Value);
				_c.Imports = (string[])new ArrayList(_imports.Keys).ToArray(typeof(string));
				if (Categories.Contains(string.Format("{0}_{1}", _c.Name, _c.Class)))
					Console.WriteLine("ERROR: " + f.Name + "/" + _toParse.Name + ": @category: " + _c.Name + " is already defined.");
				else
					Categories[string.Format("{0}_{1}", _c.Name, _c.Class)] = _c;
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach (Match m in _interfaceRegex.Matches(_headerData)) {
				HeaderInterface _i = new HeaderInterface(m.Groups[1].Value, m.Groups[3].Value, m.Groups[5].Value, f.Name);
				//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": @interface: " + _i.Name + ", " + f.Name);
				_i.AddMethods(m.Groups[6].Value);
				_i.Imports = (string[])new ArrayList(_imports.Keys).ToArray(typeof(string));
				if (Interfaces.Contains(_i.Name))
					Console.WriteLine("ERROR: " + f.Name + "/" + _toParse.Name + ": @interface: " + _i.Name + " is already defined.");
				else
					Interfaces[_i.Name] = _i;
				_headerData = _headerData.Replace(m.Value, "");
			}

			foreach (Match m in _enumRegex.Matches(_headerData)) {
				// We found an enum
				HeaderEnum _s = new HeaderEnum(m.Groups[3].Value, m.Groups[2].Value, f.Name);
				if (Enums.Contains(_s.Name))
					Console.WriteLine("ERROR: " + f.Name + "/" + _toParse.Name + ": @enum: " + _s.Name + " is already defined.");
				else
					Enums[m.Groups[3].Value] = _s;
				_headerData = _headerData.Replace(m.Value, "");
			}
			foreach (Match m in _structRegex.Matches(_headerData)) {
				// We found an struct
				HeaderStruct _s = new HeaderStruct(m.Groups[3].Value, m.Groups[2].Value, f.Name);
				if (Interfaces.Contains(_s.Name))
					Console.WriteLine("ERROR: " + f.Name + "/" + _toParse.Name + ": @struct: " + _s.Name + " is already defined.");
				else
					Structs[m.Groups[3].Value] = _s;
				_headerData = _headerData.Replace(m.Value, "");
			}
			//Console.WriteLine("DEBUG: " + f.Name + "/" + _toParse.Name + ": leftover: " + _headerData.Replace("\n","\\n"));
		}

		private string LocateFramework(Framework _tolocate) {
			foreach (string sp in mConfig.SearchPaths) {
				string rp = sp.Replace("%NAME%", _tolocate.Name);
				if(Directory.Exists(rp))
					return rp;
			}

			throw new Exception("Unable to locate framework " +  _tolocate.Name);
		}

		private bool OutputOC {
			get { return mOutputFlag == string.Empty || mOutputFlag == "OC"; }
		}

		private bool OutputCS {
			get { return mOutputFlag == string.Empty || mOutputFlag == "CS"; }
		}

		public void ProcessFramework(Framework _toprocess) {
			try {
				ObjCClassInspector.AddBundle(_toprocess.Name);
			} catch {}
			Console.Write("Processing framework ({0}): ", _toprocess.Name);
			try {
				DirectoryInfo _frameworkDirectory = new DirectoryInfo(LocateFramework(_toprocess));
				FileSystemInfo[] _infos = _frameworkDirectory.GetFileSystemInfos();
				Console.Write("00%");
				for(int i = 0; i < _infos.Length; i++) {
					float length = _infos.Length;
					float complete = (i/length)*100;
					ParseFile(_infos[i], _toprocess);
					Console.Write("\b\b\b{0:00}%", complete);
				}
				Console.WriteLine("\b\b\b100%");
			}
			catch (Exception e) {
				Console.WriteLine("\b\b\bfailed!");
				Console.WriteLine("Exception: " + e.Message);
			}
		}

		public void BuildInterfaces() {
			IDictionary extras = new Hashtable();
			foreach (HeaderInterface i in Interfaces.Values) {
				Framework frmwrk = mConfig.GetFramework(i.Framework);
				ArrayList interfaceMethods = new ArrayList();
				//Console.WriteLine("Interface: {0}({1}):{2}", i.Name, i.Framework, i.Methods.Keys.Count);
				if (i.Parent.Length > 0)
					i.ParentInterface = (HeaderInterface)Interfaces[i.Parent];
				i.AddAllMethods(i.Methods.Values,false);

				// Process this interface and check to see if it implements any protocols
				foreach(string proto in i.Protocols) 
					if(proto.Length > 0) {
						//Console.Write("\t\tProtocol: <{0}>", proto);
						HeaderProtocol p = (HeaderProtocol)Protocols[proto];
						if(p != null) {
							//	Console.Write(":{0}", p.Methods.Keys.Count); 
							i.AddAllMethods(p.Methods.Values,true);
						}
						//else
						//	Console.Write(":missing"); 
						//Console.WriteLine();
					}

				IDictionary _categoryImports = new Hashtable();
				foreach (string import in i.Imports)
					_categoryImports[import] = true;
				foreach (DictionaryEntry e in Categories) {
					string _key = (string)e.Key;
					if(_key.EndsWith("_" + i.Name)) {
						HeaderCategory _cat = (HeaderCategory)e.Value;
						if (!frmwrk.ContainsDependency(_cat.Framework)) {
							HeaderInterface catInter = GetCategoryInterface(extras,i,_cat);
							
							//Console.WriteLine("\t\tCategory: ({0}) added to {1} (no dependency to {2})", 
							//	_key.Substring(0, _key.IndexOf("_")), catInter.Name, _cat.Framework);
							catInter.AddAllMethods(_cat.Methods.Values,false);
							continue;
						}
						//Console.Write("\t\tCategory: ({0})", _key.Substring(0, _key.IndexOf("_")));
						//Console.Write(":{0}", _cat.Methods.Keys.Count);
						i.AddAllMethods(_cat.Methods.Values,false);
						foreach (string import in _cat.Imports)
							_categoryImports[import] = true;
						//Console.WriteLine();
					}
				}
				i.Imports = (string[])new ArrayList(_categoryImports.Keys).ToArray(typeof(string));

				//Console.WriteLine("\tTOTAL:{0}", i.AllMethods.Count);
			}
			foreach (DictionaryEntry e in extras)
				Interfaces[e.Key] = e.Value;
		}
		
		private HeaderInterface GetCategoryInterface(IDictionary extras,HeaderInterface extrasFor,HeaderCategory _cat) {
			string extraName = extrasFor.Name + _cat.Framework + "Extras";
			HeaderInterface ret = (HeaderInterface)extras[extraName];
			if (ret == null) {
				ret = new HeaderInterface(extraName,extrasFor.Name,string.Empty,_cat.Framework);
				ret.Imports = extrasFor.Imports;
				ret.SetExtrasFor(extrasFor);
				extras[extraName] = ret;
			}
			ArrayList imports = new ArrayList(ret.Imports);
			imports.AddRange(_cat.Imports);
			ret.Imports = (string[])imports.ToArray(typeof(string));
			return ret;
		}

#if XML_FILE
		private bool LoadConfiguration() {
			// Ensure the file exists
			if(!File.Exists(mXmlFile)) {
				Console.WriteLine("ERROR: Generator cannot run; XML File ({0}) does not exist", mXmlFile);
				return false;
			}

			// Deserialize our frameworks file
			XmlTextReader _xmlreader = new XmlTextReader(mXmlFile);
			XmlSerializer _serializer = new XmlSerializer(typeof(Configuration));
			mConfig = (Configuration)_serializer.Deserialize(_xmlreader);
			return true;
		}
#endif

		public void Run() {
#if XML_FILE
			if(!LoadConfiguration())
				return;
#endif

			foreach(Framework f in mConfig.Frameworks)
				ProcessFramework(f);

			BuildInterfaces();

			WriteCS csWriter = new WriteCS(mConfig);
			csWriter.AddRange(HeaderEnum.ToOutput(this.Enums));
			csWriter.AddRange(HeaderStruct.ToOutput(this.Structs));
			csWriter.AddRange(HeaderProtocol.ToOutput(this.Protocols));
			csWriter.AddRange(HeaderInterface.ToOutput(this.Interfaces));

			foreach(Framework f in mConfig.Frameworks)
				csWriter.OutputNamespace(f.Name);

			Console.WriteLine("Code generation successful");
#if false
			Console.WriteLine("Updating mapping.");
			HeaderMethod.SaveMapping();
#endif
		}

		static void Main(string[] args) {
			ObjCManagedExporter exporter = new ObjCManagedExporter(args);
			exporter.Run();
		}
	}
}

//	$Log: Main.cs,v $
//	Revision 1.3  2004/09/18 17:30:17  urs
//	Move CS output gen into gen-out
//
//	Revision 1.2  2004/09/11 00:41:22  urs
//	Move Output to gen-out
//	
//	Revision 1.1  2004/09/09 13:18:53  urs
//	Check header generator back in.
//	
//	Revision 1.40  2004/09/07 20:51:21  urs
//	Fix line endings
//	
//	Revision 1.39  2004/07/01 12:41:33  urs
//	- Better verbose support, individual verbose ignore per selector and per interface
//	- Minor improvements with monodoc
//	
//	Revision 1.38  2004/06/30 19:29:22  urs
//	Cleanup
//	
//	Revision 1.37  2004/06/29 03:32:58  urs
//	Cleanup mapping usage: only one bug left
//	
//	Revision 1.36  2004/06/28 22:07:43  gnorton
//	Updates/bugfixes
//	
//	Revision 1.35  2004/06/28 21:31:22  gnorton
//	Initial mapping support in the gen.
//	
//	Revision 1.34  2004/06/28 19:18:31  urs
//	Implement latest name bindings changes, and using objective-c reflection to see is a type is a OC class
//	
//	Revision 1.33  2004/06/27 20:27:48  gnorton
//	Turn conditional output support back on
//	
//	Revision 1.32  2004/06/25 17:39:10  urs
//	Handle char* as argument and return value
//	
//	Revision 1.31  2004/06/25 02:49:14  gnorton
//	Sample 2 now runs.
//	
//	Revision 1.30  2004/06/24 14:15:42  gnorton
//	Some appkit fixes
//	
//	Revision 1.29  2004/06/24 06:29:36  gnorton
//	Make foundation compile.
//	
//	Revision 1.28  2004/06/24 04:36:17  gnorton
//	Updates to fix build errors; not many left now
//	
//	Revision 1.27  2004/06/24 01:08:43  gnorton
//	Core file support so we can add files ot the build that aren't generated
//	
//	Revision 1.26  2004/06/23 22:10:19  urs
//	Adding support for out of dependecy categories, generating a new class named $(class)$(categoryFramework)Extras with a the methods of all categories in same framework
//	
//	Revision 1.25  2004/06/23 20:45:18  urs
//	Only add category of dependent frameworks, this might be changed in the future, but would require a new class
//	
//	Revision 1.24  2004/06/23 18:31:51  urs
//	Add dependency for frameworks
//	
//	Revision 1.23  2004/06/23 18:12:13  gnorton
//	Add WebKit to the generator
//	Change the output directories to be Framework.Glue
//	
//	Revision 1.22  2004/06/23 17:52:41  gnorton
//	Added ability to override what the generator outputs on a per-file/per-method basis
//	
//	Revision 1.21  2004/06/23 17:14:20  gnorton
//	Custom addins supported on a per file basis.
//	
//	Revision 1.20  2004/06/23 15:29:29  urs
//	Major refactor, allow inheriting parent constructors
//	
//	Revision 1.19  2004/06/22 19:54:21  urs
//	Add property support
//	
//	Revision 1.18  2004/06/22 15:13:18  urs
//	New fixing
//	
//	Revision 1.17  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//	
//	Revision 1.16  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
