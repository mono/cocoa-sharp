using System;
using System.Runtime.InteropServices;

namespace Apple.Foundation
{
	public class NSInvocation : NSObject {
		static IntPtr NSInvocation_class = NSString.NSClass("NSInvocation");
		
		#region -- FoundationGlue --
		[DllImport("FoundationGlue")]
		static extern IntPtr/*(SEL)*/ NSInvocation_selector(IntPtr /*(NSInvocation*)*/ THIS);
		#endregion

		public NSInvocation() : this(NSObject__alloc(NSInvocation_class)) {}
		protected internal NSInvocation(IntPtr raw) : base(raw) {}

		public string selector() {
			return NSString.FromSEL(NSInvocation_selector(Raw)).ToString();
		}
	}
}
