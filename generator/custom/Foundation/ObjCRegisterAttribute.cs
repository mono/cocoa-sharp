using System;

namespace Apple.Foundation {

    [AttributeUsage(AttributeTargets.Class)]
	public class ObjCRegisterAttribute : Attribute {
		protected string aName;

		public ObjCRegisterAttribute() {}
		public ObjCRegisterAttribute(string name) {
			this.aName = name;
		}

		public string Name {
			get { return this.aName; }
			set { this.aName = value; }
		}
	}
}
