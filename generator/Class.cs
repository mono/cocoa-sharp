using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Class {
		
		private objc_class occlass;
	
		public Class (byte *ptr) {
			occlass = *((objc_class *)ptr);
			Utils.MakeBigEndian(ref occlass.version);
			Utils.MakeBigEndian(ref occlass.info);
			Utils.MakeBigEndian(ref occlass.instance_size);

			Console.WriteLine ("Class: {0}", Marshal.PtrToStringAuto (occlass.name));
		}
	}

	public struct objc_class {
		public IntPtr isa;
		public IntPtr super_class;
		public IntPtr name;
		public uint version;
		public uint info;
		public uint instance_size;
		public IntPtr ivars;
		public IntPtr methodLists;
		public IntPtr cache;
		public IntPtr protocols;
	}
}
