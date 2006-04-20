using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Timer : Cocoa.Object {
		private static string ObjectiveCName = "NSTimer";                                                                                      
		private double interval=0;

		public Timer () : base () {}

		public Timer (IntPtr native_object) : base (native_object) {}

		public double Interval {
			get {
				return interval;
			}
			set {
				interval = value;
			}
		}

		public void Start () {
			RunLoop.Current.AddTimer (this);
		}

		public void Stop () {
			ObjCMessaging.objc_msgSend (NativeObject, "invalidate", typeof (void));
		} 

		public event ActionHandler Tick {
                        add {
                                Cocoa.Object target = (Cocoa.Object)((ActionHandler)value).Target;
                                MethodInfo method = ((ActionHandler)value).Method;
                                string selector = method.Name;
				ExportAttribute attr = (ExportAttribute) Attribute.GetCustomAttribute (method, typeof (ExportAttribute));
				if (attr != null)
					selector = attr.Selector;
				NativeObject = (IntPtr) ObjCMessaging.objc_msgSend ((IntPtr)ObjCClass.FromType (typeof (Timer)).ToIntPtr (), "timerWithTimeInterval:target:selector:userInfo:repeats:", typeof (IntPtr), typeof (double), interval, typeof (IntPtr), target.NativeObject, typeof (IntPtr), ObjCMethods.sel_getUid (selector), typeof (IntPtr), IntPtr.Zero, typeof (bool), true);
                        }
                        remove {
                                // TODO: Remove the handler
                        }
                }  

	}
}
