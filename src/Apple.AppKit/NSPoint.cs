using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public struct NSPoint {
		public float x;
		public float y;

		public NSPoint(float x, float y) { this.x = x; this.y = y; }
	}
}
