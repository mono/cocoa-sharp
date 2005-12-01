using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class RunLoop : Cocoa.Object {
		private static string ObjectiveCName = "NSRunLoop";                                                                                      

		static RunLoop () {
			NativeClasses [typeof (RunLoop)] = Native.RegisterClass (typeof (RunLoop)); 
		}

		public RunLoop () : base () {}

		public RunLoop (IntPtr native_object) : base (native_object) {}

		public static RunLoop Current {
			get {
				return (RunLoop) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend ((IntPtr) NativeClasses [typeof (RunLoop)], "currentRunLoop", typeof (IntPtr)));
			}
		}
		
		public void AddTimer (Timer timer) {
			ObjCMessaging.objc_msgSend (NativeObject, "addTimer:forMode:", typeof (void), typeof (IntPtr), timer.NativeObject, typeof (IntPtr), new Cocoa.String ("NSDefaultRunLoopMode").NativeObject);
		}

		public void Stop () {
			CFRunLoopStop ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "getCFRunLoop", typeof (IntPtr)));
		}

		public void Run () {
			ObjCMessaging.objc_msgSend (NativeObject, "run", typeof (void));
		}
	
		[DllImport ("/System/Library/Frameworks/Foundation.framework/Foundation")]
		private extern static void CFRunLoopStop (IntPtr cfrunloop);
	}
}
