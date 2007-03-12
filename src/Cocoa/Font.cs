using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa 
{
	public class Font : Cocoa.Object 
    {
        
		private static string ObjectiveCName = "NSFont"; 
        
        
		public Font (IntPtr native_object) 
            : base (native_object)
        {
        }
        
        /// <summary>
        /// Instanciates an instance of Font with a specify font name and size.
        /// </summary>
        /// <param name="aFontName">
        /// A fully specified family-face name, such as Helvetica-BoldOblique or Times-Roman. 
        /// </param>
        /// <param name="aFontSize">
        /// Used to scale the font. If you use a fontSize of 0.0, this method uses the default User Font size
        /// </param>
        public Font( string aFontName, float aFontSize )
        {
            NativeObject = (IntPtr) ObjCMessaging.objc_msgSendSuper( NativeObject,
                "initWithName:size:", 
                typeof (IntPtr), 
                typeof (IntPtr), new Cocoa.String( aFontName ).NativeObject, /// Name
                typeof (float), aFontSize /// Size
            );
        }
		
    }
}
