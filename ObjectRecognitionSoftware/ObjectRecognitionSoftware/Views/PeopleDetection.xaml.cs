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
using System.Threading;
using System.Windows.Shapes;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class PeopleDetection : Page, IResourceItemEntity
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        
        public PeopleDetection()
        {
            InitializeComponent();
            TfInvoke.CheckLibraryLoaded();
        }

        public string Name => "People Detection";

        public Page Page => this;

        public Control IconControl => new PeopleDetectionIcon();

        private void DetectImage(string fileName)
        {
            var graph = new MultiboxGraph();
            var imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 224, 224, 128.0f, 1.0f / 128.0f);
            var result = graph.Detect(imageTensor);

            var bmp = new Bitmap(fileName);
            MultiboxGraph.DrawResults(bmp, result, 0.1f);
            
            Dispatcher.Invoke(() =>
            {
                ChooseImage1.Source = ConvertBitmap(bmp);
                LoadingModalDialog.HideModal();
            });
        }

        private BitmapImage ConvertBitmap(Bitmap bitmap)
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
                LoadingModalDialog.ShowModal();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    DetectImage(filename);                    
                }).Start();
            }            
        }      
    }
}
