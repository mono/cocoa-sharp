using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Responder : Cocoa.Object {
		private static string ObjectiveCName = "NSResponder";                                                                                      
		public Responder () : base () {}

		public Responder (IntPtr native_object) : base (native_object) {}

	}
}
