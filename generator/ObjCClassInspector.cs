using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices; 

namespace ObjCManagedExporter
{
	public class ObjCClassInspector {
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_getClass(string className);
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr classPtr, IntPtr command);
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr classPtr, IntPtr command, IntPtr argument);
		[DllImport("libobjc.dylib")]
		public static extern IntPtr objc_msgSend(IntPtr classPtr, IntPtr command, string argument);
		
		[DllImport("libobjc.dylib")]
		public static extern IntPtr sel_registerName(string selectorName);

		[DllImport("/System/Library/Frameworks/Cocoa.framework/Cocoa")]
		protected static extern IntPtr NSClassFromString(IntPtr classPtr);

		private static IDictionary ObjCClasses;
		private static IDictionary ObjCBundles;
		private static IntPtr sNSBundle, sPool, sNSString;

		static ObjCClassInspector() {
			ObjCClasses = new Hashtable();
			ObjCBundles = new Hashtable();
			sPool = objc_msgSend(objc_getClass("NSAutoreleasePool"), sel_registerName("new"));
		}
		
		private static IntPtr CreateObjCString(string toConvert) {
			return objc_msgSend(objc_getClass("NSString"), sel_registerName("stringWithCString:"), toConvert);
		}
		private static void ReleaseObjCObject(IntPtr toRelease) {
			objc_msgSend(toRelease, sel_registerName("release"));
		}
		public static void AddBundle(string bundleName) {
			if(!ObjCBundles.Contains(bundleName)) {
				IntPtr objcBundleName = CreateObjCString("/System/Library/Frameworks/" + bundleName + ".framework");
				IntPtr bundle = objc_msgSend(objc_getClass("NSBundle"), sel_registerName("bundleWithPath:"), objcBundleName);
				objc_msgSend(bundle, sel_registerName("load"));
				ObjCBundles[bundleName] = true;
			}
		}
		public static bool IsObjCClass(string className) {
			if(ObjCClasses.Contains(className))
				return (bool)ObjCClasses[className];

			IntPtr isClass = NSClassFromString(CreateObjCString(className));

			ObjCClasses[className] = isClass != IntPtr.Zero;
			return isClass != IntPtr.Zero;
		}
	}
}
