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
		private String mXmlFile = "generator.xml";
		private Configuration mConfig;
        
		public IDictionary Interfaces;
		public IDictionary Protocols;
		public IDictionary Categories;

		public ObjCManagedExporter(String[] args) 
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
		public ObjCManagedExporter() : this(new String[] {String.Empty}) {}
    
		private void ParseFile(FileSystemInfo _toParse, Framework f) 
		{
			ArrayList _imports = new ArrayList();
			_imports.Add(String.Format("{0}/{1}", f.Name, _toParse.Name));
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
			String _headerData = _fileReader.ReadToEnd();
            
			// Strip out the comments
			foreach(Match m in _commentRegex.Matches(_headerData))
				_headerData = _headerData.Replace(m.Value, "");
            
			foreach(Match m in _importRegex.Matches(_headerData)) 
				_imports.Add(m.Groups[1].Value);
			foreach (Match m in _protocolRegex.Matches(_headerData)) 
			{
            
				Protocol _p = new Protocol(m.Groups[1].Value, m.Groups[3].Value);
				_p.AddMethods(m.Groups[4].Value);
				Protocols.Add(_p.Name, _p);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _categoryRegex.Matches(_headerData)) 
			{
				Category _c = new Category(m.Groups[2].Value, m.Groups[1].Value);
				_c.AddMethods(m.Groups[3].Value);
				_c.Imports = (String[])_imports.ToArray(typeof(String));
				Categories.Add(String.Format("{0}_{1}", _c.Name, _c.Class), _c);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _interfaceRegex.Matches(_headerData)) 
			{
				Interface _i = new Interface(m.Groups[1].Value, m.Groups[3].Value, m.Groups[5].Value, f.Name);
				_i.AddMethods(m.Groups[6].Value);
				_i.Imports = (String[])_imports.ToArray(typeof(String));
				Interfaces.Add(_i.Name, _i);
				_headerData = _headerData.Replace(m.Value, "");
			}
            
			foreach (Match m in _enumRegex.Matches(_headerData)) 
			{
				// We found an enum
				//Console.WriteLine("{0}", m.Groups[2].Value);
			}
		}
        
		private String LocateFramework(Framework _tolocate) 
		{
			foreach (String sp in mConfig.SearchPaths) 
			{
				String rp = sp.Replace("%NAME%", _tolocate.Name);
				if(Directory.Exists(rp))
					return rp;
			}
    
			throw new Exception("Unable to locate framework " +  _tolocate.Name);
		}
        
		private void OutputFramework(Framework _toprocess) 
		{
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
				foreach(String proto in i.Protocols) 
				{
					if(!proto.Equals("")) 
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
					TextWriter _gs = new StreamWriter(File.Create(String.Format("src{0}{1}{0}{2}_glue.m", Path.DirectorySeparatorChar, _toprocess.Name, i.Name)));
					foreach(string import in i.Imports)
						_gs.WriteLine("#import <{0}>", import);
					foreach(string import in _categoryImports)
						_gs.WriteLine("#import <{0}>", import);
					// Create the glue
					ArrayList _addedMethods = new ArrayList();
					for(int j = 0; j < interfaceMethods.Count; j++) 
					{
						Hashtable _methods = (Hashtable)interfaceMethods[j];
						IDictionaryEnumerator _methodEnum = _methods.GetEnumerator();
						while(_methodEnum.MoveNext()) 
						{
							Method _toOutput = (Method)_methodEnum.Value;
							String _methodSig = _toOutput.GlueMethodName;
							if(!_addedMethods.Contains((string)_methodSig)) 
							{ 
								_addedMethods.Add((string)_methodSig);
								_toOutput.ObjCMethod(i.Name,_gs);
							} 
							else 
								Console.WriteLine("\t\t\tWARNING: Method {0} is duplicated.", (string)_methodSig);
						}
					}
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
		public String[] SearchPaths;
	}
        
	public class Framework 
	{
		[XmlElement("name")]
		public string Name;
		[XmlElement("output")]
		public bool Output;
            
	}
}
