using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class MenuItem : Cocoa.Object {
		private static string ObjectiveCName = "NSMenuItem";                                                                                      
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

        public Menu SubMenu {
            get
            {
                return ObjCMessaging.objc_msgSend(NativeObject, "submenu", typeof(void)) as Menu;
            }
            set
            {
                ObjCMessaging.objc_msgSend(NativeObject, "setSubmenu:", typeof(void), typeof(System.IntPtr), ((Cocoa.Object)value).NativeObject);
            }
        }

        public bool HasSubMenu
        {
            get
            {
                return (bool)ObjCMessaging.objc_msgSend(NativeObject, "hasSubmenu", typeof(System.Boolean));
            }

        }

        public string Title
        {
            get
            {
                return Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "title", typeof(IntPtr))).ToString();
            }
            set
            {
                ObjCMessaging.objc_msgSend(NativeObject, "setTitle:", typeof(IntPtr), typeof(IntPtr), new Cocoa.String(value).NativeObject);
            }
        }
	}
}
