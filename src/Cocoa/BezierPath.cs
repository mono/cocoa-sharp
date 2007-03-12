using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa 
{    
	public class BezierPath : Cocoa.Object 
    {
        enum LineCapStyle 
        {
            ButtLine = 0,
            RoundLine = 1,
            SquareLine = 2
        }

        enum LineJoinStyle
        {
            Miter = 0,
            Round = 1,
            Bevel = 2
        }

        enum WindingRule
        {
            NonZero = 0,
            EvenOdd = 1
        }

        enum BezierPathElement
        {
            MoveTo,
            LineTo,
            CurveTo,
            ClosePath
        }

        private static string ObjectiveCName = "NSBezierPath";       

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="native_object"></param>
        public BezierPath (IntPtr native_object) 
        : base (native_object)
        {
        }

        /// <summary>
        /// Fills the specified rectangular path with the current fill color.
        /// </summary>
        /// <remarks>
        /// This method fills the specified region immediately. This method uses the compositing
        /// operation returned by the compositingOperation method of NSGraphicsContext.
        /// </remarks>
        /// <param name="bounds">
        /// A rectangle in the current coordinate system.
        /// </param> 
        public static void FillRect (Rect bounds) 
        {
            ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (BezierPath)).ToIntPtr (), 
                                        "fillRect:", typeof (void), typeof (Rect), bounds);
        }

        /// <summary>
        /// Strokes the path of the specified rectangle using the current stroke color and the default drawing attributes
        /// </summary>
        /// <remarks>
        /// The path is drawn beginning at the rectangle’s origin and proceeding in a
        /// counterclockwise direction. This method strokes the specified path immediately.
        /// </remarks>
        /// <param name="bounds">
        /// A rectangle in the current coordinate system.
        /// </param> 
        public static void StrokeRect (Rect bounds) 
        {
            ObjCMessaging.objc_msgSend ((IntPtr) ObjCClass.FromType (typeof (BezierPath)).ToIntPtr (), 
                                        "strokeRect:", typeof (void), typeof (Rect), bounds);
        }
    }
}
