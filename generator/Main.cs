using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    class ObjCManagedExporter {
        private String mXmlFile = "generator.xml";
        private Configuration mConfig;
        
        public IDictionary Interfaces;
        public IDictionary Protocols;
        public IDictionary Categories;

        public ObjCManagedExporter(String[] args) {
            Interfaces = new Hashtable();
            Protocols = new Hashtable();
            Categories = new Hashtable();
            
            foreach (string arg in args) {
                if(arg.IndexOf("-xml:") > -1)
                    mXmlFile = arg.Substring(4);
            }
                
        }
        public ObjCManagedExporter() : this(new String[] {String.Empty}) {}
    
        private void ParseFile(FileSystemInfo _toParse, Framework f) {
            if(!_toParse.Name.EndsWith(".h")) 
                return;
                
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
            
            foreach (Match m in _protocolRegex.Matches(_headerData)) {
            
                Protocol _p = new Protocol(m.Groups[1].Value, m.Groups[3].Value);
                _p.AddMethods(m.Groups[4].Value);
                Protocols.Add(_p.Name, _p);
                _headerData = _headerData.Replace(m.Value, "");
            }
            
            foreach (Match m in _categoryRegex.Matches(_headerData)) {
                Category _c = new Category(m.Groups[2].Value, m.Groups[1].Value);
                _c.AddMethods(m.Groups[3].Value);
                Categories.Add(String.Format("{0}_{1}", _c.Name, _c.Class), _c);
                _headerData = _headerData.Replace(m.Value, "");
            }
            
            foreach (Match m in _interfaceRegex.Matches(_headerData)) {
                Interface _i = new Interface(m.Groups[1].Value, m.Groups[3].Value, m.Groups[5].Value);
                Console.WriteLine("FILE: {3} 1: {0} 2: {1} 3: {2}", m.Groups[1].Value, m.Groups[3].Value, m.Groups[5].Value, _toParse.Name);
                _i.AddMethods(m.Groups[6].Value);
                Interfaces.Add(_i.Name, _i);
                _headerData = _headerData.Replace(m.Value, "");
            }
            
            foreach (Match m in _enumRegex.Matches(_headerData)) {
                // We found an enum
                Console.WriteLine("{0}", m.Groups[2].Value);
            }
         }
        
        private String LocateFramework(Framework _tolocate) {
            foreach (String sp in mConfig.SearchPaths) {
                String rp = sp.Replace("%NAME%", _tolocate.Name);
                if(Directory.Exists(rp))
                    return rp;
            }
    
            throw new Exception("Unable to locate framework " +  _tolocate.Name);
        }
        
        private void ProcessFramework(Framework _toprocess) {
            DirectoryInfo _frameworkDirectory = new DirectoryInfo(LocateFramework(_toprocess));
              
            foreach (FileSystemInfo _frameworkHeader in _frameworkDirectory.GetFileSystemInfos())
                ParseFile(_frameworkHeader, _toprocess);

        }
        
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
        
        public void Run() {
            if(!LoadConfiguration())
                return;
                    
            foreach(Framework f in mConfig.Frameworks)
                ProcessFramework(f);

        }   
            
        static void Main(string[] args) {
            ObjCManagedExporter exporter = new ObjCManagedExporter(args);
            exporter.Run();
        }
        
        [XmlRoot("generator")]
        internal class Configuration {
            [XmlElement("framework")]
            public Framework[] Frameworks;
            [XmlElement("searchpath")]
            public String[] SearchPaths;
        }
        
        internal class Framework {
            [XmlElement("name")]
            public string Name;
            [XmlElement("output")]
            public bool Output;
            
        }
            
    }
}