using System;

namespace Apple.Foundation {

    [AttributeUsage(AttributeTargets.Method)]
	public class ObjCExportAttribute : Attribute {
		protected string aSelector;
		protected string aSignature;
		protected bool aAutoSync = true;

		public ObjCExportAttribute() {}
		public ObjCExportAttribute(string selector) {
			this.aSelector = selector;
		}

		public string Selector {
			get { return this.aSelector; }
			set { this.aSelector = value; }
		}

		public string Signature {
			get { return this.aSignature; }
			set { this.aSignature = value; }
		}
		
		public bool AutoSync {
			get { return this.aAutoSync; }
			set { this.aAutoSync = value; }
		}
	}
}
