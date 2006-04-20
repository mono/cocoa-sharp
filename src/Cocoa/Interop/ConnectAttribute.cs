using System;

namespace Cocoa {

    [AttributeUsage(AttributeTargets.Field)]
	public class ConnectAttribute : Attribute {
		protected string name;
		protected string type;
		protected int size = -1;

		public ConnectAttribute() {}
		public ConnectAttribute(string name) {
			this.name = name;
		}

		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		public string Type {
			get { return this.type; }
			set { this.type = value; }
		}

		public int Size {
			get { return this.size; }
			set { this.size = value; }
		}
	}
}
