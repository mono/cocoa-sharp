using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSString : NSObject, NSCopying, NSMutableCopying, NSCoding {
		//static IntPtr NSString_class = NSClass("NSString");

		[DllImport("FoundationGlue")]
		static extern IntPtr NSString__stringWithCString(IntPtr CLASS,string str);
		[DllImport("FoundationGlue")]
		static extern IntPtr NSString_initWithUTF8String(IntPtr THIS, string str);

		[DllImport("Foundation")]
		static extern IntPtr NSSelectorFromString(IntPtr str);
		[DllImport("Foundation")]
		static extern IntPtr NSClassFromString(IntPtr str);
		
		public NSString(string str) : this(NSString__stringWithCString(IntPtr.Zero,str)) {}
		protected internal NSString(IntPtr raw) : base(raw) {}

		public static NSString initWithCString(string val) {
			return new NSString(val);
		}

		public static IntPtr NSSelector(string val)
		{
			return NSSelectorFromString(new NSString(val).Raw);
		}
		public static IntPtr NSClass(string val)
		{
			return NSClassFromString(new NSString(val).Raw);
		}
	}
}
