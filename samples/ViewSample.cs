using System;
using System.Drawing;
using System.Runtime.InteropServices;

using Cocoa;

namespace CocoaSharp.Samples {

	public class OpenGLViewSample {

		public static void Main (string[] args) {
			Application.Init ();
			Application.LoadNib ("View.nib");

			Application.Run ();
		}

	}

	[Register ("Controller")]
	public class Controller : Cocoa.Object {

		[Connect] public SimpleView itsView;

		[Export ("windowDidResize:")]
		public void WindowDidResize (Notification notification) {
			itsView.Invalidate (itsView.Bounds);
		}

		public Controller (IntPtr raw) : base(raw) { }
	}

	[Register ("SimpleView")]
	public class SimpleView : View {

		public SimpleView (IntPtr raw) : base(raw) { }

		[Export ("initWithFrame:")]
		public SimpleView (Rect aRect) : base (aRect) {}

		[Export ("drawRect:")]
		public void Draw (Rect aRect) {
#if SYSD
			Graphics g = Graphics.FromHwnd (this.NativeObject);
			Rectangle r = new Rectangle ((int)this.Bounds.Origin.X, (int)this.Bounds.Origin.Y, (int)this.Bounds.Size.Width, (int)this.Bounds.Size.Height);
			Brush b = new SolidBrush (System.Drawing.Color.Red);
			g.FillRectangle (b, r);
			System.Drawing.Font f = new System.Drawing.Font ("Times New Roman", (int)(this.Bounds.Size.Height/15));
			b = new SolidBrush (System.Drawing.Color.White);
			g.DrawString ("This is System.Drawing Text\non a g.FillRectangle background!\nTry Resizing the Window!", f, b, 10, 10);
#else
			BezierPath.FillRect (this.Bounds);
			Graphics g = Graphics.FromHwnd (this.NativeObject);
			System.Drawing.Font f = new System.Drawing.Font ("Times New Roman", (int)(this.Bounds.Size.Height/15));
			Brush b = new SolidBrush (System.Drawing.Color.White);
			g.DrawString ("This is System.Drawing Text\non a NSBezierPath background!\nTry Resizing the Window!", f, b, 10, 10);
#endif
		}
	}
}
