using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Cocoa.Foundation
{
	public class NSString : NSObject, NSCopying, NSMutableCopying, NSCoding {

		[DllImport("CoreFoundationGlue")]
		static extern IntPtr NSString_initWithCString(string str);

		public NSString(string str) : this(NSString_initWithCString(str)) {}
		protected internal NSString(IntPtr raw) : base(raw) {}

		public static NSString initWithCString(string val) {
			return new NSString(val);
		}
	}
}
