using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    public class Interface {
        private IDictionary mMethods;
        private String mFramework;
        private String mName;
        private String mChild;
        private String[] mProtos;
	private String[] mImports;
	private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
        public Interface(String _name, String _child, String _protos, String _framework) {
            mName = _name;
            mChild = _child;
	    mFramework = _framework;
	    _protos = _protos.Replace(" ", "");		
            mProtos = _protos.Split(new char[]{','});
            mMethods = new Hashtable();
        }
        
        public String Child {
            get { return mChild; } set { mChild = value; }
        }
        
        public String[] Protocols {
            get { return mProtos; } set { mProtos = value; }
        }
        
        public String[] Imports {
            get { return mImports; } set { mImports = value; }
        }
        public String Framework {
            get { return mFramework; } set { mFramework = value; }
        }

        public String Name {
            get { return mName; } set { mName = value; }
        }
        
        public IDictionary Methods {
            get { return mMethods; }
        }
        
        public void AddMethods(String methods) {
	      String[] splitMethods = methods.Split('\n');
	      foreach(string method in splitMethods) 
		if(mMethodRegex.IsMatch(method) && mMethods[method] == null)
			mMethods.Add(method, new Method(method));
        }
    }
}
