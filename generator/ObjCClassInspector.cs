using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices; 

namespace ObjCManagedExporter
{
	public class ObjCClassInspector {
		[DllImport("libobjc.dylib")]
		public static extern int objc_getClass(string className);
		[DllImport("libobjc.dylib")]
		public static extern int objc_msgSend(int classPtr, int command);
		[DllImport("libobjc.dylib")]
		public static extern int objc_msgSend(int classPtr, int command, int argument);
		[DllImport("libobjc.dylib")]
		public static extern int objc_msgSend(int classPtr, int command, string argument);
		
		[DllImport("libobjc.dylib")]
		public static extern int sel_registerName(string selectorName);

		[DllImport("/System/Library/Frameworks/Cocoa.framework/Cocoa")]
		protected static extern int NSClassFromString(int classPtr);


		private static IDictionary ObjCClasses;
		private static IDictionary ObjCBundles;
		private static int sBundle;
		private static int sPool;

		static ObjCClassInspector() {
			ObjCClasses = new Hashtable();
			ObjCBundles = new Hashtable();
			sPool = objc_getClass("NSAutoreleasePool");
			sPool = objc_msgSend(sPool, sel_registerName("new"));
			sBundle = objc_getClass("NSBundle");
		}
		
		private static int CreateObjCString(string toConvert) {
			int objcString = objc_getClass("NSString");
			return objc_msgSend(objcString, sel_registerName("stringWithCString:"), toConvert);
		}
	
		private static void ReleaseObjCObject(int toRelease) {
			objc_msgSend(toRelease, sel_registerName("release"));
		}
		public static void AddBundle(string bundleName) {
			if(ObjCBundles[bundleName] == null) {
				int objcBundleName = CreateObjCString("/System/Library/Frameworks/" + bundleName + ".framework");
				sBundle = objc_msgSend(sBundle, sel_registerName("bundleWithPath:"), objcBundleName);
				sBundle = objc_msgSend(sBundle, sel_registerName("load"));
				ReleaseObjCObject(objcBundleName);
			}
		}
		public static bool IsObjCClass(string className) {
			if(ObjCClasses[className] != null)
				return true;

			int classAsObjCString = CreateObjCString(className);
			int isClass = NSClassFromString(classAsObjCString);
			ReleaseObjCObject(classAsObjCString);
			if(isClass != 0) {
				ObjCClasses[className] = 1;
			}
			ReleaseObjCObject(isClass);

			return (ObjCClasses[className] != null ? true : false);
		}
	}
}
