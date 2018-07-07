using MachineLearningSoftware.Common;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MNISTViewModel : BaseViewModel
    {
        #region Fields
        
        private ExceptionLogDataAccess _exceptionLogging;
        private string _datasetInformation;
        private string _compressedImageFileName = "t10k-images.idx3-ubyte.zip";
        private string _compressedLabelsFileName = "t10k-labels.idx1-ubyte.zip";
        private string _imageFileName = "t10k-images.idx3-ubyte";
        private string _labelsFileName = "t10k-labels.idx1-ubyte";
        private string _imagesDownloadLink = "http://yann.lecun.com/exdb/mnist/t10k-images-idx3-ubyte.gz";
        private string _labelsDownloadLink = "http://yann.lecun.com/exdb/mnist/t10k-labels-idx1-ubyte.gz";
        private int _datasetPosition = 1;
        private int _labelPosition = 0;
        private int _correspondingLabel;
        private List<int> _pixelArray;
        
        #endregion

        #region Properties

        public string DatasetInformation
        {
            get { return _datasetInformation; }
            set
            {
                _datasetInformation = value;
                OnPropertyChanged(nameof(DatasetInformation));
            }
        }

        public List<int> PixelArray
        {
            get { return _pixelArray; }
            set
            {
                _pixelArray = value;
                OnPropertyChanged(nameof(PixelArray));
            }
        }

        public int CorrespondingLabel
        {
            get { return _correspondingLabel; }
            set
            {
                _correspondingLabel = value;
                OnPropertyChanged(nameof(CorrespondingLabel));
            }
        }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public MNISTViewModel(ExceptionLogDataAccess exceptionLogging)
        {
            ConfigureHeaderControl(true, true, Properties.MNISTResource.Title,
                true, Properties.MNISTResource.Description);
            _exceptionLogging = exceptionLogging;
            InitialiseMNISTDataset();
        }

        #endregion

        #region Public Methods

        public void NextDatasetImage()
        {
            _datasetPosition += 784;
            _labelPosition++;
            ReadMNISTDataset();
        }

        public void PreviousDatasetImage()
        {
            if (_datasetPosition - 784 > 0)
            {
                _datasetPosition -= 784;
                _labelPosition--;
                ReadMNISTDataset();
            }            
        }

        #endregion

        #region Private Methods

        private void InitialiseMNISTDataset()
        {
            DownloadDataset();            
            var readMNISTThread = new Thread(new ThreadStart(ReadMNISTDataset));
            readMNISTThread.Start();
        }

        private void DownloadDataset()
        {
            var webClient = new WebClient();
            webClient.UseDefaultCredentials = true;
            webClient.DownloadDataCompleted += WebClientDownloadCompleted;
            
            var directories = GetDirectories();

            if (!CurrentDirectory.DirectoryExists(directories.MNISTFileDir))
            {
                CurrentDirectory.CreateNewDirectory(directories.MNISTFileDir);
            }

            if (!DatasetAlreadyExists(directories.MNISTFileDir))
            {
                try
                {
                    webClient.DownloadFile(_imagesDownloadLink, directories.CompressedImagesFileDir);
                    webClient.DownloadFile(_labelsDownloadLink, directories.CompressedLabelsFileDir);
                }
                catch(Exception e)
                {

                }
                finally
                {

                }
            }                    
        }

        private void ReadMNISTDataset()
        {
            try
            {
                var directories = GetDirectories();
                var ifsLabels = new FileStream(directories.LabelsFileDir, FileMode.Open);
                var ifsImages = new FileStream(directories.ImagesFileDir, FileMode.Open);
                ifsLabels.Seek(_labelPosition, SeekOrigin.Begin);
                ifsImages.Seek(_datasetPosition, SeekOrigin.Begin);
                var brLabels = new BinaryReader(ifsLabels);
                var brImages = new BinaryReader(ifsImages);
                int magic1 = brImages.ReadInt32();
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();
                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();
                var pixels = new byte[28][];

                for (int i = 0; i < pixels.Length; ++i)
                {
                    pixels[i] = new byte[28];
                }
                
                for (int i = 0; i < 28; ++i)
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte();
                var dImage = new DigitImage(pixels, lbl);
                CorrespondingLabel = lbl;
                PixelArray = dImage.GetImageArray();
                var formatEmptyLines = Regex.Replace(dImage.ToString(), @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                DatasetInformation += string.Format("{0}\n\r", formatEmptyLines);

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();
            }
            catch (Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }
        }
        
        private bool DatasetAlreadyExists(string directory)
        {
            var directories = GetDirectories();
            return File.Exists(directories.CompressedImagesFileDir) && File.Exists(directories.CompressedLabelsFileDir) ? true : false;
        }

        private void WebClientDownloadCompleted(object args, EventArgs e)
        {
            var directories = GetDirectories();
            CurrentDirectory.ExtractFiles(directories.CompressedImagesFileDir, directories.MNISTFileDir);
            CurrentDirectory.ExtractFiles(directories.CompressedLabelsFileDir, directories.MNISTFileDir);
        }

        private DownloadDirectories GetDirectories()
        {
            var directory = CurrentDirectory.NavigateToDirectory(@"Assets\MNISTDataset");
            var compressedImagesFileDir = string.Format(@"{0}\{1}", directory, _compressedImageFileName);
            var compressedLabelsFileDir = string.Format(@"{0}\{1}", directory, _compressedLabelsFileName);
            var imagesFileDir = string.Format(@"{0}\{1}", directory, _imageFileName);
            var labelsFileDir = string.Format(@"{0}\{1}", directory, _labelsFileName);
            
            return new DownloadDirectories()
            {
                MNISTFileDir = directory,
                CompressedImagesFileDir = compressedImagesFileDir,
                CompressedLabelsFileDir = compressedLabelsFileDir,
                ImagesFileDir = imagesFileDir,
                LabelsFileDir = labelsFileDir
            };
        }

        #endregion
    }

    public class DownloadDirectories
    {
        public string MNISTFileDir { get; set; }
        public string CompressedImagesFileDir { get; set; }
        public string CompressedLabelsFileDir { get; set; }
        public string ImagesFileDir { get; set; }
        public string LabelsFileDir { get; set; }
    }
}
