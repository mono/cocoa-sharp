using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

    public class CEnum {
        private String mName;
        private String mFramework;
        private String mEnum;
        
        public CEnum(String _name, String _struct, String _framework) {
            mName = _name;
	    mEnum = _struct;
	    mFramework = _framework;
        }
        
	public String Name {
		get { return mName; }
	}
	public String Framework {
		get { return mFramework; }
	}
	public String CSEnum {
		get {
			String structVal = "using System;\n";
			structVal += "namespace Apple." + mFramework + " {\n";
			structVal += "protected enum " + mName + " {\n" + mEnum + "}\n}";
			return structVal;
		}
	}
    }
}
