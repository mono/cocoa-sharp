using System;

namespace Cocoa {
	internal class NativeRepresentation {
		private string[] methods;
		private string[] signatures;
		private NativeMember[] members;

		public string[] Methods {
			get { return this.methods; }
			set { this.methods = value; }
		}
		
		public string[] Signatures {
			get { return this.signatures; }
			set { this.signatures = value; }
		}
		
		public NativeMember[] Members {
			get { return this.members; }
			set { this.members = value; }
		}
	}
}
