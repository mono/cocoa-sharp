using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa 
{
	public class ColorWell : Control
    {
        
		private static string ObjectiveCName = "NSColorWell";   
        

        public ColorWell () 
            : base () 
        {
        }
        
		public ColorWell (Rect frame) 
            : base (frame) 
        {
        } 
        
		public ColorWell (IntPtr native_object) 
            : base (native_object) 
        {
        }
        
        public Color Color
        {
            get{        
                return (Color)Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "color", typeof (IntPtr)));
            }
            set{
                ObjCMessaging.objc_msgSend (NativeObject, "setColor:", typeof (void), typeof (IntPtr), value.NativeObject);
            }
        }
        
        public bool DragColor( Color aColor, Event aEvent, View aView )
        {
            return (bool) ObjCMessaging.objc_msgSend(NativeObject, "dragColor:withEvent:fromView:",
                typeof (System.Boolean),                 // return value
                typeof (IntPtr), aColor.NativeObject,    // dragColor:
                typeof (IntPtr), aEvent.NativeObject,    // withEvent:
                typeof (IntPtr), aView.NativeObject );  // fromView:
        }
        
        public void Activate( bool aExclusive )
        {
            ObjCMessaging.objc_msgSend (NativeObject, "activate;", typeof (void), typeof (int), aExclusive ? 1 : 0 );
        }
        
    }
}
