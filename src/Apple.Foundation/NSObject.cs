using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSObject {
		//static IntPtr NSObject_class = NSString.NSClass("NSObject");

		private IntPtr _obj;
		static Hashtable Objects = new Hashtable();

		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSObject__alloc(IntPtr CLASS);

		[DllImport("FoundationGlue")]
		static extern IntPtr NSObject_init(IntPtr THIS);

		[DllImport("FoundationGlue")]
		static extern void NSObject_release(IntPtr THIS);

		public NSObject() : this(NSObject__alloc(IntPtr.Zero)) {}

		protected NSObject(IntPtr raw) {
			Raw = raw; 
		}

		public IntPtr Raw {
			get {
				return _obj;
			}
			set {
				Objects [value] = new WeakReference (this);
				_obj = value;
			}
		}

		public NSObject init() {
			Raw = NSObject_init(Raw);
			return this;
		}

		public void release() {
			NSObject_release(Raw);
		}
	}
}
