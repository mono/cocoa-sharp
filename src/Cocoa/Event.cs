using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa 
{
	public class Event : Cocoa.Object 
    {
        
		private static string ObjectiveCName = "NSEvent"; 
        
        
		public Event (IntPtr native_object) 
            : base (native_object)
        {
        }
        
        /// <summary>
        /// The event's location.
        /// </summary>
        /// <remarks>
        /// With MouseMoved and other events, where the window may be null,
        /// the property returns the locaiton in the scree coordiates.
        /// </remarks>
        /// <returns>The location is in the base coordinate system of the associated window.</returns>
        public Point LocationInWindow 
        {
            get{
				return (Point)ObjCMessaging.objc_msgSend (NativeObject, "locationInWindow", typeof (Point));
			}
		}    
        
        public uint ModifierFlag
        {
            get{
                return (uint)ObjCMessaging.objc_msgSend (NativeObject, "modifierFlags", typeof (uint));
            }
        }
        
        public uint PointingDeviceId
        {
            get{
                return (uint)ObjCMessaging.objc_msgSend (NativeObject, "pointingDeviceId", typeof (uint));
            }
        }
        
        public uint PointingDeviceSerialNumber
        {
            get{
                return (uint)ObjCMessaging.objc_msgSend (NativeObject, "pointingDeviceSerialNumber", typeof (uint));
            }
        }
        
        public float Pressure
        {
            get{
                return (float)ObjCMessaging.objc_msgSend (NativeObject, "pressure", typeof (float));
            }
        }
        
        public float Rotation
        {
            get{
                return (float)ObjCMessaging.objc_msgSend (NativeObject, "rotation", typeof (float));
            }
        }
        
        public short SubType
        {
            get{
                return (short)ObjCMessaging.objc_msgSend (NativeObject, "subtype", typeof (short));
            }
        }
        
        public int ClickCount
        {
            get{
                return (int)ObjCMessaging.objc_msgSend (NativeObject, "clickCount", typeof (int));
            }
        }
        
        public string Characters
        {
            get{
                return Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "characters", typeof (IntPtr))).ToString ();
            }
        }
        
        public string CharactersIgnoringModifiers
        {
            get{
                return Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "charactersIgnoringModifiers", typeof (IntPtr))).ToString ();
            }
        }
        
        public ushort KeyCode
        {
            get{
                return (ushort)ObjCMessaging.objc_msgSend (NativeObject, "keyCode", typeof (ushort));
            }
        }
        
        
    }
}
