using System;
using System.Collections;

namespace ObjCManagedExporter {

    public class Protocol {
        private IDictionary mMethods;
        private String mName;
        private String[] mChildren;
        
        public Protocol(String _name, String _children) {
            mName = _name;
            mChildren = _children.Split(new char[]{' ', ','});
            mMethods = new Hashtable();
        }
        
        public String[] Children {
            get { return mChildren; } set { mChildren = value; }
        }
        
        public String Name {
            get { return mName; } set { mName = value; }
        }
        
        public IDictionary Methods {
            get { return mMethods; }
        }
        
        public void AddMethods(String methods) {
        //    if(mMethods[method] == null) 
        //        mMethods.Add(method, new Method(method));
        }
    }
}