using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace AlbumCatalog.Utilities
{
    public class Thumbnailer
    {
        public static Stream Create(Stream input, int maxSize)
        {
            var orig = new Bitmap(input);

            int width;
            int height;
            if (orig.Width > orig.Height)
            {
                width = Math.Min(maxSize, orig.Width);
                height = width * orig.Height / orig.Width;
            }
            else
            {
                height = Math.Min(maxSize, orig.Height);
                width = height * orig.Width / orig.Height;
            }

            var thumb = new Bitmap(width, height);
            using (Graphics graphic = Graphics.FromImage(thumb))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.AntiAlias;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphic.DrawImage(orig, 0, 0, width, height);
                var ms = new MemoryStream();
                thumb.Save(ms, ImageFormat.Jpeg);

                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
        }
    }
}