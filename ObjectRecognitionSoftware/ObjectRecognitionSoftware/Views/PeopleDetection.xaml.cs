using Emgu.TF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Emgu.TF.Models;
using Microsoft.Win32;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Drawing.Imaging;
using ObjectRecognitionSoftware.Views.DialogBoxes;
using ObjectRecognitionSoftware.Entities;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class PeopleDetection : Page, IResourceItemEntity
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        private LoadingDialogBox loadingDialogBox;
        
        public PeopleDetection()
        {
            InitializeComponent();
            TfInvoke.CheckLibraryLoaded();
            loadingDialogBox = new LoadingDialogBox();
        }
        
        public string Name
        {
            get { return "People Detection"; }
        }

        public Page Page
        {
            get { return this; }
        }

        private void DetectImage(string fileName)
        {
            MultiboxGraph graph = new MultiboxGraph();
            Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 224, 224, 128.0f, 1.0f / 128.0f);
            MultiboxGraph.Result result = graph.Detect(imageTensor);

            Bitmap bmp = new Bitmap(fileName);
            MultiboxGraph.DrawResults(bmp, result, 0.1f);

            ChooseImage1.Source = ConvertBitmap(bmp);
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

        private void ChooseImageButton1_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.FileName;
            ChooseImageLabel1.Text = filename;
            if (!string.IsNullOrEmpty(filename))
            {
                loadingDialogBox.Show();
                DetectImage(filename);
                loadingDialogBox.Hide();
            }            
        }
    }
}
