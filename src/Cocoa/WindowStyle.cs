using System;

namespace Cocoa {
	[Flags()]
	public enum WindowStyle {
		Borderless           = 0,
		Titled               = 1 << 0,
		Closable             = 1 << 1,
		Miniaturizable       = 1 << 2,
		Resizable            = 1 << 3,
		TexturizedBackground = 1 << 8
	}
}
