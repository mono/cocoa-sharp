using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSString : NSObject, NSCopying, NSMutableCopying, NSCoding {

		[DllImport("FoundationGlue")]
		static extern IntPtr NSString_alloc();
		[DllImport("FoundationGlue")]
		static extern IntPtr NSString_initWithCString(IntPtr THIS, string str);

		[DllImport("FoundationGlue")]
		static extern IntPtr SEL_fromString(IntPtr str);

		public NSString(string str) : this(NSString_initWithCString(NSString_alloc(), str)) {}
		protected internal NSString(IntPtr raw) : base(raw) {}

		public static NSString initWithCString(string val) {
			return new NSString(val);
		}

		public static IntPtr NSSelector(string val)
		{
			return SEL_fromString(new NSString(val).Raw);
		}
	}
}