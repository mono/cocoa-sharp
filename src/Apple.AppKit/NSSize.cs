using System;
using Apple.Foundation;

namespace Apple.AppKit
{
	public struct NSSize {
		private float _width; /* should never be negative */
		private float _height; /* should never be negative */

		public float width {
			get {
				return _width;
			}
			set {
				if(value < 0)
					throw new InvalidOperationException("width < 0");
				_width = value;
			}
		}
		public float height {
			get {
				return _height;
			}
			set {
				if(value < 0)
					throw new InvalidOperationException("height < 0");
				_height = value;
			}
		}

		public NSSize(float w, float h) { _width = w; _height = h; }
	}
}
