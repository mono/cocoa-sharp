using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class OpenGLView : View {
		private static string ObjectiveCName = "NSOpenGLView";                                                                                      
		public OpenGLView () : base () {}

		public OpenGLView (Rect frame) : base (frame) {}

		public OpenGLView (IntPtr native_object) : base (native_object) {}
	}
}
