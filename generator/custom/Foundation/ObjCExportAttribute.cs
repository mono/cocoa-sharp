using System;

namespace Apple.Foundation {

	public class ObjCExportAttribute : Attribute {
		protected string aSelector;
		protected string aSignature;
		public ObjCExportAttribute() {}
		public ObjCExportAttribute(String selector) {
			this.aSelector = selector;
		}

		public String Selector {
			get { return this.aSelector; }
			set { this.aSelector = value; }
		}

		public String Signature {
			get { return this.aSignature; }
			set { this.aSignature = value; }
		}
	}
}
