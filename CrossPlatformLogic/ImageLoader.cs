using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Net;


namespace CrossPlatformLogic
{
    public class ImageLoader
    {
		public IEnumerable<string> Images { get; set; }
		private const string baseAddress = "http://10.211.55.5/ImageServer";

		public ImageLoader()
		{
			Images = new List<string>
			{
				"RSV4-1.jpg",
				"S1000RR-1.jpg",
				"ZX12R-1.jpg", 
				"RGV500.jpg"
			};
		}

		public Image LoadImage(string fileName)
		{ 
			var location = string.Format ("{0}/{1}", baseAddress, fileName);
            
            var request = (HttpWebRequest)WebRequest.Create(location);
            request.ContentType = string.Format("image/{0}", fileName.Split('.')[1]);
            request.Method = "GET";

		    Bitmap image = null;

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                image = new Bitmap(stream);
            }
			
            return image;
        }

		public Image FlipHorizontal(Image image)
        {
            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return image;
        }
    }
}
