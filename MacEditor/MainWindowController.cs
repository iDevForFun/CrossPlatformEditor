
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using CrossPlatformLogic;
using System.IO;
using System.Drawing;
using CrossPlatformLogic.Network;

namespace MacEditor
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		private INetworkClient client;


		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
			client = new NetworkClient ();
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		partial void Click_Button(NSObject sender)
		{
			MessageLabel.StringValue = string.Empty;

			var openPanel = new NSOpenPanel();
			openPanel.ReleasedWhenClosed = true;
			openPanel.Prompt = "Select file";

			var result = openPanel.RunModal();
			if (result == 1)
			{
				var filePath = openPanel.Url;
				var loader = new ImageLoader();

				try
				{
					var image = loader.LoadImage(filePath.FilePathUrl.Path) as Bitmap;
					var nsImage = ConvertFromImage(image);

					ImageView.Image = nsImage;
					FlipBtn.Enabled = true;
					ListenBtn.Enabled = true;
					client.ReportLoaded(filePath.FilePathUrl.Path);

				}
				catch(Exception ex)
				{
					MessageLabel.StringValue = string.Format("Error: {0}", ex.Message);
				}

			}

		}

		partial void Click_Flip(NSObject sender)
		{
			Flip();
		}

		partial void Click_Listen(NSObject sender)
		{
			ListenBtn.Enabled = false;
			client.OnNetworkEvent().Subscribe (x => { 

				if (x.Type == EventType.Flip){
						this.InvokeOnMainThread(() => 
						{
							Flip();
						});
				}
			});
		}

		private void Flip()
		{
			var nsImg = ImageView.Image;
			var bitmap = ConvertToImage(nsImg);
			var loader = new ImageLoader ();

			bitmap = loader.FlipHorizontal ((Image)bitmap) as Bitmap;

			ImageView.Image = ConvertFromImage (bitmap);
			client.ReportFlip ();

		}

		private NSImage ConvertFromImage(Bitmap img)
		{
			using (var stream = new MemoryStream ()) {

				img.Save (stream, System.Drawing.Imaging.ImageFormat.Bmp);
				stream.Position = 0;

				var data = NSData.FromArray(stream.ToArray());
				var nsImg = new NSImage (data);
				return nsImg;
			}
		}

		private Bitmap ConvertToImage(NSImage img)
		{
			var data = img.AsTiff();
			var imgRep = NSBitmapImageRep.ImageRepFromData(data) as NSBitmapImageRep;
			var imageProps = new NSDictionary();
			var imgData = imgRep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png, imageProps);
			return Bitmap.FromStream(imgData.AsStream()) as Bitmap;

		}

	}
}

