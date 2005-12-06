using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class MenuItem : Cocoa.Object {
		private static string ObjectiveCName = "NSMenuItem";                                                                                      

		static MenuItem () {
			NativeClasses [typeof (MenuItem)] = Native.RegisterClass (typeof (MenuItem)); 
		}

		public MenuItem (IntPtr native_object) : base (native_object) {}

		public Cocoa.Object Target {
			get {
				return (Cocoa.Object)ObjCMessaging.objc_msgSend (NativeObject, "target", typeof (void));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setTarget:", typeof (void), typeof (System.IntPtr), ((Cocoa.Object)value).NativeObject);
			}
		} 

		public event ActionHandler Click {
			add {
				Cocoa.Object target = (Cocoa.Object)((ActionHandler)value).Target;
				MethodInfo method = ((ActionHandler)value).Method;
				string selector = method.Name;
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
					if (export_attribute.Selector != null)
						selector = export_attribute.Selector;
				}
				Target = target;
				ObjCMessaging.objc_msgSend (NativeObject, "setAction:", typeof (void), typeof (IntPtr), new Cocoa.String (selector).NativeObject);
			}
			remove {
				// TODO: Remove the handler
				ObjCMessaging.objc_msgSend (NativeObject, "setAction:", typeof (void), typeof (IntPtr), IntPtr.Zero);
			}
		}
			
		public Cocoa.CellStateValue State {
			get {
				return(Cocoa.CellStateValue) ObjCMessaging.objc_msgSend(NativeObject, "state", typeof(System.Int32));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setState:", typeof(void), typeof(System.Int32), value);
			}
		}
	}
}
