using System;

namespace Apple.Foundation {

	public class ObjCConnectAttribute : Attribute {
		protected string aName;
		protected string aType;
		protected int aSize = -1;
		public ObjCConnectAttribute() {}
		public ObjCConnectAttribute(String name) {
			this.aName = name;
		}

		public String Name {
			get { return this.aName; }
			set { this.aName = value; }
		}

		public String Type {
			get { return this.aType; }
			set { this.aType = value; }
		}

		public int Size {
			get { return this.aSize; }
			set { this.aSize = value; }
		}
	}
}
