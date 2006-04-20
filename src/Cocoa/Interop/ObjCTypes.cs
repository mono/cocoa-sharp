using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cocoa {

	public class ObjCTypes {

		public static string FromType (Type type, out int size) {
			if (type == typeof (char)) {
				size = Marshal.SizeOf (typeof (char));
				return "c";
			}
			if (type == typeof (Int32)) {
				size = Marshal.SizeOf (typeof (Int32));
				return "i";
			}
			if (type == typeof (short)) {
				size = Marshal.SizeOf (typeof (short));
				return "s";
			}
			if (type == typeof (long)) {
				size = Marshal.SizeOf (typeof (long));
				return "l";
			}
			if (type == typeof (Int64)) {
				size = Marshal.SizeOf (typeof (Int64));
				return "q";
			}
			if (type == typeof (UInt32)) {
				size = Marshal.SizeOf (typeof (UInt32));
				return "I";
			}
			if (type == typeof (ushort)) {
				size = Marshal.SizeOf (typeof (ushort));
				return "S";
			}
			if (type == typeof (ulong)) {
				size = Marshal.SizeOf (typeof (ulong));
				return "L";
			}
			if (type == typeof (UInt64)) {
				size = Marshal.SizeOf (typeof (UInt64));
				return "Q";
			}
			if (type == typeof (float)) {
				size = Marshal.SizeOf (typeof (float));
				return "f";
			}
			if (type == typeof (double)) {
				size = Marshal.SizeOf (typeof (double));
				return "d";
			}
			if (type == typeof (bool)) {
				size = Marshal.SizeOf (typeof (bool));
				return "B";
			}
			if (type == typeof (string)) {
				size = Marshal.SizeOf (typeof (IntPtr));
				return "@";
			}
			if (type == typeof (void)) {
				size = 0;
				return "v";
			}
			size = 4;
			return "@";
		}
	}
}
