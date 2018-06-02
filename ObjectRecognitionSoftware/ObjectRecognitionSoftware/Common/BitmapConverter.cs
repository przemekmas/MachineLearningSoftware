using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ObjectRecognitionSoftware.Common
{
    public static class BitmapConverter
    {
        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        public static BitmapImage ConvertBitmap(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}