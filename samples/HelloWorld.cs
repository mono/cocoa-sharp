using System;
using Cocoa;

namespace CocoaSharp.Samples {
	[Register ("ApplicationController")]
	public class HelloWorld : Cocoa.Object {

		[Connect]
		public TextField textBox1;

		public HelloWorld (IntPtr native_object) : base (native_object) {}

		[Export("applicationWillFinishLaunching:")]
		public void FinishLoading(Notification notification) {
			textBox1.Value = "Form Loaded";
		}

		[Export("buttonClick:")]
		public void buttonClick(Cocoa.Object sender) {
			textBox1.Value = "Button Pushed";
		}
	}

	public class WorldRunner {
		static void Main (string [] args) {
			Application.Init ();
			Application.LoadNib ("HelloWorld.nib");
			Application.SharedApplication.Icon = new Cocoa.Image ("applemono.icns");
			Application.Run ();
		}
    	} 
}
