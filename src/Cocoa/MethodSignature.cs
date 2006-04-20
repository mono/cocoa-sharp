using System;

namespace Cocoa {
	public class MethodSignature : Cocoa.Object {

		public MethodSignature () : base () {}

		public MethodSignature (IntPtr native_object) : base (native_object) {}

		public static MethodSignature SignatureWithObjCTypes (string types) {
			return new MethodSignature ();
		}
	}
}
