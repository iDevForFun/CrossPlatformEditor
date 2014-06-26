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
		MonoMac.AppKit.NSTextField _MessageLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ImageView { get; set; }

		MonoMac.AppKit.NSButton FlipBtn { get; set; }

		[Outlet]
		MonoMac.AppKit.NSImageView ImageView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton ListenBtn { get; set; }


		[Outlet]
		MonoMac.AppKit.NSTextField MessageLabel { get; set; }

		[Action ("Click_Button:")]
		partial void Click_Button (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Flip:")]
		partial void Click_Flip (MonoMac.Foundation.NSObject sender);

		[Action ("Click_Listen:")]
		partial void Click_Listen (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{

			if (FlipBtn != null) {
				FlipBtn.Dispose ();
				FlipBtn = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}


			if (ListenBtn != null) {
				ListenBtn.Dispose ();
				ListenBtn = null;
			}

			if (MessageLabel != null) {
				MessageLabel.Dispose ();
				MessageLabel = null;
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
