using System;

namespace Cocoa
{
	public class StatusItem : Cocoa.Object
	{
		private static string ObjectiveCName = "NSStatusItem";

		public StatusItem (IntPtr native_object) : base (native_object)
		{
			this.Retain();
		}

		public StatusBar StatusBar
		{
			get
			{
				return (StatusBar) Object.FromIntPtr ((System.IntPtr) ObjCMessaging.objc_msgSend(NativeObject, "statusBar", typeof(System.IntPtr)));
			}
		}

		public bool HighlightMode
		{
			get
			{
				return ((int)ObjCMessaging.objc_msgSend(NativeObject, "highlightMode", typeof(System.Int32)) == 0 ? false : true);
			}
			
			set
			{
				ObjCMessaging.objc_msgSend(NativeObject, "setHighlightMode:", typeof(void), typeof(System.Int32), (value == false ? 0 : 1));
			}
		}
		
		public string Title
		{
			get
			{
				return Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "title", typeof(System.IntPtr))).ToString ();
			}
			
			set
			{
				ObjCMessaging.objc_msgSend(NativeObject, "setTitle:", typeof(void), typeof(IntPtr), new Cocoa.String(value).NativeObject);
			}
		}
	}
}
