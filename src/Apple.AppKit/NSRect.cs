using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public enum NSRectEdge {
		NSMinXEdge = 0,
		NSMinYEdge = 1,
		NSMaxXEdge = 2,
		NSMaxYEdge = 3
	}

	public struct NSRect {
		public NSPoint origin;
		public NSSize size;

		public NSRect(float x, float y, float w, float h) {
			origin = new NSPoint(x, y);
			size = new NSSize(w, h);
		}
	}
}
