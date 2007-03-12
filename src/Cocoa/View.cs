using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
    
	public class View : Responder 
    {
		private static string ObjectiveCName = "NSView";
        
		public View () 
            : base ()
        {
        }

		public View (IntPtr native_object) 
            : base (native_object) 
        {
        }

		public View (Rect frame) : base () {
			if (this.GetType ().IsSubclassOf (typeof (View))) 
				NativeObject = (IntPtr) ObjCMessaging.objc_msgSendSuper (NativeObject, "initWithFrame:", typeof (IntPtr), typeof (Rect), frame);
			else
				throw new ArgumentException ("initWithFrame: directly on NSView is unsupported");
		}
		
		public Rect Bounds {
			get {
				return (Rect)ObjCMessaging.objc_msgSend (NativeObject, "bounds", typeof (Rect));
			}
		}
        
        /// <summary>
        /// Indicates if the view is utilizing the native or flipped coordinate system.
        /// </summary>
        public bool IsFlipped
        {
            get
            {
                return (bool) ObjCMessaging.objc_msgSend(NativeObject, "isFlipped", typeof(System.Boolean));
            }
        }
        
        /// <summary>
        /// Indicates if the view is considered hidden.
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return (bool) ObjCMessaging.objc_msgSend(NativeObject, "isHidden", typeof(System.Boolean));
            }
        }
        
        /// <summary>
        /// Indicates if the view or any of this ancestors in the view heirarchy are hidden.
        /// </summary>
        public bool IsHiddenOrHasHiddenAncestor
        {
            get
            {
                return (bool) ObjCMessaging.objc_msgSend(NativeObject, "isHidden", typeof(System.Boolean));
            }
        }
        

		public void Display () {
			ObjCMessaging.objc_msgSend (NativeObject, "display", typeof (void));
		}

		public void AddSubview (View view) {
			ObjCMessaging.objc_msgSend (NativeObject, "addSubview:", typeof (void), typeof (IntPtr), view.NativeObject);
		}

		public void RemoveFromSuperview (bool update) {
			ObjCMessaging.objc_msgSend (NativeObject, (update) ? "removeFromSuperview" : "removeFromSuperviewWithoutNeedingDisplay", typeof (void));
		}

		public void Invalidate (Rect bounds) {
			ObjCMessaging.objc_msgSend (NativeObject, "invalidate:", typeof (void), typeof (Rect), bounds);
		}

		public void RegisterDragType (string type) {
			RegisterDragTypes (new string [] {type});
		}

		public void RegisterDragTypes (string [] types) {
			MutableArray array = new MutableArray ();
			foreach (string t in types) {
				array.Add (new Cocoa.String (t));
			}

			ObjCMessaging.objc_msgSend (NativeObject, "registerForDraggedTypes:", typeof (void), typeof (IntPtr), array.NativeObject);
		}
		
		public Window Window {
			get {
				return (Window)Object.FromIntPtr((IntPtr)ObjCMessaging.objc_msgSend (NativeObject, "window", typeof (IntPtr)));
			}
		}
        
        /// <summary>
        ///
        /// </summary>
        /// <param name"aRect"></param>
        public void NeedsDisplay( Rect aRect )
        {
            ObjCMessaging.objc_msgSend (NativeObject, "setNeedsDisplayInRect:", typeof (void), typeof (Rect), aRect);
        }
        
                
        /// <summary>
        ///
        /// </summary>
        /// <param name"aPoint"></param>
        /// <oaran name="aView"></param>
        /// <return></return>
        public Point ConvertPointFromView( Point aPoint, View aView )
        {
            return (Point)ObjCMessaging.objc_msgSend (
                NativeObject, "convertPoint:fromView:", 
                typeof (Point),                            // return value type
                typeof (Point), aPoint,                    // 1st arg
                typeof (IntPtr), aView.NativeObject );     // 2nd arg
            
        }
        
        /// <summary>
        /// Override to specify custom drawing primitives.
        /// </summary>
        /// <param name"aRect">The view's display area.</param>
        [Export ("drawRect:")]
        public virtual void OnDrawRect (Rect aRect) 
        {		
        }
        
        
        /// <summary>
        /// Handles the mouse down event.
        /// </summary>
        /// <param name"aEvent"></param>
        [Export ("mouseDown:")]
        public virtual void OnMouseDown( Event aEvent )
        {
        }
        
        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <remarks>
        ///  The AcceptsFirstResponder property must be overridden to return true for this handler to be activated.
        /// </remarks>
        /// <param name"aEvent"></param>
        [Export ("keyDown:")]
        public virtual void OnKeyDown( Event aEvent )
        {
            if( aEvent == null )
            {
                return;
            }
            
            switch( aEvent.KeyCode )
            {
                case 115:
                    OnKeyDownHome();
                    break;
                    
                case 116:
                    OnKeyDownPageUp();
                    break;
                    
                case 117:
                    OnKeyDownDelete();
                    break;
                    
                case 119:
                    OnKeyDownEnd();
                    break;
                    
                case 121:
                    OnKeyDownPageDown();
                    break;
                    
                case 123:
                    OnKeyDownArrowLeft();
                    break;
                    
                case 124:
                    OnKeyDownArrowRight();
                    break;
                    
                case 125:
                    OnKeyDownArrowDown();
                    break;
                    
                case 126:
                    OnKeyDownArrowUp();
                    break;
            }
        }
        
        public virtual void OnKeyDownArrowDown()
        {
        }
        
        public virtual void OnKeyDownArrowUp()
        {
        }
        
        public virtual void OnKeyDownArrowLeft()
        {
        }
        
        public virtual void OnKeyDownArrowRight()
        {
        }
        
        public virtual void OnKeyDownHome()
        {
        }
        
        public virtual void OnKeyDownPageUp()
        {
        }
        
        public virtual void OnKeyDownPageDown()
        {
        }
        
        public virtual void OnKeyDownEnd()
        {
        }
        
        public virtual void OnKeyDownDelete()
        {
        }
        
        /// <summary>
        /// Indicates if the view or any of this ancestors in the view heirarchy are hidden.
        /// </summary>
        [Export ("acceptsFirstResponder")]
        public virtual bool AcceptsFirstResponder()
        {
            return false;
        }
        
	}
}
