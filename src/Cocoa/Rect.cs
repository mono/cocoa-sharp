using Cocoa;

namespace Cocoa {
	public struct Rect {
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
	}
}
