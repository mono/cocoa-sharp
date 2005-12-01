using System;
using Cocoa;

[Register ("MyView")]
public class Controller : View {

	[Connect] private TextField filename;
	[Connect] private ImageView iconwell;

	[Export]
	public Controller (Rect frame) : base (frame) {
		RegisterDragType (Pasteboard.Filenames);
	}

	public Controller (IntPtr native_object) : base (native_object) {}

	[Export ("draggingEntered:")]
	public int DraggingEntered (DragDestination sender) {
		if (ValidateDrag (sender) != null)
			return (int)DragOperation.Copy;	
		return (int)DragOperation.None;
	}

	[Export ("draggingUpdated:")]
	public int DraggingUpdated (DragDestination sender) {
		if (ValidateDrag (sender) != null)
			return (int)DragOperation.Copy;	
		return (int)DragOperation.None;
	}
	
	[Export ("draggingExited:")]
	public void DraggingExited (DragDestination sender) {
		// nop
	}

	[Export ("prepareForDragOperation:")]
	public bool PrepareForDrag (DragDestination sender) {
		if (ValidateDrag (sender) != null)
			return true;
		return false;
	}

	[Export ("performDragOperation:")]
	public bool PerformDrag (DragDestination sender) {
		if (ValidateDrag (sender) != null)
			return true;
		return false;
	}

	[Export ("concludeDragOperation:")]
	public void ConcludeDrag (DragDestination sender) {
		string type = ValidateDrag (sender);

		if (type != null) {
			Pasteboard pb = sender.Pasteboard;

			string [] list = pb.ListForType (type);
			filename.Value = list [0];

			FileWrapper file = new FileWrapper (list [0]);

			iconwell.Image = file.Icon;
		} else {
			Console.WriteLine (".");
		}
	}

	private string ValidateDrag (DragDestination sender) {
		if (sender.Source != this) {
			Pasteboard pb = sender.Pasteboard;
			if (pb.Types.Length > 0) {
				foreach  (string type in pb.Types) {
					if (type.Equals (Pasteboard.Filenames)) {
						return type;
					}
				}
			}
		}
		return null;
	}
}

public class MyApplication {
	static void Main (string [] args) {
		Application.Init ();
		Application.LoadNib ("DragNDrop.nib");
		Application.Run ();
	}
} 
