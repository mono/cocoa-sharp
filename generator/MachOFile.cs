//
// $Id: MachOFile.cs,v 1.9 2004/09/04 04:18:32 urs Exp $
//

using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	public class Utils {
		public static void MakeBigEndian(ref int value) 
		{
			uint tmp = (uint)value;
			MakeBigEndian(ref tmp);
			value = (int)tmp;
		}

		public static void MakeBigEndian(ref uint value) 
		{
			if (BitConverter.IsLittleEndian) 
			{
				byte[] bytes = BitConverter.GetBytes(value);
				value = BitConverter.ToUInt32(new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] },0);
			}
		}

		public static void MakeBigEndian(ref short value) 
		{
			ushort tmp = (ushort)value;
			MakeBigEndian(ref tmp);
			value = (short)tmp;
		}

		public static void MakeBigEndian(ref ushort value) 
		{
			if (BitConverter.IsLittleEndian) 
			{
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
		private static int DEBUG_LEVEL = 0;
		private ArrayList commands;

		static MachOFile () {
			try {
				DEBUG_LEVEL = Int32.Parse (System.Environment.GetEnvironmentVariable ("COCOASHARP_GENERATOR_DEBUG_LEVEL"));
			} catch (Exception) {
				DEBUG_LEVEL = 0;
			}
		}

		public MachOFile () {
		}

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
			SegmentCommand segment = this.SegmentContainingAddress(offset);
			if (segment == null) {
				DebugOut(0,"ERROR: Segment for offset {0} not found",offset);
				return null;
			}
			if (segName != null && segment.Name != segName) {
				DebugOut(0,"ERROR: Segment has wrong name {0} != {1}",segment.Name,segName);
				return null;
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

		public static void DebugOut(int level, string format, params object[] args) {
			if (DEBUG_LEVEL >= level) 
				Console.WriteLine(format,args);
		}

		public static void DebugOut(string format, params object[] args) {
			DebugOut(1,format,args);
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
					DebugOut(0,"DEBUG: DylibCommand()");
					cmd = new DylibCommand (this, lcmd);
				} else { 
					DebugOut(0,"DEBUG: LoadCommand()");
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

		public void Parse () {
		}
	}

	public struct load_command {
		public uint cmd;
		public uint cmdsize;
	}

	public struct mach_header {
		public uint magic;
		public int cputype;
		public int cpusubtype;
		public uint filetype;
		public uint ncmds;
		public uint sizeofcmds;
		public uint flags;
	}
}
