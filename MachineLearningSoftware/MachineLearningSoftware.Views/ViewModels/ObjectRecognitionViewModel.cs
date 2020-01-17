using Emgu.TF;
using Emgu.TF.Models;
using MachineLearningSoftware.Common;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ObjectRecognitionViewModel : BaseViewModel
    {
        #region

        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;
        private string _informationText;
        private string _imageFileName;
        private BitmapFrame _fileSource;
        private Inception _inceptionGraph;
        private string _fileName;
        private string _inceptionGraphURL;
        private string _outputLabelsURL;
        private string _downloadURL;

        #endregion

        #region Properties

        public BitmapFrame FileSource
        {
            get { return _fileSource; }
            set
            {
                _fileSource = value;
                OnPropertyChanged(nameof(FileSource));
            }
        }

        public string InformationText
        {
            get { return _informationText; }
            set
            {
                _informationText = value;
                OnPropertyChanged(nameof(InformationText));
            }
        }

        public string ImageFileName
        {
            get { return _imageFileName; }
            set
            {
                _imageFileName = value;
                OnPropertyChanged(nameof(ImageFileName));
            }
        }

        public string InceptionGraphFileName
        {
            get { return _inceptionGraphURL; }
            set
            {
                _inceptionGraphURL = value;
                OnPropertyChanged(nameof(InceptionGraphFileName));
            }
        }

        public string OutputLabelsFileName
        {
            get { return _outputLabelsURL; }
            set
            {
                _outputLabelsURL = value;
                OnPropertyChanged(nameof(OutputLabelsFileName));
            }
        }

        public string DownloadURL
        {
            get { return _downloadURL; }
            set
            {
                _downloadURL = value;
                OnPropertyChanged(nameof(DownloadURL));
            }
        }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public ObjectRecognitionViewModel(ExceptionLogDataAccess exceptionLogDataAccess)
        {
            ConfigureHeaderControl(true, true, Properties.ObjectRecognitionResource.Title);
            FileSource = BitmapFrame.Create(BitmapConverter.CreateBitmapSourceFromBitmap(Properties.Resources.ImagePlaceholder));
            _exceptionLogDataAccess = exceptionLogDataAccess;
        }

        #endregion

        #region Public Methods
        
        public void Recognise(string fileName)
        {
            FileSource = BitmapFrame.Create(new Uri(fileName));
            ImageFileName = fileName;

            try
            {
                using (_inceptionGraph = new Inception())
                {
                    _fileName = fileName;
                    _inceptionGraph.OnDownloadCompleted += OnDownloadComplete;
                    if (!string.IsNullOrEmpty(DownloadURL)
                        && !string.IsNullOrEmpty(InceptionGraphFileName)
                        && !string.IsNullOrEmpty(OutputLabelsFileName))
                    {
                        _inceptionGraph.Init(new string[] { InceptionGraphFileName, OutputLabelsFileName }, DownloadURL);
                    }
                    else
                    {
                        _inceptionGraph.Init();
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
                IsModalVisible = false;
            }
        }

        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                var stringResult = new StringBuilder();

                using (var imageTensor = ImageIO.ReadTensorFromImageFile<float>(_fileName, 256, 256))
                {
                    var results = _inceptionGraph.Recognize(imageTensor);

                    foreach (var recognitionResult in results.OrderByDescending(x => x.Probability).Take(5))
                    {
                        if (decimal.TryParse(recognitionResult.Probability.ToString(), out decimal resultResult))
                        {
                            stringResult.Append($"Object is {recognitionResult.Label} with {resultResult}% probability.")
                                .AppendLine();
                        }
                    }
                }

                InformationText = stringResult.ToString();
                IsModalVisible = false;
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
            }
            IsModalVisible = false;
        }

        public void OpenWindowsDialog(FileDialogOption option)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.FileName;
            ImageFileName = filename;

            if (!string.IsNullOrEmpty(filename))
            {

                if (option == FileDialogOption.ChooseImage)
                {
                    IsModalVisible = true;                    
                    Task.Run(() => Recognise(filename));
                }
            }
        }

        #endregion
    }
}
