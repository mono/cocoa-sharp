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
        
		public IDictionary Interfaces;
		public IDictionary Protocols;
		public IDictionary Categories;

		public ObjCManagedExporter(string[] args) 
		{
			Interfaces = new Hashtable();
			Protocols = new Hashtable();
			Categories = new Hashtable();
            
			foreach (string arg in args) 
			{
				if(arg.IndexOf("-xml:") > -1)
					mXmlFile = arg.Substring(5);
			}
                
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
			Regex _protocolRegex = new Regex(@"^@protocol\s+(\w+)\s*(<([\w,\s]+)>)?(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _categoryRegex = new Regex(@"^@interface\s+(\w+)\s*\((\w+)\)(.+?)?@end$", RegexOptions.Multiline | RegexOptions.Singleline);
			Regex _enumRegex = new Regex(@"typedef\s+enum\s+{(.+?)}\s+(\w+)", RegexOptions.Multiline | RegexOptions.Singleline);
            
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
				Category _c = new Category(m.Groups[2].Value, m.Groups[1].Value);
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
				//Console.WriteLine("{0}", m.Groups[2].Value);
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
        
		private void OutputFramework(Framework _toprocess) 
		{
			IDictionaryEnumerator _protoenum = Protocols.GetEnumerator();
			while(_protoenum.MoveNext())
			{
				ArrayList _addedMethods = new ArrayList();
				Protocol p = (Protocol)_protoenum.Value;
		  		TextWriter _cs = new StreamWriter(File.Create(String.Format("src{0}Apple.{1}{0}{2}.cs.gen", Path.DirectorySeparatorChar, p.Framework, p.Name)));
				_cs.WriteLine("using System;");
				_cs.WriteLine("using System.Runtime.InteropServices;");
				if(!p.Framework.Equals("Foundation")) 
					_cs.WriteLine("using Apple.Foundation;");
				_cs.WriteLine("namespace Apple.{0}", _toprocess.Name);
				_cs.WriteLine("{");
 				_cs.Write("    public interface {0}", p.Name);
				_cs.WriteLine("    {");
				IDictionaryEnumerator _methodEnum = p.Methods.GetEnumerator();
				while(_methodEnum.MoveNext()) {
					Method _toOutput = (Method)_methodEnum.Value;
					String _methodSig = _toOutput.GlueMethodName;
					if(!_addedMethods.Contains((string)_methodSig)) 
					{ 
						_addedMethods.Add((string)_methodSig);
						_toOutput.CSInterfaceMethod(p.Name, _cs);
					}
				}
				_cs.WriteLine("    }");
				_cs.WriteLine("}");
				_cs.Close();
			}
			IDictionaryEnumerator _enum = Interfaces.GetEnumerator();
			while(_enum.MoveNext()) 
			{
				int totalMethods = 0;
				ArrayList interfaceMethods = new ArrayList();
				Interface i = (Interface)_enum.Value;
				if(!i.Framework.Equals(_toprocess.Name)) {
					continue;
				}
				Console.WriteLine("Interface: {0}:{1}", i.Name, i.Methods.Keys.Count);
				totalMethods += i.Methods.Keys.Count;
				// Add all the methods
				interfaceMethods.Add(i.Methods);

				// Process this interface and check to see if it implements any protocols
				foreach(string proto in i.Protocols) 
					if(proto.Length > 0) 
					{
						Console.Write("\t\tProtocol: <{0}>", proto);
						if(Protocols[proto] != null) 
						{
							Console.Write(":{0}", ((Protocol)Protocols[proto]).Methods.Keys.Count); 
							totalMethods += ((Protocol)Protocols[proto]).Methods.Keys.Count;
							interfaceMethods.Add(((Protocol)Protocols[proto]).Methods);
						}
						Console.WriteLine("");
					}

				IDictionaryEnumerator _categoryEnum = Categories.GetEnumerator();
				ArrayList _categoryImports = new ArrayList();
				while(_categoryEnum.MoveNext()) 
				{
					string _key = (string)_categoryEnum.Key;
					Category _cat = (Category)_categoryEnum.Value;
					if(_key.EndsWith("_" + i.Name)) 
					{
						Console.Write("\t\tCategory: ({0})", _key.Substring(0, _key.IndexOf("_")));
						Console.Write(":{0}", _cat.Methods.Keys.Count);
						totalMethods += _cat.Methods.Keys.Count;
						interfaceMethods.Add(_cat.Methods);
						foreach (string _imp in _cat.Imports)
							_categoryImports.Add(_imp);
						Console.WriteLine("");
					}
				}
				
				Console.WriteLine("\tTOTAL:{0}", totalMethods);	
				if(totalMethods > 0) 
				{
					string path = string.Format("src{0}{1}{0}", Path.DirectorySeparatorChar, _toprocess.Name);
					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);
					TextWriter _gs = new StreamWriter(File.Create(string.Format("src{0}{1}{0}{2}_glue.m", Path.DirectorySeparatorChar, _toprocess.Name, i.Name)));
					path = string.Format("src{0}Apple.{1}{0}", Path.DirectorySeparatorChar, _toprocess.Name);
					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);
					TextWriter _cs = new StreamWriter(File.Create(string.Format("src{0}Apple.{1}{0}{2}.cs.gen", Path.DirectorySeparatorChar, _toprocess.Name, i.Name)));
					_cs.WriteLine("using System;");
					_cs.WriteLine("using System.Runtime.InteropServices;");
					if(!i.Name.Equals("Foundation")) 
						_cs.WriteLine("using Apple.Foundation;");
					_cs.WriteLine("namespace Apple.{0}", _toprocess.Name);
					_cs.WriteLine("{");
 					_cs.Write("    public class {0}", i.Name);
					if(i.Child.Length > 0)
						_cs.Write(" : {0}{1}", i.Child, (String.Join(",", i.Protocols).Trim() != "" ? "," + String.Join(",", i.Protocols) : ""));
					if(i.Child.Length == 0 && i.Protocols.Length > 0)
						_cs.Write(" : {0}", String.Join(",", i.Protocols));
					_cs.WriteLine("    {");
					foreach(string import in i.Imports)
						_gs.WriteLine("#import <{0}>", import);
					foreach(string import in _categoryImports)
						_gs.WriteLine("#import <{0}>", import);

					// Create the glue
					IDictionary _addedMethods = new Hashtable();
					foreach (IDictionary _methods in interfaceMethods) 
					{
						foreach (Method _toOutput in _methods.Values)
						{
							if (_toOutput.IsUnsupported)
								continue;

							string _methodSig = _toOutput.GlueMethodName;
							if(!_addedMethods.Contains(_methodSig)) 
							{
								_addedMethods[_methodSig] = 1;
								_toOutput.ObjCMethod(i.Name, _gs);
								_toOutput.CSGlueMethod(i.Name, _toprocess.Name + "Glue", _cs);
								_toOutput.CSAPIMethod(i.Name, _cs);
							} 
							else 
								Console.WriteLine("\t\t\tWARNING: Method {0} is duplicated.", (string)_methodSig);
						}
					}
					_cs.WriteLine("    }");
					_cs.WriteLine("}");
					_cs.Close();
					_gs.Close();
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
	}
        
	public class Framework 
	{
		[XmlElement("name")]
		public string Name;
		[XmlElement("output")]
		public bool Output;
            
	}
}
