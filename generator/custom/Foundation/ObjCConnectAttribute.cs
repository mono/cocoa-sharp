using System;

namespace Apple.Foundation {

    [AttributeUsage(AttributeTargets.Field)]
	public class ObjCConnectAttribute : Attribute {
		protected string aName;
		protected string aType;
		protected int aSize = -1;

		public ObjCConnectAttribute() {}
		public ObjCConnectAttribute(string name) {
			this.aName = name;
		}

		public string Name {
			get { return this.aName; }
			set { this.aName = value; }
		}

		public string Type {
			get { return this.aType; }
			set { this.aType = value; }
		}

		public int Size {
			get { return this.aSize; }
			set { this.aSize = value; }
		}
	}
}
