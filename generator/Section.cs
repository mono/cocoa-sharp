//
// $Id: Section.cs,v 1.10 2004/09/07 20:51:21 urs Exp $
//

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CocoaSharp {
	
	public class Section {
	
		private MachOFile mfile;
		private segment_command scmd;
		private section sec;
		private string segname;
		private string sectname;

		public Section (MachOFile mfile, segment_command scmd) {
			this.mfile = mfile;
			this.scmd = scmd;
		}

		public string Name {
			get { return sectname; }
		}
		
		public uint Offset {
			get { return sec.offset; }
		}
		
		public uint Addr {
			get { return sec.addr; }
		}
		
		public uint Size {
			get { return sec.size; }
		}

		public bool ContainsAddress(uint offset) {
			int off = (int)(offset-Addr);
			return (off >= 0) && (off < Size);
		}

		public uint SegmentOffsetForVMAddr(uint offset) {
			return offset - Addr;
		}

		public void ProcessSection () {
			unsafe {
				sec = *((section *)mfile.Pointer);
				Utils.MakeBigEndian(ref sec.addr);
				Utils.MakeBigEndian(ref sec.size);
				Utils.MakeBigEndian(ref sec.offset);
				Utils.MakeBigEndian(ref sec.align);
				Utils.MakeBigEndian(ref sec.reloff);
				Utils.MakeBigEndian(ref sec.nreloc);
				Utils.MakeBigEndian(ref sec.flags);
				Utils.MakeBigEndian(ref sec.reserved1);
				Utils.MakeBigEndian(ref sec.reserved2);
				sectname = Utils.GetString(mfile.Pointer, 16);
				segname = Utils.GetString(mfile.Pointer+16, 16);
				mfile.Pointer += (int)Marshal.SizeOf (sec);
			}

			MachOFile.DebugOut(0,"\t\tSectName: {0}", sectname);
		}
	}

	// http://developer.apple.com/documentation/DeveloperTools/Conceptual/MachORuntime/FileStructure/chapter_4_section_8.html#//apple_ref/doc/uid/20001298/section
	//
	// Directly following a segment_command data structure is an array of section data structures, with the exact count determined by the nsects field of the 
	// segment_command structure.
	public struct section {
		// A string specifying the name of this section. The value of this field can be any sequence of ASCII characters, although section names defined by Apple 
		// begin with two underscores and consist of lowercase letters (as in __text and __data). This field is fixed at 16 bytes in length.
		public byte sectname0,sectname1,sectname2,sectname3,sectname4,sectname5,sectname6,sectname7,
			sectname8,sectname9,sectname10,sectname11,sectname12,sectname13,sectname14,sectname15;
		// A string specifying the name of the segment that should eventually contain this section. For compactness, intermediate object files�files of type 
		// MH_OBJECT�contain only one segment, in which all sections are placed. The static linker places each section in the named segment when building the final 
		// product (any file that is not of type MH_OBJECT).
		public byte segname0,segname1,segname2,segname3,segname4,segname5,segname6,segname7,
			segname8,segname9,segname10,segname11,segname12,segname13,segname14,segname15;
		// An integer specifying the virtual memory address of this section.
		public uint addr;
		// An integer specifying the size in bytes of the virtual memory occupied by this section.
		public uint size;
		// An integer specifying the offset to this section in the file.
		public uint offset;
		// An integer specifying the section�s byte alignment. Specify this as a power of two; for example, a section with 8-byte alignment would have an align value 
		// of 3 (2 to the 3rd power equals 8).
		public uint align;
		// An integer specifying the file offset of the first relocation entry for this section.
		public uint reloff;
		// An integer specifying the number of relocation entries located at reloff for this section.
		public uint nreloc;
		// An integer divided into two parts. The least significant 8 bits contain the section type, while the most significant 24 bits contain a set of flags that 
		// specify other attributes of the section. These types and flags are primarily used by the static linker and file analysis tools, such as otool, to determine 
		// how to modify or display the section. These are the possible types:
		// - S_REGULAR�This section has no particular type. The standard tools create a __TEXT,__text section of this type.
		// - S_ZEROFILL�Zero-fill-on-demand section�when this section is first read from or written to, each page within is automatically filled with bytes containing 
		//   zero.
		// - S_CSTRING_LITERALS�This section contains only constant C strings. The standard tools create a __TEXT,__cstring section of this type.
		// - S_4BYTE_LITERALS�This section contains only constant values that are 4 bytes long. The standard tools create a __TEXT,__literal4 section of this type.
		// - S_8BYTE_LITERALS�This section contains only constant values that are 8 bytes long. The standard tools create a __TEXT,__literal8 section of this type.
		// - S_LITERAL_POINTERS�This section contains only pointers to constant values.
		// - S_NON_LAZY_SYMBOL_POINTERS�This section contains only non-lazy pointers to symbols. The standard tools create a section of the __DATA,__nl_symbol_ptrs 
		//   section of this type.
		// - S_LAZY_SYMBOL_POINTERS�This section contains only lazy pointers to symbols. The standard tools create a __DATA,__la_symbol_ptrs section of this type.
		// - S_SYMBOL_STUBS��This section contains symbol stubs. The standard tools create __TEXT,__symbol_stub and __TEXT,__picsymbol_stub sections of this type. 
		//   See �Indirect Addressing� for more information.
		// - S_MOD_INIT_FUNC_POINTERS�This section contains pointers to module initialization functions. The standard tools create __DATA,__mod_init_func sections of 
		//   this type.
		// - S_MOD_TERM_FUNC_POINTERS�This section contains pointers to module termination functions. The standard tools create __DATA,__mod_term_func sections of this
		//   type.
		// - S_COALESCED�This section contains symbols that are coalesced by the static linker and possibly the dynamic linker. More than one file may contain coalesced
		//   definitions of the same symbol without causing multiple-defined-symbol errors.
		// The following are the possible attributes of a section:
		// - S_ATTR_PURE_INSTRUCTIONS�This section contains only executable machine instructions. The standard tools set this flag for the sections __TEXT,__text, 
		//   __TEXT,__symbol_stub, and __TEXT,__picsymbol_stub.
		// - S_ATTR_NO_TOC�This section contains coalesced symbols that must not be placed in the table of contents (SYMDEF member) of a static archive library.
		// - S_ATTR_SOME_INSTRUCTIONS�This section contains executable machine instructions and other data.
		// - S_ATTR_EXT_RELOC�This section contains references that must be relocated. These references refer to data that exists in other files (undefined symbols). 
		//   To support external relocation, the maximum virtual memory protections of the segment that contains this section must allow both reading and writing.
		// - S_ATTR_LOC_RELOC�This section contains references that must be relocated. These references refer to data within this file.
		// - S_ATTR_STRIP_STATIC_SYMS�The static symbols in this section can be stripped if the MH_DYLDLINK flag of the image�s mach_header header structure is set.
		// - S_ATTR_NO_DEAD_STRIP�This section must not be dead-stripped. See �Dead-Code Stripping� in Xcode Build System for details.
		// - S_ATTR_LIVE_SUPPORT�This section must not be dead-stripped if they reference code that is live, but the reference is undetectable.
		public uint flags;
		// An integer reserved for use with certain section types. For symbol pointer sections and symbol stubs sections that refer to indirect symbol table entries, 
		// this is the index into the indirect table for this section�s entries. The number of entries is based on the section size divided by the size of the symbol 
		// pointer or stub. Otherwise this field is set to zero.
		public uint reserved1;
		// For sections of type S_SYMBOL_STUBS, an integer specifying the size (in bytes) of the symbol stub entries contained in the section. Otherwise, this field is 
		// reserved for future use and should be set to zero.
		public uint reserved2;
	}
}
