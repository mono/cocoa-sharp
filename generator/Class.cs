using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Class {
		
		public objc_class occlass;
	
		public Class (byte *headptr, uint offset, SegmentCommand objcSegment) {
			Utils.MakeBigEndian(ref offset);
			byte *ptr = headptr+(int)(offset - objcSegment.VMAddr + objcSegment.FileOffset);
			occlass = *(objc_class *)ptr;
			Utils.MakeBigEndian(ref occlass.version);
			Utils.MakeBigEndian(ref occlass.info);
			Utils.MakeBigEndian(ref occlass.instance_size);
			string name = Marshal.PtrToStringAnsi(new IntPtr(ptr + 8));
			Console.WriteLine ("Class: {0}", name);
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
