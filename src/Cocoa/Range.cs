
using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa
{
	public struct Range
    {
        public uint location;
        public uint length;
        
        public Range (uint location, uint length) 
        {
			this.location = location;
            this.length = length;
		}
        
    }
}

