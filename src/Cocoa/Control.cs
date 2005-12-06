using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Control : View {
		private static string ObjectiveCName = "NSControl";                                                                                      

		static Control () {
			NativeClasses [typeof (Control)] = Native.RegisterClass (typeof (Control)); 
		}

		public Control () : base () {}

		public Control (Rect frame) : base (frame) {} 

		public Control (IntPtr native_object) : base (native_object) {}

		public string Value {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "stringValue", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setStringValue:", typeof (void), typeof (System.IntPtr), new Cocoa.String (value).NativeObject);
			}
		}
		
		public Cocoa.Object Target {
			get {
				return (Cocoa.Object)ObjCMessaging.objc_msgSend (NativeObject, "target", typeof (void));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setTarget:", typeof (void), typeof (System.IntPtr), ((Cocoa.Object)value).NativeObject);
			}
		}

		//NOTE: This technically isn't in control; but since pretty much every subclass of control has it we move it here to avoid code duplication
		public event ActionHandler DoubleClick {
			add {
				Cocoa.Object target = (Cocoa.Object)((ActionHandler)value).Target;
				MethodInfo method = ((ActionHandler)value).Method;
				string selector = method.Name;
				foreach (ExportAttribute export_attribute in Attribute.GetCustomAttributes (method, typeof (ExportAttribute))) {
					if (export_attribute.Selector != null)
						selector = export_attribute.Selector;
				}
				Target = target;
				ObjCMessaging.objc_msgSend (NativeObject, "setDoubleAction:", typeof (void), typeof (IntPtr), sel_getUid (selector));
			}
			remove {
				// TODO: Remove the handler
				ObjCMessaging.objc_msgSend (NativeObject, "setDoubleAction:", typeof (void), typeof (IntPtr), IntPtr.Zero);
			}
		}

		public bool Enabled {
			get {
				return(bool) ObjCMessaging.objc_msgSend(NativeObject, "isEnabled", typeof(System.Boolean));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setEnabled:", typeof(void), typeof(System.Boolean), value);
			}
		}

		[DllImport ("libobjc.dylib")]
		private static extern IntPtr sel_getUid (string name);
	}
}
