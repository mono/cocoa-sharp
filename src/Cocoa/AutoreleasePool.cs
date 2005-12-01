using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Cocoa {

	public class AutoreleasePool : Object {	
		private static string ObjectiveCName = "NSAutoreleasePool";

		#region Constructors
		static AutoreleasePool () {
			NativeClasses [typeof (AutoreleasePool)] = Native.RegisterClass (typeof (AutoreleasePool));
		} 

		public AutoreleasePool () : base () { Initialize (); }

		public AutoreleasePool (IntPtr native_object) : base (native_object) {}
		#endregion

		#region Properties
		#endregion
		
		#region Methods
		#endregion

		#region PInvokes
		#endregion
	}

}
