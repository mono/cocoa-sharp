using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public struct Point {
		public float X;
		public float Y;
        
		public Point (float x, float y) {
			X = x;
			Y = y;
		}
        
        public override bool Equals( object aObject )
        {
            if( aObject == null )
            {
                return false;
            }
            
            if( !(aObject is Point) )
            {
                return false;
            }
            
            
            Point p2 = (Point)aObject;
            
            /// We should really use a threshold difference here...
            ///
            return X == p2.X && Y == p2.Y;
        }
        
        public bool Equals( Point p2 )
        {
            if( (object)p2 == null )
            {
                return false;
            }
            
            /// Because we are dealing with floating points...we cannot perfrom
            /// a straight comparison..but must apply a threshold comparison.
            ///
            if( !(Math.Abs( X - p2.X ) < .001) )
            {
                return false;
            }
            
            if( !(Math.Abs( Y - p2.Y ) < .001) )
            {
                return false;
            }
            
            
            return true;
        }
        
        public override int GetHashCode()
        {
            return (int)X ^ (int)Y;
        }
        
        public static bool operator == ( Point p1, Point p2)
        {
            if( System.Object.ReferenceEquals( p1, p2 ) )
            {
                return true;
            }
            
            if( (object)p1 == null || (object)p2 == null )
            {
                return false;
            }
            
            return p1.Equals( p2 );
        }
        
        public static bool operator != ( Point p1, Point p2)
        {
            return !(p1 == p2);
        }
        
    }
}
