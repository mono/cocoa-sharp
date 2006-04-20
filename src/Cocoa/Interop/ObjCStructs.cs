using System;
using System.Runtime.InteropServices;

namespace Cocoa {
	[StructLayout (LayoutKind.Sequential)]
	public struct objc_super {
		public IntPtr receiver;
		public IntPtr superclass;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct objc_class {
		public IntPtr isa;
		public IntPtr super_class;
		public IntPtr name;
		public int version;
		public int info;
		public int instance_size;
		public IntPtr ivars;
		public IntPtr methodLists;
		public IntPtr cache;
		public IntPtr protocols;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct objc_ivar_list {
		public int count;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct objc_ivar {
		public IntPtr ivar_name;
		public IntPtr ivar_type;
		public int ivar_offset;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct objc_method_list {
		public IntPtr obsolete;
		public int count;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct objc_method {
		public IntPtr name;
		public IntPtr types;
		public Delegate imp;
	} 
}
