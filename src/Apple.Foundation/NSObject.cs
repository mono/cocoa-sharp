using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSObject {
		static IntPtr _NSObject_class;
		public static IntPtr NSObject_class { get { if (_NSObject_class == IntPtr.Zero) _NSObject_class = NSString.NSClass("NSObject"); return _NSObject_class; } }

		private IntPtr _obj;
		static Hashtable Objects = new Hashtable();

		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSObject__alloc(IntPtr CLASS);

		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSObject_init(IntPtr THIS);

		[DllImport("FoundationGlue")]
		protected static extern void NSObject_release(IntPtr THIS);
		#endregion

		public NSObject() : this(NSObject__alloc(IntPtr.Zero)) {}
		~NSObject() {
		    if (Raw != IntPtr.Zero)
		        release();
		}

		protected NSObject(IntPtr raw) {
			Raw = raw; 
		}

		public IntPtr Raw {
			get {
				return _obj;
			}
			set {
			    if (value != IntPtr.Zero)
				    Objects [value] = new WeakReference (this);
				_obj = value;
			}
		}

		public static NSObject alloc() {
			return new NSObject();
		}


		public NSObject init() {
			Raw = NSObject_init(Raw);
			return this;
		}

		public void release() {
			NSObject_release(Raw);
			Raw = IntPtr.Zero;
		}
	}
}
