//
//  NSObject.cs
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSObject.cs,v 1.10 2004/06/17 05:48:00 gnorton Exp $
//

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

using Apple.Tools;

namespace Apple.Foundation
{
	public class NSObject : BridgeHelper {
		static IntPtr _NSObject_class;
		public static IntPtr NSObject_class { get { if (_NSObject_class == IntPtr.Zero) _NSObject_class = NSString.NSClass("NSObject"); return _NSObject_class; } }

		private IntPtr _obj;
		static Hashtable Objects = new Hashtable();

		#region -- Glue --
		[DllImport("Glue")]
		protected static extern IntPtr /*(Class)*/ CreateClassDefinition(string name, string superclassName, int nummethods, IntPtr[] methods, IntPtr[] signatures);
		
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
		protected static extern IntPtr /*(NSMethodSignature *)*/ MakeMethodSignature(string types);
		#endregion

		protected enum GlueDelegateWhat {
			methodSignatureForSelector = 0,
			forwardInvocation = 1,
		}
		protected delegate IntPtr BridgeDelegate(GlueDelegateWhat what,IntPtr /*(NSInvocation*)*/ invocation);
		protected static IntPtr /*(Class)*/ NSRegisterClass(Type type) {
			ObjCClassRepresentation r = GenerateObjCRepresentation(type);
			for(int i = 0; i < r.Methods.Length; i++)
				Console.WriteLine("{0} {1}", r.Methods[i], r.Signatures[i]);
			IntPtr retval = IntPtr.Zero;
			unsafe {
				IntPtr[] methods = new IntPtr[r.NumMethods];
				IntPtr[] signatures = new IntPtr[r.NumMethods];

				for(int i = 0; i < r.NumMethods; i++) {
					methods[i] = Marshal.StringToCoTaskMemAnsi(r.Methods[i]);
					signatures[i] = Marshal.StringToCoTaskMemAnsi(r.Signatures[i]);
				}
				retval = CreateClassDefinition(type.Name, "NSObject", r.NumMethods, methods, signatures); 
				for(int i = 0; i < r.NumMethods; i++) {
					Marshal.FreeCoTaskMem(methods[i]);
					Marshal.FreeCoTaskMem(signatures[i]);
				}
			}
			return retval;
		}
		protected IntPtr MethodInvoker(GlueDelegateWhat what,IntPtr arg) {
			switch (what) {
				case GlueDelegateWhat.methodSignatureForSelector:
				{
					return MakeMethodSignature(GenerateMethodSignature(this.GetType(), NSString.FromSEL(arg).ToString()));
				}
				case GlueDelegateWhat.forwardInvocation:
				{
					NSInvocation invocation = new NSInvocation(arg);
					InvokeMethodByObject(this, invocation.selector(), null);
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

//***************************************************************************
//
// $Log: NSObject.cs,v $
// Revision 1.10  2004/06/17 05:48:00  gnorton
// Modified to move non apple stuff out of NSObject
//
// Revision 1.9  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
