using System;

namespace Apple.Foundation {

	public class ObjCRegisterAttribute : Attribute {
		protected string aName;
		public ObjCRegisterAttribute() {}
		public ObjCRegisterAttribute(String name) {
			this.aName = name;
		}

		public String Name {
			get { return this.aName; }
			set { this.aName = value; }
		}
	}
}
