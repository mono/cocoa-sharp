using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSObject {
		static IntPtr _NSObject_class;
		public static IntPtr NSObject_class { get { if (_NSObject_class == IntPtr.Zero) _NSObject_class = NSString.NSClass("NSObject"); return _NSObject_class; } }

		private IntPtr _obj;
		static Hashtable Objects = new Hashtable();

		#region -- Glue --
		[DllImport("Glue")]
		protected static extern IntPtr /*(Class)*/ CreateClassDefinition(string name, string superclassName);
		
		[DllImport("Glue")]
		protected static extern IntPtr /*(id)*/ DotNetForwarding_initWithManagedDelegate(IntPtr THIS, BridgeDelegate managedDelegate);
		#endregion
		
		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSObject__alloc(IntPtr CLASS);

		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSObject_init(IntPtr THIS);

		[DllImport("FoundationGlue")]
		protected static extern void NSObject_release(IntPtr THIS);
		
		[DllImport("Glue")]
		protected static extern IntPtr Class_instanceMethodSignatureForSelector(IntPtr CLASS, IntPtr sel);
		#endregion

		protected enum GlueDelegateWhat {
			methodSignatureForSelector = 0,
			forwardInvocation = 1,
		}
		protected delegate IntPtr BridgeDelegate(GlueDelegateWhat what,IntPtr /*(NSInvocation*)*/ invocation);
		protected static IntPtr /*(Class)*/ NSRegisterClass(Type type) {
			return CreateClassDefinition(type.Name,"NSObject");
		}
		protected IntPtr MethodInvoker(GlueDelegateWhat what,IntPtr /*(NSInvocation*)*/ invocation) {
			switch (what) {
				case GlueDelegateWhat.methodSignatureForSelector:
					return Class_instanceMethodSignatureForSelector(NSString.NSClass("_CSControl"),invocation);
				case GlueDelegateWhat.forwardInvocation:
				{
					string method = new NSInvocation(invocation).selector();
					
					this.GetType().InvokeMember(method, 
												BindingFlags.Default | BindingFlags.InvokeMethod, null, this, null);
					break;
				}
			}
			return IntPtr.Zero;
		}
		
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
