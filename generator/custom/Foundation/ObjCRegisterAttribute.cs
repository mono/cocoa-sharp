using System;

namespace Apple.Foundation {

	public class ObjCExportAttribute : Attribute {
		protected string aName;
		public ObjCExportAttribute() {}
		public ObjCExportAttribute(String name) {
			this.aName = name;
		}

		public String Name {
			get { return this.aName; }
			set { this.aName = value; }
		}
	}
}
