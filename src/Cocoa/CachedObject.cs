using System;

namespace Cocoa {
	internal struct CachedObject {
		internal WeakReference Reference;
		internal Type Type;

		internal CachedObject (WeakReference objref, Type type) {
			Reference = objref;
			Type = type;
		}
	}
}
