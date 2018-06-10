using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MachineLearningSoftware.Common
{
    public static class BitmapConverter
    {
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
        {
            var gdiBitmap = bitmap.GetHbitmap();

            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            var bitmapImage = Imaging.CreateBitmapSourceFromHBitmap(
                gdiBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(gdiBitmap);
            return bitmapImage;
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
                memory.Dispose();

                return bitmapImage;
            }
        }
    }
}