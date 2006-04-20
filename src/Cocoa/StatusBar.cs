using System;

namespace Cocoa
{
	public enum StatusItemLength
	{
		VariableStatusItemLength = -1,
		SquareStatusItemLength = -2
	}

	public class StatusBar : Cocoa.Object
	{
		private static string ObjectiveCName = "NSStatusBar";

		public StatusBar (IntPtr native_object) : base (native_object) {}

		public static StatusBar SystemStatusBar
		{
			get
			{
				return (StatusBar) Object.FromIntPtr ((System.IntPtr) ObjCMessaging.objc_msgSend((IntPtr) ObjCClass.FromType (typeof (StatusBar)).ToIntPtr (), "systemStatusBar", typeof(System.IntPtr)));
			}
		}

		public StatusItem StatusItemWithLength(double length)
		{
			return (StatusItem) Object.FromIntPtr ((IntPtr)ObjCMessaging.objc_msgSend(NativeObject, "statusItemWithLength:", typeof(System.IntPtr), typeof(System.Double), length));
		}

		public void RemoveStatusItem(StatusItem statusItem)
		{
			ObjCMessaging.objc_msgSend (NativeObject, "removeStatusItem:", typeof(void), typeof(System.IntPtr), statusItem.NativeObject);
		}

		public bool IsVertical
		{
			get
			{
				return ((int)ObjCMessaging.objc_msgSend (NativeObject, "isVertical", typeof (System.Int32)) == 0 ? false : true);
			}
		}

		public double Thickness
		{
			get
			{
				return (double)ObjCMessaging.objc_msgSend (NativeObject, "thickness", typeof (System.Double));
			}
		}
	}
}
