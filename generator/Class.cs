using System;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Class {
		
		public objc_class occlass;
	
		public Class (uint offset, MachOFile file) {
			byte *ptr = file.GetPtr(offset);
			if (ptr == null)
				return;
			occlass = *(objc_class *)ptr;
			Utils.MakeBigEndian(ref occlass.super_class);
			Utils.MakeBigEndian(ref occlass.name);
			Utils.MakeBigEndian(ref occlass.version);
			Utils.MakeBigEndian(ref occlass.info);
			Utils.MakeBigEndian(ref occlass.instance_size);
			string name = file.GetString(occlass.name);
			Console.WriteLine ("Class: {0}", name);
		}
	}

	public struct objc_class {
		public uint isa;
		public uint super_class;
		public uint name;
		public uint version;
		public uint info;
		public uint instance_size;
		public uint ivars;
		public uint methodLists;
		public uint cache;
		public uint protocols;
	}
}
