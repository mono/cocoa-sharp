using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    public class Protocol {
        private IDictionary mMethods;
        private String mName;
        private String mFramework;
        private String[] mChildren;
	private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
        public Protocol(String _name, String _children, String _framework) {
            mName = _name;
	    mFramework = _framework;
            mChildren = _children.Split(new char[]{' ', ','});
            mMethods = new Hashtable();
        }
        
        public String[] Children {
            get { return mChildren; } set { mChildren = value; }
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
