using System;
using System.Collections;
using System.Reflection;

namespace Apple.Tools {

        public class ObjCClassRepresentation {
                private String[] methods;
                private String[] signatures;
                private int nummethods;

		public String[] Methods {
			get {
				return methods;
			}
			set {
				methods = value;
			}
		}
		
		public String[] Signatures {
			get {
				return signatures;
			}
			set {
				signatures = value;
			}
		}

		public int NumMethods {
			get {
				return nummethods;
			}
			set {
				nummethods = value;
			}
		}
        }
}
