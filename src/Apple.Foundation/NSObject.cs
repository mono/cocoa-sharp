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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/src/Apple.Foundation/Attic/NSObject.cs,v 1.16 2004/06/19 17:19:27 gnorton Exp $
//

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using Apple.Tools;

namespace Apple.Foundation
{
	
	public class NSObject {
		public static object NS2Net(IntPtr raw) {
			return TypeConverter.NS2Net(raw);
		}
		
		public static IntPtr Net2NS(object obj) {
			return TypeConverter.Net2NS(obj);
		}
	
		static IntPtr _NSObject_class;
		public static IntPtr NSObject__class { get { if (_NSObject_class == IntPtr.Zero) _NSObject_class = Class.Get("NSObject"); return _NSObject_class; } }

		private IntPtr _obj;
		protected bool _release;
		static Hashtable Objects = new Hashtable();

		#region -- Glue --
		[DllImport("Glue")]
		protected static extern IntPtr /*(Class)*/ CreateClassDefinition(string name, string superclassName, int nummethods, IntPtr[] methods, IntPtr[] signatures);

		[DllImport("Glue")]
		protected static extern IntPtr /*(id)*/ DotNetForwarding_initWithManagedDelegate(IntPtr THIS, BridgeDelegate managedDelegate);
		#endregion

		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr NSObject__alloc(IntPtr CLASS);

		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr NSObject_class(IntPtr THIS);

		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr NSObject_className(IntPtr THIS);

		[DllImport("FoundationGlue")]
		protected internal static extern IntPtr NSObject_init(IntPtr THIS);

		[DllImport("FoundationGlue")]
		protected internal static extern void NSObject_release(IntPtr THIS);

		[DllImport("Glue")]
		protected internal static extern IntPtr /*(NSMethodSignature *)*/ MakeMethodSignature(string types);
		#endregion

		protected enum GlueDelegateWhat {
			methodSignatureForSelector = 0,
			forwardInvocation = 1,
		}
		protected delegate IntPtr BridgeDelegate(GlueDelegateWhat what,IntPtr /*(NSInvocation*)*/ invocation);
		protected static IntPtr /*(Class)*/ NSRegisterClass(Type type, String superclass) {
			ObjCClassRepresentation r = BridgeHelper.GenerateObjCRepresentation(type);
			for(int i = 0; i < r.Methods.Length; i++)
				Console.WriteLine("{0} {1}", r.Methods[i], r.Signatures[i]);
			IntPtr retval = IntPtr.Zero;

			IntPtr[] methods = new IntPtr[r.NumMethods];
			IntPtr[] signatures = new IntPtr[r.NumMethods];

			for(int i = 0; i < r.NumMethods; i++) {
				methods[i] = Marshal.StringToCoTaskMemAnsi(r.Methods[i]);
				signatures[i] = Marshal.StringToCoTaskMemAnsi(r.Signatures[i]);
			}
			retval = CreateClassDefinition(type.Name, superclass, r.NumMethods, methods, signatures); 
			for(int i = 0; i < r.NumMethods; i++) {
				Marshal.FreeCoTaskMem(methods[i]);
				Marshal.FreeCoTaskMem(signatures[i]);
			}
			return retval;
		}
		protected IntPtr MethodInvoker(GlueDelegateWhat what,IntPtr arg) {
			switch (what) {
				case GlueDelegateWhat.methodSignatureForSelector:
					return MakeMethodSignature(BridgeHelper.GenerateMethodSignature(this.GetType(), NSString.FromSEL(arg).ToString()));
				case GlueDelegateWhat.forwardInvocation:
				{
					NSInvocation invocation = new NSInvocation(arg,false);
					object[] args = ProcessInvocation(invocation);
					
					BridgeHelper.InvokeMethodByObject(this, invocation.Selector, ProcessInvocation(invocation));
					break;
				}
			}
			return IntPtr.Zero;
		}
		
		public NSObject() : this(NSObject__alloc(IntPtr.Zero),true) {}
		~NSObject() {
			if (Raw != IntPtr.Zero && _release)
				release();
		}

		protected internal NSObject(IntPtr raw,bool release) {
			SetRaw(raw,release);
		}

		public IntPtr Raw {
			get { return _obj; }
		}

		public void SetRaw(IntPtr raw,bool release) {
		    if (raw != IntPtr.Zero)
			    Objects [raw] = new WeakReference (this);
			_obj = raw;
			_release = release;
		}

		public static NSObject alloc() {
			return new NSObject();
		}

		public NSObject init() {
			SetRaw(NSObject_init(Raw),_release);
			return this;
		}

		public Class Class {
			get { return new Class(NSObject_class(Raw),false); }
		}

		public string ClassName {
			get { return new NSString(NSObject_className(Raw),false).ToString(); }
		}

		public void release() {
			NSObject_release(Raw);
			SetRaw(IntPtr.Zero,false);
		}

		private object[] ProcessInvocation(NSInvocation invocation) {
			string method = invocation.Selector;
			if(method.IndexOf(":") > 0)
				method = method.Substring(0, method.IndexOf(":"));

			int numArgs = BridgeHelper.GetParameterInfosByMethod(BridgeHelper.GetMethodByTypeAndName(this.GetType(), method)).Length;
			object[] retArgs = new object[numArgs];
			for(int i = 0; i < numArgs; i++) {
				retArgs[i] = invocation.getArgument(i);
			}
			return retArgs;
		}
	}

	public class Class : NSObject {
		/*
		void class_addMethods(Class aClass, struct objc_method_list* methodList);
		id class_createInstance(Class theClass, unsigned additionalByteCount);
		Method class_getClassMethod(Class aClass, SEL aSelector);
		Method class_getInstanceMethod(Class aClass, SEL aSelector);
		Ivar class_getInstanceVariable(Class aClass, const char* aVariableName);
		int class_getVersion(Class theClass);
		struct objc_method_list* class_nextMethodList(Class theClass, void** iterator);
		Class class_poseAs(Class imposter, Class original);
		void class_removeMethods(Class aClass, struct objc_method_list* methodList);
		void class_setVersion(Class theClass, int version);
		*/

		#region -- Foundation --
		[DllImport("Foundation")]
		protected static extern IntPtr /*(Class)*/ NSClassFromString(IntPtr /*(NSString*)*/ str);
		#endregion

		public static IntPtr Get(string className) {
			return NSClassFromString(new NSString(className).Raw);
		}

		private Class() : this(IntPtr.Zero,false) {}

		protected internal Class(IntPtr raw,bool release) : base(raw,release) {}
		public Class(string name) : this(NSClassFromString(new NSString(name).Raw),false) {}

		public string Name {
			get { return ClassName; }
		}
	}
}

//***************************************************************************
//
// $Log: NSObject.cs,v $
// Revision 1.16  2004/06/19 17:19:27  gnorton
// Broken API fixes.
// Delegates and methods with multi-argument support working.
// Argument parsing and casting working for all our known classes.
//
// Revision 1.15  2004/06/18 20:13:00  gnorton
// Support for multi-argument method signatures/calling in .Net
//
// Revision 1.14  2004/06/18 03:42:45  gnorton
// Doesn't need to be unsafe anymore because we can pass IntPtr[] and have it become char ** nicely after our conversion; we still clean up the managed memory tho
//
// Revision 1.13  2004/06/17 16:10:45  gnorton
// Cleanup
//
// Revision 1.12  2004/06/17 15:58:07  urs
// Public API cleanup, making properties and using .Net types rather then NS*
//
// Revision 1.11  2004/06/17 13:06:27  urs
// - release cleanup: only call release when requested
// - loader cleanup
//
// Revision 1.10  2004/06/17 05:48:00  gnorton
// Modified to move non apple stuff out of NSObject
//
// Revision 1.9  2004/06/16 12:20:27  urs
// Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//
//***************************************************************************
