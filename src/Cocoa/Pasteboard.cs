using System;
using System.Runtime.InteropServices;
using Cocoa;

namespace Cocoa {
	public class Pasteboard : Cocoa.Object {
		private static string ObjectiveCName = "NSPasteboard";

		public const string String = "NSStringPboardType";
		public const string Filenames = "NSFilenamesPboardType";
		public const string PostScript = "NSPostScriptPboardType";
		public const string TIFF = "NSTIFFPboardType";
		public const string RTF = "NSRTFPboardType";
		public const string TabularText = "NSTabularTextPboardType";
		public const string Font = "NSFontPboardType";
		public const string Ruler = "NSRulerPboardType";
		public const string FileContents = "NSFileContentsPboardType";
		public const string Color = "NSColorPboardType";
		public const string RTFD = "NSRTFDPboardType";
		public const string HTML = "NSHTMLPboardType";
		public const string PICT = "NSPICTPboardType";
		public const string URL = "NSURLPboardType";
		public const string PDF = "NSPDFPboardType";
		public const string VCard = "NSVCardPboardType";
		public const string FilesPromise = "NSFilesPromisePboardType";
		public const string InkText = "NSInkTextPboardType";

		public Pasteboard () : base () {}

		public Pasteboard (IntPtr native_object) : base (native_object) {}

		public string [] ListForType (string type) {
				return (string []) ((Array) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "propertyListForType:", typeof (IntPtr), typeof (IntPtr), new Cocoa.String (type).NativeObject))).ToArray ();
		}

		public string [] Types {
			get {
				return (string []) ((Array) Object.FromIntPtr ((IntPtr) ObjCMessaging.objc_msgSend (NativeObject, "types", typeof (IntPtr)))).ToArray ();
			}
		}
	}
}
