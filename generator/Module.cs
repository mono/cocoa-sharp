using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class Module {

		private objc_module ocmodule;
		private SymbolTable symtab;

		unsafe public Module (objc_module ocmodule, byte *ptr) {
			this.ocmodule = ocmodule;
			this.symtab = new SymbolTable (ptr);
		}

		public int Version {
			get { return (int)ocmodule.version; }
		}

		public string Name {
			get { return Marshal.PtrToStringAuto (ocmodule.name); }
		}

		public SymbolTable SymTab {
			get { return symtab; }
		}

		unsafe public static ArrayList ParseModules (byte *headptr, byte *ptr, int segvmaddr, int segfileoff, int count) {
			ArrayList modules = new ArrayList ();
			objc_module ocmodule;
			Console.WriteLine ("Count: {0}", count);
			for (int i = 0; i < count; i++, ptr+=Marshal.SizeOf (ocmodule)) {
				ocmodule = *((objc_module *)ptr);
				Utils.MakeBigEndian(ref ocmodule.version);
				Utils.MakeBigEndian(ref ocmodule.size);
				modules.Add (new Module (ocmodule, headptr + (ocmodule.symtab.ToInt32 () - segvmaddr + segfileoff)));
			}
			return modules;
		}
	}

	public struct objc_module {
		public uint version;
		public uint size;
		public IntPtr name;
		public IntPtr symtab;
	}
}
