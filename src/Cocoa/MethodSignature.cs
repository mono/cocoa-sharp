using System;

namespace Cocoa {
	public class MethodSignature : Cocoa.Object {

		static MethodSignature () {
			NativeClasses [typeof (MethodSignature)] = Native.RegisterClass (typeof (MethodSignature));
		} 

		public MethodSignature () : base () {}

		public MethodSignature (IntPtr native_object) : base (native_object) {}

		public static MethodSignature SignatureWithObjCTypes (string types) {
			return new MethodSignature ();
		}
	}
}
