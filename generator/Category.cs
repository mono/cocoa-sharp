using System;
using System.Collections;

namespace ObjCManagedExporter {

    public class Category {
        private IDictionary mMethods;
        private String mName;
        private String mClass;
        
        public Category(String _name, String _class) {
            mName = _name;
            mClass = _class;
            mMethods = new Hashtable();
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
  //          if(mMethods[method] == null) 
    //            mMethods.Add(method, new Method(method));
        }
    }
}