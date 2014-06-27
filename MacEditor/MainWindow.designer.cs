// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;
using System.CodeDom.Compiler;

namespace MacEditor
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSButton EditCheckBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton FlipBtn { get; set; }

		[Outlet]
		MonoMac.AppKit.NSComboBox ImageDropDown { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ImageView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView Logo { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField MessageLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton RotateBtn { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton SelectBtn { get; set; }

		[Action ("Click_Button:")]
		partial void Click_Button (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Edit:")]
		partial void Click_Edit (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Flip:")]
		partial void Click_Flip (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Listen:")]
		partial void Click_Listen (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Rotate:")]
		partial void Click_Rotate (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FlipBtn != null) {
				FlipBtn.Dispose ();
				FlipBtn = null;
			}

			if (SelectBtn != null) {
				SelectBtn.Dispose ();
				SelectBtn = null;
			}

			if (EditCheckBox != null) {
				EditCheckBox.Dispose ();
				EditCheckBox = null;
			}

			if (ImageDropDown != null) {
				ImageDropDown.Dispose ();
				ImageDropDown = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (Logo != null) {
				Logo.Dispose ();
				Logo = null;
			}

			if (MessageLabel != null) {
				MessageLabel.Dispose ();
				MessageLabel = null;
			}

			if (RotateBtn != null) {
				RotateBtn.Dispose ();
				RotateBtn = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
