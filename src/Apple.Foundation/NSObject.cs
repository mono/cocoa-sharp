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
		protected static extern IntPtr /*(NSMethodSignature *)*/ MakeMethodSignature(string types,int nargs, int sizeOfParams, int returnValueLength);
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
				{
					string types = "";
					int nargs = 2;
					int sizeOfParams = 0;
					int returnValueLength = 0;
					
					// Get the method info for this method.
					MethodInfo method = this.GetType().GetMethod(NSString.FromSEL(invocation).ToString());
					// Determine the return type and push it onto the types
					switch(method.ReturnType.ToString()) {
						case "System.Void":
							types = types + "v";
							returnValueLength = 0;
							break;
					}
					// Add the id and the selector to the types
					types = types + "@:";
					// Get the parameters for this method
					ParameterInfo[] parms = method.GetParameters();
					// Increment the nargs to include the parms
					nargs += parms.Length;
					// Add each parm to the types
					foreach (ParameterInfo p in parms) {
						Console.WriteLine("MethodParam: {0}", p.ParameterType.ToString());
						switch(p.ParameterType.ToString()) {
							case "System.Void":
								break;
						}
					}
					// WHY?
					sizeOfParams = 160;
					// Make the info
					return MakeMethodSignature(types,nargs,sizeOfParams,returnValueLength);
				}
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
