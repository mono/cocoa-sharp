using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    public class Struct {
        private String mName;
        private String mFramework;
        private String mStruct;
        
        public Struct(String _name, String _struct, String _framework) {
            mName = _name;
	    mStruct = _struct;
	    mFramework = _framework;
        }
        
	public String Name {
		get { return mName; }
	}
	public String Framework {
		get { return mFramework; }
	}
	public String CSStruct {
		get {
			String structVal = "using System;\n";
			structVal += "namespace Apple." + mFramework + " {\n";
			structVal += "protected enum " + mName + " {\n" + mStruct + "}\n}";
			return structVal;
		}
	}
    }
}
