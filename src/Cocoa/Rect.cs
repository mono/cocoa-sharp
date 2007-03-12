using System;
using Cocoa;

namespace Cocoa {
    
	public struct Rect 
    {
		public Point Origin;
		public Size Size;
        
		public Rect (Point origin, Size size) {
			this.Origin = origin;
			this.Size = size;
		}
        
		public Rect (float x, float y, float width, float height) {
			this.Origin = new Point (x, y);
			this.Size = new Size (width, height);
		}
        
        public bool Contains( Point aPoint )
        {          
            if( aPoint.X >= Origin.X && aPoint.X <= (Origin.X + Size.Width ) )
            {
                if( aPoint.Y >= Origin.Y && aPoint.Y <= (Origin.Y + Size.Height ) )
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public override string ToString()
        {
            return System.String.Format( "x: {0}, y: {1}, width: {2}, heigth: {3}",
                                         Origin.X, Origin.Y, Size.Width, Size.Height );
        }
	}
    
    
}
