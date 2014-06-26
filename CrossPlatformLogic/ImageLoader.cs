using System;
using System.Drawing;
using System.IO;

namespace CrossPlatformLogic
{
    public class ImageLoader
    {
        public Image LoadImage(string filePath)
        {
            if(!File.Exists(filePath)) throw new FileNotFoundException("No file found at this path", filePath);
            if (!filePath.EndsWith(".jpg") && !filePath.EndsWith(".jpeg") && !filePath.EndsWith(".png")) throw new NotSupportedException("png of jpeg files only");

            var image = new Bitmap(filePath);
            return image;
        }

        public Image FlipHorizontal(Image image)
        {
            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return image;
        }
    }
}
