using System;
using System.Collections;

namespace ObjCManagedExporter {

    public class Interface {
        private IDictionary mMethods;
        private String mName;
        private String mChild;
        private String[] mProtos;
        
        public Interface(String _name, String _child, String _protos) {
            mName = _name;
            mChild = _child;
            mProtos = _protos.Split(new char[]{' ', ','});
            mMethods = new Hashtable();
        }
        
        public String Child {
            get { return mChild; } set { mChild = value; }
        }
        
        public String[] Protocols {
            get { return mProtos; } set { mProtos = value; }
        }
        
        public String Name {
            get { return mName; } set { mName = value; }
        }
        
        public IDictionary Methods {
            get { return mMethods; }
        }
        
        public void AddMethods(String methods) {
//            if(mMethods[method] == null) 
//                mMethods.Add(method, new Method(method));
        }
    }
}
