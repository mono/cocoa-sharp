using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSString : NSObject, NSCopying, NSMutableCopying, NSCoding {
		static IntPtr _NSString_class;
		public static IntPtr NSString_class { get { if (_NSString_class == IntPtr.Zero) _NSString_class = NSString.NSClass("NSString"); return _NSString_class; } }

        #region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSString__stringWithCString(IntPtr CLASS,string str);

		[DllImport("FoundationGlue")]
		protected static extern IntPtr NSString_initWithUTF8String(IntPtr THIS, string str);
		#endregion

        #region -- Foundation --
		[DllImport("Foundation")]
		protected static extern IntPtr NSSelectorFromString(IntPtr str);
		[DllImport("Foundation")]
		protected static extern IntPtr NSClassFromString(IntPtr str);
		#endregion
		
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
