using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class Module {
		private objc_module ocmodule;
		private SymbolTable symtab;
		private string name;

		unsafe static public Module NewModule(byte *ptr, MachOFile file) {
			objc_module ocmodule = *((objc_module *)ptr);
			Utils.MakeBigEndian(ref ocmodule.version);
			Utils.MakeBigEndian(ref ocmodule.size);
			Utils.MakeBigEndian(ref ocmodule.name);
			Utils.MakeBigEndian(ref ocmodule.symtab);
			byte *symPtr = file.GetPtr(ocmodule.symtab,"__OBJC");
			if (symPtr != null)
				return new Module(ocmodule, symPtr, file);
			return null;
		}
		unsafe public Module(objc_module ocmodule, byte *symPtr, MachOFile file) {
			this.ocmodule = ocmodule;
			name = file.GetString(ocmodule.name);
			MachOFile.DebugOut(1,"Module: {0} version={1}, size={2}, symtab={3,8:x}", name, ocmodule.version, ocmodule.size, ocmodule.symtab);
			symtab = new SymbolTable(symPtr, file);
		}

		public int Version {
			get { return (int)ocmodule.version; }
		}

		public string Name {
			get { return name; }
		}

		public SymbolTable SymTab {
			get { return symtab; }
		}

		unsafe public static ArrayList ParseModules (Section moduleSection, MachOFile file) {
			ArrayList modules = new ArrayList ();
			uint count = moduleSection.Size / (uint)Marshal.SizeOf(typeof(objc_module));
			MachOFile.DebugOut(0,"Module {0}, Count: {1}, Addr={2,8:x}, Offset={3,8:x}", 
				moduleSection.Name, count, moduleSection.Addr, moduleSection.Offset);
			byte *ptr = file.HeadPointer + (int)moduleSection.Offset;
			for (int i = 0; i < count; ++i, ptr += Marshal.SizeOf (typeof(objc_module))) {
				Module m = Module.NewModule(ptr, file);
				if (m != null)
					modules.Add(m);
			}

			return modules;
		}
	}

	unsafe public struct objc_module {
		public uint version;
		public uint size;
		public uint name;
		public uint symtab;
	}
}
