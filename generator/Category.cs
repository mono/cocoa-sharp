using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    public class Category {
        private IDictionary mMethods;
        private String mName;
        private String mClass;
	private String[] mImports;
	private static Regex mMethodRegex = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
        
        public Category(String _name, String _class) {
            mName = _name;
            mClass = _class;
            mMethods = new Hashtable();
        }
        
	public String[] Imports {
            get { return mImports; } set { mImports = value; }
        }
        public String Class {
            get { return mClass; } set { mClass = value; }
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
