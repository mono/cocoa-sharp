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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Main.cs,v 1.25 2004/06/23 20:45:18 urs Exp $
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	class ObjCManagedExporter 
	{
		private string mXmlFile = "generator.xml";
		private Configuration mConfig;
		private string mOutputFlag = string.Empty;
        
		public IDictionary Interfaces;
		public IDictionary Protocols;
		public IDictionary Categories;
		public IDictionary Enums;
		public IDictionary Structs;

		public ObjCManagedExporter(string[] args) 
		{
			Interfaces = new Hashtable();
			Protocols = new Hashtable();
			Categories = new Hashtable();
			Enums = new Hashtable();
			Structs = new Hashtable();
            
			foreach (string arg in args) 
				if(arg.IndexOf("-xml:") >= 0)
					mXmlFile = arg.Substring("-xml:".Length);
				else if(arg.IndexOf("-out:") >= 0)
					mOutputFlag = arg.Substring("-out:".Length);
                
		}

		public ObjCManagedExporter() : this(new string[] {string.Empty}) {}
    
		private void ParseFile(FileSystemInfo _toParse, Framework f) 
		{
			ArrayList _imports = new ArrayList();
			_imports.Add(string.Format("{0}/{1}", f.Name, _toParse.Name));
			_imports.Add("Foundation/NSString.h");
			if(!_toParse.Name.EndsWith(".h")) 
				return;
                
			Regex _importRegex = new Regex(@"#import <(.+?)>");
			Regex _commentRegex = new Regex(@"(/\*([^\*]+)\*/$)|(//.+$)", RegexOptions.Multiline);
			Regex _interfaceRegex = new Regex(@"^@interface\s+(\w+)(\s*:\s*(\w+))?(\s*<([,\w\s]+)>\s*)?(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _protocolRegex = new Regex(@"^@protocol\s+(\w+)\s*(<([\w,\s]+)>)?[^;](.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _categoryRegex = new Regex(@"^@interface\s+(\w+)\s*\((\w+)\)(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _enumRegex = new Regex(@"typedef\s+enum\s+(.+?\s+)?{(.+?)}\s+(\w+)", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _structRegex = new Regex(@"typedef\s+struct\s+(.+?\s+)?{(.+?)}\s+(\w+)", RegexOptions.Multiline | RegexOptions.Singleline);
            
			TextReader _fileReader = new StreamReader(_toParse.FullName);
			string _headerData = _fileReader.ReadToEnd();
            
			// Strip out the comments
			foreach(Match m in _commentRegex.Matches(_headerData))
				_headerData = _headerData.Replace(m.Value, "");
            
			foreach(Match m in _importRegex.Matches(_headerData)) 
				_imports.Add(m.Groups[1].Value);
			foreach (Match m in _protocolRegex.Matches(_headerData)) 
			{
				Protocol _p = new Protocol(m.Groups[1].Value, m.Groups[3].Value, f.Name);
				_p.AddMethods(m.Groups[4].Value);
				Protocols.Add(_p.Name, _p);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _categoryRegex.Matches(_headerData)) 
			{
				Category _c = new Category(m.Groups[2].Value, m.Groups[1].Value, f.Name);
				_c.AddMethods(m.Groups[3].Value);
				_c.Imports = (string[])_imports.ToArray(typeof(string));
				Categories.Add(string.Format("{0}_{1}", _c.Name, _c.Class), _c);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _interfaceRegex.Matches(_headerData)) 
			{
				Interface _i = new Interface(m.Groups[1].Value, m.Groups[3].Value, m.Groups[5].Value, f.Name);
				_i.AddMethods(m.Groups[6].Value);
				_i.Imports = (string[])_imports.ToArray(typeof(string));
				Interfaces.Add(_i.Name, _i);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _enumRegex.Matches(_headerData)) 
			{
				// We found an enum
				CEnum _s = new CEnum(m.Groups[3].Value, m.Groups[2].Value, f.Name);
				Enums.Add(m.Groups[3].Value, _s);
			}
			foreach (Match m in _structRegex.Matches(_headerData)) 
			{
				// We found an struct
				Struct _s = new Struct(m.Groups[3].Value, m.Groups[2].Value, f.Name);
				Structs.Add(m.Groups[3].Value, _s);
			}
		}
        
		private string LocateFramework(Framework _tolocate) 
		{
			foreach (string sp in mConfig.SearchPaths) 
			{
				string rp = sp.Replace("%NAME%", _tolocate.Name);
				if(Directory.Exists(rp))
					return rp;
			}
    
			throw new Exception("Unable to locate framework " +  _tolocate.Name);
		}

		private bool OutputOC
		{
			get { return mOutputFlag == string.Empty || mOutputFlag == "OC"; }
		}

		private bool OutputCS
		{
			get { return mOutputFlag == string.Empty || mOutputFlag == "CS"; }
		}

		private void OutputFramework(Framework _toprocess) 
		{
			if (OutputCS)
			{
				foreach(CEnum e in Enums.Values)
					if(e.Framework == _toprocess.Name)
						e.WriteFile(mConfig);

				foreach (Struct s in Structs.Values)
					if(s.Framework == _toprocess.Name) 
						s.WriteFile(mConfig);

				foreach (Protocol p in Protocols.Values)
					if(p.Framework == _toprocess.Name) 
						p.WriteFile(mConfig);
			}

			foreach (Interface i in Interfaces.Values) 
			{
				if(i.Framework != _toprocess.Name)
					continue;

				if(i.AllMethods.Count > 0) 
				{
					TextWriter _gs = null;

					if (OutputOC)
					{
						_gs = Element.OpenFile("src{0}{1}.Glue","{1}{0}{2}_glue.m", _toprocess.Name, i.Name);

						foreach(string import in i.Imports)
							_gs.WriteLine("#import <{0}>", import);

						_gs.WriteLine();

						foreach (Method _toOutput in i.AllMethods.Values)
							_toOutput.ObjCMethod(i.Name, _gs);
						_gs.Close();
					}

					if (OutputCS)
						i.WriteFile(mConfig);
				}
			}
		}

		private void ProcessFramework(Framework _toprocess) 
		{
			Console.Write("Processing framework ({0}): ", _toprocess.Name);
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
		
		private void BuildInterfaces()
		{ 
			foreach (Interface i in Interfaces.Values) 
			{
				Framework frmwrk = mConfig.GetFramework(i.Framework);
				ArrayList interfaceMethods = new ArrayList();
				Console.WriteLine("Interface: {0}({1}):{2}", i.Name, i.Framework, i.Methods.Keys.Count);
				if (i.Parent.Length > 0)
					i.ParentInterface = (Interface)Interfaces[i.Parent];
				i.AddAllMethods(i.Methods.Values);

				// Process this interface and check to see if it implements any protocols
				foreach(string proto in i.Protocols) 
					if(proto.Length > 0) 
					{
						Console.Write("\t\tProtocol: <{0}>", proto);
						Protocol p = (Protocol)Protocols[proto];
						if(p != null) 
						{
							Console.Write(":{0}", p.Methods.Keys.Count); 
							i.AddAllMethods(p.Methods.Values);
						}
						else
							Console.Write(":missing"); 
						Console.WriteLine();
					}

				ArrayList _categoryImports = new ArrayList(i.Imports);
				foreach (DictionaryEntry e in Categories) 
				{
					string _key = (string)e.Key;
					if(_key.EndsWith("_" + i.Name)) 
					{
						Category _cat = (Category)e.Value;
						if (!frmwrk.ContainsDependency(_cat.Framework))
						{
							Console.WriteLine("\t\tCategory: ({0}) ignored (no dependency to {1})", _key.Substring(0, _key.IndexOf("_")), _cat.Framework);
							continue;
						}
						Console.Write("\t\tCategory: ({0})", _key.Substring(0, _key.IndexOf("_")));
						Console.Write(":{0}", _cat.Methods.Keys.Count);
						i.AddAllMethods(_cat.Methods.Values);
						foreach (string _imp in _cat.Imports)
							_categoryImports.Add(_imp);
						Console.WriteLine();
					}
				}
				i.Imports = (string[])_categoryImports.ToArray(typeof(string));

				Console.WriteLine("\tTOTAL:{0}", i.AllMethods.Count);
			}
		}

		private bool LoadConfiguration() 
		{
			// Ensure the file exists
			if(!File.Exists(mXmlFile)) 
			{
				Console.WriteLine("ERROR: Generator cannot run; XML File ({0}) does not exist", mXmlFile);
				return false;
			}

			// Deserialize our frameworks file
			XmlTextReader _xmlreader = new XmlTextReader(mXmlFile);
			XmlSerializer _serializer = new XmlSerializer(typeof(Configuration));
			mConfig = (Configuration)_serializer.Deserialize(_xmlreader);
			return true;
		}
        
		public void Run() 
		{
			if(!LoadConfiguration())
				return;
                    
			foreach(Framework f in mConfig.Frameworks)
				ProcessFramework(f);

			BuildInterfaces();

			foreach(Framework f in mConfig.Frameworks)
				OutputFramework(f);
		}   
            
		static void Main(string[] args) 
		{
			ObjCManagedExporter exporter = new ObjCManagedExporter(args);
			exporter.Run();
		}
	}

	[XmlRoot("generator")]
	public class Configuration 
	{
		[XmlElement("framework")]
		public Framework[] Frameworks;
		[XmlElement("searchpath")]
		public string[] SearchPaths;
		[XmlElement("addinpath")]
		public string AddinPath;
		[XmlElement("overridepath")]
		public string OverridePath;
		
		public Framework GetFramework(string which)
		{
			foreach (Framework frmwrk in Frameworks)
				if (frmwrk.Name == which)
					return frmwrk;
			return null;
		}
	}
        
	public class Framework 
	{
		[XmlElement("name")]
		public string Name;
		[XmlElement("output")]
		public bool Output;
		[XmlElement("dependency")]
		public string[] Dependencies;
		
		public bool ContainsDependency(string dep)
		{
			if (dep == Name)
				return true;
			if (Dependencies == null)
				return false;
				
			foreach (string dependency in Dependencies)
				if (dependency == dep)
					return true;
			return false;
		}
	}

	[XmlRoot("overrides")]
	public class Overrides
	{
		[XmlElement("method")] public MethodOverride[] Methods;
	}

	public class MethodOverride {
		[XmlAttribute("instance")] public bool InstanceMethod;
		[XmlAttribute("sel")] public String Selector;
		[XmlText] public String Method;
	}
}

//	$Log: Main.cs,v $
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
