using System;

namespace Apple.Cocoa.Foundation
{
	public struct NSRect {
		public NSPoint origin;
		public NSSize size;

		public NSRect(float x, float y, float w, float h) {
			origin = new NSPoint(x, y);
			size = new NSSize(w, h);
		}
	}
}
