//
//

using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace CocoaSharp {

	unsafe public class Category {
		
		private objc_category occategory;
		private string class_name, name;
		private ArrayList methods, classMethods;
	
		public Category (uint offset, MachOFile file) {
			byte *ptr = file.GetPtr(offset);
			occategory = *(objc_category *)ptr;
			name = file.GetString(occategory.category_name);
			class_name = file.GetString(occategory.class_name);
			MachOFile.DebugOut(0,"Category: {0} class_name : {1}",name,class_name);
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
