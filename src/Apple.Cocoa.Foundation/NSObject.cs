using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSObject {
		private IntPtr _obj;
		static Hashtable Objects = new Hashtable();

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSObject_alloc();

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSObject_init(IntPtr obj);

		[DllImport("CoreFoundationGlue")]
		static extern void NSObject_release(IntPtr obj);

		public NSObject() : this(NSObject_alloc()) {}

		protected NSObject(IntPtr raw) {
			Raw = raw; 
		}

		protected virtual IntPtr Raw {
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
