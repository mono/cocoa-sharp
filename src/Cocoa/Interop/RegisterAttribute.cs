using System;

namespace Cocoa {

    [AttributeUsage(AttributeTargets.Class)]
	public class RegisterAttribute : Attribute {
		protected string name;

		public RegisterAttribute() {}
		public RegisterAttribute(string name) {
			this.name = name;
		}

		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}
	}
}
