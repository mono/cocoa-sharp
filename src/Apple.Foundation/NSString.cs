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
		[DllImport("FoundationGlue")]
		static extern IntPtr NSString_cString(IntPtr THIS);
		[DllImport("FoundationGlue")]
		static extern int NSString_length(IntPtr THIS);
		#endregion

        #region -- Foundation --
		[DllImport("Foundation")]
		protected static extern IntPtr /*(SEL)*/ NSSelectorFromString(IntPtr /*(NSString*)*/ str);
		[DllImport("Foundation")]
		protected static extern IntPtr /*(Class)*/ NSClassFromString(IntPtr /*(NSString*)*/ str);
		[DllImport("Foundation")]
		static extern IntPtr /*(NSString*)*/ NSStringFromSelector(IntPtr /*SEL)*/ sel);
		#endregion
		
		public NSString(string str) : this(NSString__stringWithCString(IntPtr.Zero,str)) {}
		~NSString() { Raw = IntPtr.Zero; /* Strings are autoreleased, no release needed */ }
		protected internal NSString(IntPtr raw) : base(raw) {}

		public static NSString FromString(string val) {
			return new NSString(val);
		}
		public static IntPtr /*(SEL)*/ NSSelector(string val) {
			return NSSelectorFromString(new NSString(val).Raw);
		}
		public static IntPtr /*(Class)*/ NSClass(string val) {
			return NSClassFromString(new NSString(val).Raw);
		}
		public static NSString FromSEL(IntPtr /*(SEL)*/ sel) {
			return new NSString(NSStringFromSelector(sel));
		}
		
		public override string ToString() {
			return (string)Marshal.PtrToStringAuto(NSString_cString(Raw));
		}
		public int length() {
			return NSString_length(Raw);
		}
	}
}
