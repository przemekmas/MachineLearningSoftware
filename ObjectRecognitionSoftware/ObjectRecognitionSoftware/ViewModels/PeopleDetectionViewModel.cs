using Emgu.TF.Models;
using ObjectRecognitionSoftware.Entities;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class PeopleDetectionViewModel : NotifyPropertyChanged
    {
        private BitmapImage m_ImageSource;
        private string m_ImageSourceDirectory;
        private bool m_IsModalVisible;

        public BitmapImage ImageSource
        {
            get { return m_ImageSource; }
            set
            {
                m_ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public string ImageSourceDirectory
        {
            get { return m_ImageSourceDirectory; }
            set
            {
                m_ImageSourceDirectory = value;
                OnPropertyChanged(nameof(ImageSourceDirectory));
            }
        }

        public bool IsModalVisible
        {
            get { return m_IsModalVisible; }
            set
            {
                m_IsModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        public void ChooseImage()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.FileName;
            ImageSourceDirectory = filename;
            if (!string.IsNullOrEmpty(filename))
            {
                IsModalVisible = true;
                Task.Run(() => DetectImage(filename));
            }
        }

        private void DetectImage(string fileName)
        {
            var graph = new MultiboxGraph();
            var imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 224, 224, 128.0f, 1.0f / 128.0f);
            var result = graph.Detect(imageTensor);

            var bmp = new Bitmap(fileName);
            MultiboxGraph.DrawResults(bmp, result, 0.1f);
            ImageSource = ConvertBitmap(bmp);
            IsModalVisible = false;
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
    }
}
