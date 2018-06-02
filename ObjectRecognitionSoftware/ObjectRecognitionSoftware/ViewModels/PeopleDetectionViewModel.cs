using Emgu.TF.Models;
using ObjectRecognitionSoftware.Common;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class PeopleDetectionViewModel : BaseViewModel
    {
        private BitmapImage _imageSource;
        private string _imageSourceDirectory;

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public string ImageSourceDirectory
        {
            get { return _imageSourceDirectory; }
            set
            {
                _imageSourceDirectory = value;
                OnPropertyChanged(nameof(ImageSourceDirectory));
            }
        }

        public PeopleDetectionViewModel()
        {
            ImageSource = BitmapConverter.ConvertBitmap(Properties.Resources.ImagePlaceholder);
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
            ImageSource = BitmapConverter.ConvertBitmap(bmp);
            IsModalVisible = false;
        }              
    }
}
