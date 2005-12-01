using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Timer : Cocoa.Object {
		private static string ObjectiveCName = "NSTimer";                                                                                      
		private double interval=0;

		static Timer () {
			NativeClasses [typeof (Timer)] = Native.RegisterClass (typeof (Timer)); 
		}

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
                                foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
                                        if (export_attribute.Selector != null)
                                                selector = export_attribute.Selector;
                                }
				NativeObject = (IntPtr) ObjCMessaging.objc_msgSend ((IntPtr)NativeClasses [typeof (Timer)], "timerWithTimeInterval:target:selector:userInfo:repeats:", typeof (IntPtr), typeof (double), interval, typeof (IntPtr), target.NativeObject, typeof (IntPtr), sel_getUid (selector), typeof (IntPtr), IntPtr.Zero, typeof (bool), true);
                        }
                        remove {
                                // TODO: Remove the handler
                        }
                }  

		[DllImport ("libobjc.dylib")]
		private static extern IntPtr sel_getUid (string name);    
	}
}
