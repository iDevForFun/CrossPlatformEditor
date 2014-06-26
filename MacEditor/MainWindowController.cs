
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using CrossPlatformLogic;
using System.IO;
using System.Drawing;

namespace MacEditor
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
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

			//var filePath = LabelField.StringValue;
			var filePath = "/Volumes/DATA/Photos/AAA_0727.jpg";
			if(string.IsNullOrEmpty(filePath)) return;

			var loader = new ImageLoader();

			try
			{
				var image = loader.LoadImage(filePath);
				var nsImage = ConvertFromImage(image);

				ImageView.Image = nsImage;

			}
			catch(Exception ex)
			{
				MessageLabel.StringValue = string.Format("Error: {0}", ex.Message);
			}
				
		}

		partial void Click_Flip(NSObject sender)
		{
			var nsImg = ImageView.Image;
			var bitmap = ConvertToImage(nsImg);
			var loader = new ImageLoader ();

			bitmap = loader.FlipHorizontal (bitmap);

			ImageView.Image = ConvertFromImage (bitmap);


		}

		private NSImage ConvertFromImage(Image img)
		{
			using (var stream = new MemoryStream ()) {

				img.Save (stream, System.Drawing.Imaging.ImageFormat.Png);
				stream.Position = 0;

				var data = NSData.FromStream(stream);
				var nsImg = new NSImage (data);
				return nsImg;
			}
		}

		private Image ConvertToImage(NSImage img)
		{
			var data = img.AsTiff();
			var imgRep = NSBitmapImageRep.ImageRepFromData(data) as NSBitmapImageRep;
			var imageProps = new NSDictionary();
			var imgData = imgRep.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png, imageProps);
			return Image.FromStream(imgData.AsStream());

		}

	}
}

