//
//  Authors
//    - Kangaroo, Geoff Norton
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//
//  Copyright (c) 2004 Quark Inc.  All rights reserved.
//
// $Id: Generator.cs,v 1.1 2004/09/20 16:42:52 gnorton Exp $
//

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;

namespace CocoaSharp {

    public class Generator {
        private static string mXmlFile = "generator.xml";
        private static Configuration mConfig;

        public Generator() {
        }

        public int Run() {
            foreach (Framework f in mConfig.Frameworks) {
                WriteCS csWriter = new WriteCS(mConfig);
                // find search path for f.name;
                string realfilename = "/System/Library/Frameworks/Foundation.framework/Versions/Current/" + f.Name;
                MachOFile mfile = new MachOFile (realfilename);
                // ToClass needs namespace we set that property in MachOFile
                mfile.Namespace = "Apple." + f.Name;
                csWriter.AddRange(mfile.Classes);
                csWriter.OutputNamespace("Apple." + f.Name);
            }

            return 1;
        }

        static int Main (string[] args) {
            if(!LoadConfiguration(args))
                return -1;

            Generator g = new Generator();
            int retval = g.Run();

            Console.WriteLine("Updating mapping.");
            Method.SaveMapping();
            return retval;
        }

        private static bool LoadConfiguration(string[] args) {
            // Ensure the file exists
             foreach (string a in args)
                if(a.IndexOf("-xml:") == 0)
                    mXmlFile = a.Substring(5);
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
    }

}

//	$Log: Generator.cs,v $
//	Revision 1.1  2004/09/20 16:42:52  gnorton
//	More generator refactoring.  Start using the MachOGen for our classes.
//
