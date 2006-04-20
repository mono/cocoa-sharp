using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class FileWrapper : Cocoa.Object {
		private static string ObjectiveCName = "NSFileWrapper";                                                                                      
		public FileWrapper (IntPtr native_object) : base (native_object) {}

		public FileWrapper (string path) : base () {
			NativeObject = (IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "initWithPath:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (path).NativeObject);
		}

		public Image Icon {
			get {
				return (Image) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "icon", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setIcon:", typeof (IntPtr), typeof(IntPtr), ((Image)value).NativeObject);
			}
		}
	}
}
