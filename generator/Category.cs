//
// $Id: Category.cs,v 1.11 2004/09/08 14:22:44 urs Exp $
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Category {
		
		private objc_category occategory;
		private string class_name, name;
		private ArrayList methods, classMethods;
	
		public Category (byte *ptr, MachOFile file) {
			occategory = *(objc_category *)ptr;
			Utils.MakeBigEndian(ref occategory.category_name);
			Utils.MakeBigEndian(ref occategory.class_name);
			Utils.MakeBigEndian(ref occategory.instance_methods);
			Utils.MakeBigEndian(ref occategory.class_methods);
			Utils.MakeBigEndian(ref occategory.protocols);
			name = file.GetString(occategory.category_name);
			class_name = file.GetString(occategory.class_name);
			MachOFile.DebugOut(1,"Category: {0} class_name : {1}",name,class_name);
			methods = Method.ProcessMethods(occategory.instance_methods,file);
			classMethods = Method.ProcessMethods(occategory.class_methods,file);
		}
	}

	public struct objc_category {
		public uint category_name;
		public uint class_name;
		public uint instance_methods;
		public uint class_methods;
		public uint protocols;
	}
}
