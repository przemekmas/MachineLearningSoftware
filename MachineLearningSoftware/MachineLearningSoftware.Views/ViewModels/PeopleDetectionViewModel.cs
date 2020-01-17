using Emgu.Models;
using Emgu.TF.Models;
using MachineLearningSoftware.Common;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PeopleDetectionViewModel : BaseViewModel
    {
        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;
        private BitmapImage _imageSource;
        private string _imageSourceDirectory;
        private string _fileName;
        private MultiboxGraph _graph;

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

        [ImportingConstructor]
        public PeopleDetectionViewModel(ExceptionLogDataAccess exceptionLogDataAccess)
        {
            ConfigureHeaderControl(true, true, Properties.PeopleDetectionResource.Title);
            ImageSource = BitmapConverter.ConvertBitmap(Properties.Resources.ImagePlaceholder);
            _exceptionLogDataAccess = exceptionLogDataAccess;
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
            using(_graph = new MultiboxGraph())
            {
                _fileName = fileName;
                _graph.OnDownloadCompleted += OnDownloadComplete;
                _graph.Init();
            }
        }

        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                using (var imageTensor = ImageIO.ReadTensorFromImageFile<float>(_fileName, 224, 224, 128.0f, 1.0f / 128.0f))
                {
                    var detectionResult = _graph.Detect(imageTensor);
                    var detectionAnnotations = MultiboxGraph.FilterResults(detectionResult, 0.1f);
                    var detectionImage = NativeImageIO.ImageFileToJpeg(_fileName, detectionAnnotations);
                    var typeConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
                    var detectionBmpImage = (Bitmap)typeConverter.ConvertFrom(detectionImage.Raw);
                    ImageSource = BitmapConverter.ConvertBitmap(detectionBmpImage);
                }
                IsModalVisible = false;
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
                IsModalVisible = false;
            }
        }
    }
}