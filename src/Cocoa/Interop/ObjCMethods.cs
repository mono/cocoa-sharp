using System;
using System.Runtime.InteropServices;

namespace Cocoa {
	public class ObjCMethods {
//		[DllImport("libobjc.dylib")]
//		public static extern void objc_setClassHandler(ClassHandlerDelegate class_handler);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr object_getInstanceVariable(IntPtr id, string name, IntPtr val);

		[DllImport("libobjc.dylib")]
		public static extern IntPtr object_setInstanceVariable(IntPtr id, string name, IntPtr val);  

		[DllImport ("libobjc.dylib")]
		public static extern IntPtr objc_lookUpClass (string name);
		
		[DllImport ("libobjc.dylib")]
		public static extern IntPtr objc_getClass (string name);

		[DllImport ("libobjc.dylib")]
		public static extern IntPtr sel_getUid (string name);
		
		[DllImport ("libobjc.dylib")]
		public static extern IntPtr sel_getUid (IntPtr name);
		
		[DllImport ("libobjc.dylib")]
		public static extern void objc_addClass (IntPtr cls);
		
		[DllImport ("libobjc.dylib")]
		public static extern void class_addMethods (IntPtr cls, IntPtr methods);

		[DllImport ("libobjc.dylib")]
		public static extern void class_poseAs (IntPtr cls, IntPtr ocls);
		
		[DllImport ("libobjc.dylib")]
		public static extern IntPtr class_getInstanceMethod (IntPtr cls, IntPtr sel);
		
		[DllImport ("libobjc.dylib")]
		public static extern int method_getNumberOfArguments (IntPtr method);
		
		[DllImport ("libobjc.dylib")]
		public static extern int dlopen (string name, int type);
	}
}
