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

		public bool Enabled {
			get {
				return (bool) ObjCMessaging.objc_msgSend(NativeObject, "isEnabled", typeof(System.Boolean));
			}
			set {
				ObjCMessaging.objc_msgSend(NativeObject, "setEnabled:", typeof(void), typeof(System.Boolean), value);
			}
		}

#region action handling
		//NOTE: This technically isn't in control; but since pretty much every subclass of control has it we move it here to avoid code duplication
		
		private Cocoa.Object Target {
			get {
				return (Cocoa.Object)ObjCMessaging.objc_msgSend (NativeObject, "target", typeof (void));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setTarget:", typeof (void), typeof (System.IntPtr), ((Cocoa.Object)value).NativeObject);
			}
		}

		private string ActionSelector {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "action", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setAction:", typeof (void), typeof (IntPtr), sel_getUid (value));
			}
		}

		private string DoubleActionSelector {
			get {
				return (string) Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "doubleAction", typeof (IntPtr)));
			}
			set {
				ObjCMessaging.objc_msgSend (NativeObject, "setDoubleAction:", typeof (void), typeof (IntPtr), sel_getUid (value));
			}
		}

		private ActionReceiver actionTarget = null;

		public event EventHandler Action {
			add {
				if (actionTarget == null) {
					actionTarget = new ActionReceiver();
					Target = actionTarget;
					ActionSelector = "action";
				}
				actionTarget.Action += value;
			}
			remove {
				if (actionTarget != null) {
					actionTarget.Action -= value;
				}
			}
		}

		public event EventHandler DoubleAction {
			add {
				if (actionTarget == null) {
					actionTarget = new ActionReceiver();
					Target = actionTarget;
					DoubleActionSelector = "doubleAction";
				}
				actionTarget.DoubleAction += value;
			}
			remove {
				if (actionTarget != null) {
					actionTarget.DoubleAction -= value;
				}
			}
		}

		private class ActionReceiver : Cocoa.Object {
			private EventHandler actionHandler = null;
			private EventHandler doubleActionHandler = null;

			public ActionReceiver () {
			}

			public ActionReceiver (IntPtr native) : base (native) {}

			public event EventHandler Action {
				add {
					actionHandler = (EventHandler)EventHandler.Combine(actionHandler, value);
				}
				remove {
					actionHandler = (EventHandler)EventHandler.Remove(actionHandler, value);
				}
			}

			public event EventHandler DoubleAction {
				add {
					doubleActionHandler = (EventHandler)EventHandler.Combine(doubleActionHandler, value);
				}
				remove {
					doubleActionHandler = (EventHandler)EventHandler.Remove(doubleActionHandler, value);
				}
			}
			
			[Export ("action")]
			public void OnAction () {
				if (actionHandler != null)
					actionHandler(this, EventArgs.Empty);
			}
			
			[Export ("doubleAction")]
			public void OnDoubleAction () {
				if (doubleActionHandler != null)
					doubleActionHandler(this, EventArgs.Empty);
			}
		}
#endregion

		[DllImport ("libobjc.dylib")]
		private static extern IntPtr sel_getUid (string name);
	}
}
