
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
		private bool editModeOn;
		private bool imageLoaded; 

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
			editModeOn = false;
			imageLoaded = false;
		}

		#endregion

		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		public override void WindowDidLoad()
		{
			base.WindowDidLoad ();

			var images = new ImageLoader ().Images;
					
			foreach (var item in images.Select (x => new NSString (x))) {
				ImageDropDown.Add (item);
			}

			if(images.Any()) ImageDropDown.Select (new NSString(images.First()));

			client.OnNetworkEvent().Subscribe (x => { 

				if (x.Type == EventType.Flip){
					this.InvokeOnMainThread(() => 
						{
							Flip(false);
						});
				}
			});

			client.OnNetworkEvent().Subscribe (x => { 

				if (x.Type == EventType.Rotate){
					this.InvokeOnMainThread(() => 
						{
							Rotate(false);
						});
				}
			});

			client.OnNetworkEvent().Subscribe (x => { 

				if (x.Type == EventType.Lock){
					this.InvokeOnMainThread(() => 
						{
							editModeOn = !editModeOn;
							SetEditMode(editModeOn, false);
						});
				}
			});

			client.OnNetworkEvent ().Subscribe (x => {
				if (x.Type == EventType.Loaded){
					this.InvokeOnMainThread(() => 
						{
							LoadImage(x.Data, false);
						});
				}
			});

		}

		partial void Click_Button(NSObject sender)
		{
			var fileName = ImageDropDown.StringValue.ToString();
			LoadImage(fileName, true);
		}

		partial void Click_Flip(NSObject sender)
		{
			Flip(true);
		}

		partial void Click_Rotate(NSObject sender){

			Rotate(true);
		}

		partial void Click_Edit(NSObject sender)
		{
			editModeOn = !editModeOn;

			ImageDropDown.Enabled = EditCheckBox.State == NSCellStateValue.On;
			SelectBtn.Enabled = EditCheckBox.State == NSCellStateValue.On;

			if (imageLoaded) {

				FlipBtn.Enabled = EditCheckBox.State == NSCellStateValue.On;
				RotateBtn.Enabled = EditCheckBox.State == NSCellStateValue.On;
			}

			client.ReportLock();
		}

		private void LoadImage(string fileName, bool report)
		{

			MessageLabel.StringValue = string.Empty;
			var loader = new ImageLoader();

			try
			{
				var image = loader.LoadImage(fileName) as Bitmap;
				var nsImage = ConvertFromImage(image);

				ImageView.Image = nsImage;
				if(EditCheckBox.State == NSCellStateValue.On)
				{
					FlipBtn.Enabled = true;
					RotateBtn.Enabled = true;
				}
			
				if(report) client.ReportLoaded(fileName);
				imageLoaded = true;
			}
			catch(Exception ex)
			{
				MessageLabel.StringValue = string.Format("Error: {0}", ex.Message);
			}
		}

		private void SetEditMode(bool editable, bool report)
		{
			editModeOn = editable;
			if (EditCheckBox.State == NSCellStateValue.On) {
				FlipBtn.Enabled = false;
				RotateBtn.Enabled = false;
				ImageDropDown.Enabled = false;
				SelectBtn.Enabled = false;
			}

			if (!editable) EditCheckBox.State = NSCellStateValue.Off;
			EditCheckBox.Enabled = editable;


		}

		private void Flip(bool report)
		{
			var nsImg = ImageView.Image;
			var bitmap = ConvertToImage(nsImg);
			var loader = new ImageLoader ();

			bitmap = loader.FlipHorizontal ((Image)bitmap) as Bitmap;

			ImageView.Image = ConvertFromImage (bitmap);
			if(report) client.ReportFlip ();

		}

		private void Rotate(bool report)
		{
			var nsImg = ImageView.Image;
			var bitmap = ConvertToImage(nsImg);
			var loader = new ImageLoader ();

			bitmap = loader.Rotate ((Image)bitmap) as Bitmap;

			ImageView.Image = ConvertFromImage (bitmap);
			if(report) client.ReportRotate ();
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

