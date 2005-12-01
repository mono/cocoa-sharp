using System;
using System.Collections;

namespace Cocoa {
	public class Array : Object {
		private static string ObjectiveCName = "NSArray";

		static Array () {
			NativeClasses [typeof (Array)] = Native.RegisterClass (typeof (Array));
		} 

		public Array () : base () {}

		public Array (IntPtr native_object) : base (native_object) {}

		public object [] ToArray () {
			ArrayList list = new ArrayList ();
			Type t = null;
			for (int i = 0; i < (int)ObjCMessaging.objc_msgSend (NativeObject, "count", typeof (int)); i++) {
				list.Add (Native.NativeToManaged ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "objectAtIndex:", typeof (IntPtr), typeof (int), i)));
				if (t == null)
					t = list [0].GetType ();
			}
			return (object [])list.ToArray (t);
		}
	}
}
