//
// $Id: MachOFile.cs,v 1.14 2004/09/07 21:02:43 urs Exp $
//

using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	public class Utils {
		public static void MakeBigEndian(ref int value) {
			uint tmp = (uint)value;
			MakeBigEndian(ref tmp);
			value = (int)tmp;
		}

		public static void MakeBigEndian(ref uint value) {
			if (BitConverter.IsLittleEndian) {
				byte[] bytes = BitConverter.GetBytes(value);
				value = BitConverter.ToUInt32(new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] },0);
			}
		}

		public static void MakeBigEndian(ref short value) {
			ushort tmp = (ushort)value;
			MakeBigEndian(ref tmp);
			value = (short)tmp;
		}

		public static void MakeBigEndian(ref ushort value) {
			if (BitConverter.IsLittleEndian) {
				byte[] bytes = BitConverter.GetBytes(value);
				value = BitConverter.ToUInt16(new byte[] { bytes[1], bytes[0] },0);
			}
		}

		public static unsafe string GetString(byte* data,int length) {
			string ret = Marshal.PtrToStringAnsi(new IntPtr (data), length);
			int termChar = ret.IndexOf((char)0);
			if (termChar >= 0)
				ret = ret.Substring(0,termChar);
			return ret;
		}
	}

	public class MachOFile {

		private const uint MH_MAGIC = 0xfeedface;
		private const uint MH_CIGAM = 0xcefaedfe;

		private const uint LC_REQ_DYLD = 0x80000000;
		private const uint LC_SEGMENT = 0x1;
		private const uint LC_LOAD_DYLIB = 0xc;
		private const uint LC_ID_DYLIB = 0xd;
		private const uint LC_LOAD_WEAK_DYLIB = (0x18 | LC_REQ_DYLD);

		private string filename;
		private byte[] filedata;
		private unsafe byte* ptr;
		private unsafe byte* headptr;
		private mach_header header;
		private ArrayList commands;

		static public IDictionary Types = new Hashtable();
		static private int DEBUG_LEVEL = 0;
		static MachOFile () {
			try {
				DEBUG_LEVEL = Int32.Parse (System.Environment.GetEnvironmentVariable ("COCOASHARP_GENERATOR_DEBUG_LEVEL"));
			} catch (Exception) {
				DEBUG_LEVEL = 0;
			}
		}

		public static void DebugOut(int level, string format, params object[] args) {
			if (DEBUG_LEVEL >= level) 
				Console.WriteLine(format,args);
		}

		public static void DebugOut(string format, params object[] args) {
			DebugOut(1,format,args);
		}

		public MachOFile () {}
		public MachOFile (string filename) {
			commands = new ArrayList ();
			this.filename = filename;
			LoadFile ();
		}

		unsafe private void LoadFile () {
			if (!File.Exists (filename))
				throw new Exception ("ERROR: " + filename + " does not exist");
			FileStream fs = new FileStream (filename, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader (fs);
			filedata = new byte [fs.Length];
			reader.Read (filedata, 0, filedata.Length);
			reader.Close ();
			fixed (byte *pdata = filedata) {
				ptr = pdata;
				headptr = ptr;
			}
			ParseHeader ();
			LoadCommands ();
			ProcessModules ();
		}

		unsafe public byte* Pointer {
			get { return ptr; }
			set { ptr = value; }
		}
		unsafe public byte* HeadPointer {
			get { return headptr; }
		}

		public SegmentCommand SegmentContainingAddress(uint offset) {
			foreach (ICommand cmd in this.commands) {
				SegmentCommand scmd = cmd as SegmentCommand;
				if (scmd != null && scmd.ContainsAddress(offset))
					return scmd;
			}
			return null;
		}

		public SegmentCommand SegmentWithName(string segmentName) {
			foreach (ICommand cmd in this.commands) {
				SegmentCommand scmd = cmd as SegmentCommand;
				if (scmd != null && scmd.Name == segmentName) 
					return scmd;
			}

			return null;
		}

		unsafe public byte * GetPtr(uint offset) {
			return GetPtr(offset,null);
		}

		unsafe public byte* GetPtr(uint offset,string segName) {
			if (offset == 0)
				return null;
			SegmentCommand segment; 
			if (segName != null) {
				segment = this.SegmentWithName(segName);
				if (segment == null) {
					DebugOut(0,"ERROR: Segment with name {0} not found",segName);
					return null;
				}
				if (!segment.ContainsAddress(offset)) {
					DebugOut(1,"ERROR: Segment {0} does not contain offset {1}",segName,offset);
					return null;
				}
			}
			else {
				segment = this.SegmentContainingAddress(offset);
				if (segment == null) {
					DebugOut(0,"ERROR: Segment for offset {0} not found",offset);
					return null;
				}
			}
			return HeadPointer + (int)(offset - segment.VMAddr + segment.FileOffset);
		}

		public string GetString(uint offset) {
			unsafe {
				byte * ptr = GetPtr(offset);
				if (ptr == null)
					return null;

				int len = 0;
				byte *tmp = ptr;
				while (*tmp++ != 0) ++len;
				return Marshal.PtrToStringAnsi(new IntPtr(ptr));
			}
		}

		public string Filename {
			get { return filename; }
			set { 
				this.filename = value; 
				LoadFile ();
			}
		}

		private void ParseHeader () {
			unsafe {
				this.header = *((mach_header *)Pointer);

				Utils.MakeBigEndian(ref this.header.magic);
				Utils.MakeBigEndian(ref this.header.cputype);
				Utils.MakeBigEndian(ref this.header.cpusubtype);
				Utils.MakeBigEndian(ref this.header.filetype);
				Utils.MakeBigEndian(ref this.header.ncmds);
				Utils.MakeBigEndian(ref this.header.sizeofcmds);
				Utils.MakeBigEndian(ref this.header.flags);
				Pointer += Marshal.SizeOf (header);
			}

			if (this.header.magic != MH_MAGIC && this.header.magic != MH_CIGAM)
				throw new Exception ("ERROR: " + filename + " is not a MachO file (" + String.Format ("{0:X}", this.header.magic) + ").");

			DebugOut("MachOFile.cs-> header dump:");
			DebugOut("magic: {0:X}", header.magic);
			DebugOut("cputype: {0:X} {1}", header.cputype, (header.cputype == 0x12 ? "PowerPC" : "Unknown"));
			DebugOut("cpusubtype: {0:X}", header.cpusubtype);
			DebugOut("filetype: {0:X}", header.filetype);
			DebugOut("ncmds: {0}", header.ncmds);
			DebugOut("sizeofcmds: {0}", header.sizeofcmds);
			DebugOut("flags: {0}", header.flags);
		}

		private void LoadCommands () {
			for (int i = 0; i < header.ncmds; i++) {
				load_command lcmd;
				unsafe {
					lcmd = *((load_command *)Pointer);
					Utils.MakeBigEndian(ref lcmd.cmd);
					Utils.MakeBigEndian(ref lcmd.cmdsize);
					Pointer += Marshal.SizeOf (lcmd);
				}

				ICommand cmd;

				DebugOut("MachOFile.cs:LoadCommands(): load_command dump:");
				DebugOut("cmd: {0:X}", lcmd.cmd);
				DebugOut("cmdsize: {0}", lcmd.cmdsize);

				if (lcmd.cmd == LC_SEGMENT) {
					DebugOut(0,"DEBUG: SegmentCommand()");
					cmd = new SegmentCommand (this, lcmd);
				} else if (lcmd.cmd == LC_ID_DYLIB || lcmd.cmd == LC_LOAD_DYLIB || lcmd.cmd == LC_LOAD_WEAK_DYLIB) {
					DebugOut(0,"DEBUG: DylibCommand({0,8:x})",lcmd.cmd);
					cmd = new DylibCommand (this, lcmd);
				} else { 
					DebugOut(0,"DEBUG: LoadCommand({0,8:x})",lcmd.cmd);
					cmd = new LoadCommand (this, lcmd);
				}

				cmd.ProcessCommand ();
				commands.Add (cmd);
			}
		}

		private void ProcessModules () {
			SegmentCommand objcSegment = this.SegmentWithName("__OBJC");
			if (objcSegment == null)
				throw new Exception ("ERROR: __OBJC segment not found in MachOFile");
			Section moduleSection = objcSegment.SectionWithName("__module_info");
			if (moduleSection == null)
				throw new Exception ("ERROR: __module_info not found in __OBJC segment");

			ArrayList modules = Module.ParseModules (moduleSection, this);
		}

		public void Parse () {}
	}

	// http://developer.apple.com/documentation/DeveloperTools/Conceptual/MachORuntime/FileStructure/chapter_4_section_6.html#//apple_ref/doc/uid/20001298/load_command
	public struct load_command {
		// An integer indicating the type of load command. Table 3-2 lists the valid load command types
		public uint cmd;
		// An integer specifying the total size in bytes of the load command data structure. Each load command structure contains a different set of data, depending on
		// the load command type, so each might have a different size. The size must always be a multiple of 4. This means the cmdsize field must always divide evenly 
		// by 4. If the load command data does not divide evenly by 4, add bytes containing zeros to the end until it does.
		public uint cmdsize;
	}
	
	// Commands				Data structures			Purpose
	//----------------------------------------------------------------------------------------------------------------------------------------------------------------------
	// LC_SEGMENT			segment_command			Defines a segment of this file to be mapped into the address space of the process that loads this file. It also includes 
	//												all the sections contained by the segment. See ÒMach-O File Format ReferenceÓ.
	// LC_SYMTAB			symtab_command			Specifies the symbol table for this file. This information is used by both static and dynamic linkers when linking the 
	//												file, and also by debuggers to map symbols to the original source code files from which the symbols were generated.
	// LC_DYSYMTAB			dysymtab_command		Specifies additional symbol table information used by the dynamic linker.
	// LC_THREAD			thread_command			For an executable file, the LC_UNIXTHREAD command defines the initial thread state of the main thread of the process.
	// LC_UNIXTHREAD								LC_THREAD is similar to LC_UNIXTHREAD but does not cause the kernel to allocate a stack.
	// LC_LOAD_DYLIB		dylib_command			Defines the name of a dynamic shared library that this file links against.
	// LC_ID_DYLIB			dylib_command			Specifies the install name of a dynamic shared library.
	// LC_PREBOUND_DYLIB	prebound_dylib_command	For a shared library that this executable is linked prebound against, specifies the modules in the shared library that are used.
	// LC_LOAD_DYLINKER		dylinker_command		Specifies the dynamic linker that the kernel executes to load this file.
	// LC_ID_DYLINKER		dylinker_command		Identifies this file as a dynamic linker.
	// LC_ROUTINES			routines_command		Contains the offset of the shared library initialization routine (specified by the linkerÕs -init option).
	// LC_TWOLEVEL_HINTS	twolevel_hints_command	Contains the two-level namespace lookup hint table.
	// LC_SUB_FRAMEWORK		sub_framework_command	Identifies this file as the implementation of a subframework of an umbrella framework. The name of the umbrella 
	//												framework is stored in the string parameter.
	// LC_SUB_UMBRELLA		sub_umbrella_command	Specifies a file that is a subumbrella of this umbrella framework.
	// LC_SUB_LIBRARY		sub_library_command		Identifies this file as the implementation of a sublibrary of an umbrella framework. The name of the umbrella 
	//												framework is stored in the string parameter. Note that Apple has not defined a supported location for sublibraries.
	// LC_SUB_CLIENT		sub_client_command		A subframework can explicitly allow another framework or bundle to link against it by including an LC_SUB_CLIENT load 
	//												command containing the name of the framework or a client name for a bundle.

	// http://developer.apple.com/documentation/DeveloperTools/Conceptual/MachORuntime/FileStructure/chapter_4_section_4.html#//apple_ref/doc/uid/20001298/BAJHHFFF
	public struct mach_header {
		// An integer containing a value identifying this file as a Mach-O executable file. Use the constant MH_MAGIC if the file is intended for use on a CPU with the 
		// same endianness as the computer on which the compiler is running. The constant MH_CIGAM can be used when the byte ordering scheme of the target machine is 
		// the reverse of the host CPU.
		public uint magic;
		// An integer indicating the CPU architecture you intend to use the file on. Appropriate values include:
		// - CPU_TYPE_POWERPC for PowerPC-architecture CPUs
		// - CPU_TYPE_I386 for x86-architecture CPUs
		public int cputype;
		// An integer specifying the exact model of the CPU. To run on all PowerPC or x86 processors supported by the Mac OS X kernel, this should be set to 
		// CPU_SUBTYPE_POWERPC_ALL or CPU_SUBTYPE_I386_ALL
		public int cpusubtype;
		// An integer indicating the usage and alignment of the file. Valid values for this field include:
		// - The MH_OBJECT file type is the format used for intermediate object files. It is a very compact format containing all its sections in one segment. The 
		//   compiler and assembler usually create one MH_OBJECT file for each source code file. By convention, the file name extension for this format is .o.
		// - The MH_EXECUTE file type is the format used by standard executable programs.
		// - The MH_BUNDLE file type is the type typically used by code that you load at runtime (typically called bundles or plug-ins). By convention, the file name 
		//	 extension for this format is .bundle.
		// - The MH_DYLIB file type is for dynamic shared libraries. It contains some additional tables to support multiple modules. By convention, the file name 
		//   extension for this format is .dylib, except for the main shared library of a framework, which does not usually have a file name extension.
		// - The MH_PRELOAD file type is an executable format used for special-purpose programs that are not loaded by the Mac OS X kernel, such as programs burned into 
		//   programmable ROM chips. Do not confuse this file type with the MH_PREBOUND flag, which is a flag that the static linker sets in the header structure to mark 
		//   a prebound image.
		// - The MH_CORE file type is used to store core files, which are traditionally created when a program crashes. Core files store the entire address space of a 
		//   process at the time it crashed. You can later run gdb on the core file to figure out why the crash occurred.
		// - The MH_DYLINKER file type is the type of a dynamic linker shared library. This is the type that dyld is constructed from.
		public uint filetype;
		// An integer indicating the number of load commands following the header structure
		public uint ncmds;
		// An integer indicating the number of bytes occupied by the load commands following the header structure
		public uint sizeofcmds;
		// An integer containing a set of bit flags that indicate the state of certain optional features of the Mach-O file format. These are the masks you can use to 
		// manipulate this field:
		// - MH_NOUNDEFSÑThe object file contained no undefined references when it was built.
		// - MH_INCRLINKÑThe object file is the output of an incremental link against a base file and cannot be linked again.
		// - MH_DYLDLINKÑThe file is input for the dynamic linker and cannot be statically linked again.
		// - MH_BINDATLOADÑThe dynamic linker should bind the undefined references when the file is loaded.
		// - MH_PREBOUNDÑThe fileÕs undefined references are prebound.
		// - MH_SPLIT_SEGSÑThe file has its read-only and read-write segments split.
		// - MH_TWOLEVELÑThe image is using two-level namespace bindings.
		// - MH_FORCE_FLATÑThe executable is forcing all images to use flat namespace bindings.
		// - MH_SUBSECTIONS_VIA_SYMBOLSÑThe sections of the object file can be divided into individual blocks. These blocks are dead-stripped if they are not used by 
		//   other code. See ÒDead-Code StrippingÓ in Xcode Build System for details.
		public uint flags;
	}
}
