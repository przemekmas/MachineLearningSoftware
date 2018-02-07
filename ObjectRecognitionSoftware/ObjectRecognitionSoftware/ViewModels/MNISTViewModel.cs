using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class MNISTViewModel : NotifyPropertyChanged
    {
        #region Fields

        private string m_DatasetInformation;
        private string m_CompressedImageFileName = "t10k-images.idx3-ubyte.zip";
        private string m_CompressedLabelsFileName = "t10k-labels.idx1-ubyte.zip";
        private string m_ImageFileName = "t10k-images.idx3-ubyte";
        private string m_LabelsFileName = "t10k-labels.idx1-ubyte";
        private string m_ImagesDownloadLink = "http://yann.lecun.com/exdb/mnist/t10k-images-idx3-ubyte.gz";
        private string m_LabelsDownloadLink = "http://yann.lecun.com/exdb/mnist/t10k-labels-idx1-ubyte.gz";
        private int m_DatasetPosition = 1;
        private int m_LabelPosition = 0;
        private int m_CorrespondingLabel;
        private List<int> m_PixelArray;

        #endregion

        #region Properties

        public string DatasetInformation
        {
            get { return m_DatasetInformation; }
            set
            {
                m_DatasetInformation = value;
                OnPropertyChanged(nameof(DatasetInformation));
            }
        }

        public List<int> PixelArray
        {
            get { return m_PixelArray; }
            set
            {
                m_PixelArray = value;
                OnPropertyChanged(nameof(PixelArray));
            }
        }

        public int CorrespondingLabel
        {
            get { return m_CorrespondingLabel; }
            set
            {
                m_CorrespondingLabel = value;
                OnPropertyChanged(nameof(CorrespondingLabel));
            }
        }

        #endregion

        #region Constructor

        public MNISTViewModel()
        {
            InitialiseMNISTDataset();
        }

        #endregion

        #region Public Methods

        public void NextDatasetImage()
        {
            m_DatasetPosition += 784;
            m_LabelPosition++;
            ReadMNISTDataset();
        }

        public void PreviousDatasetImage()
        {
            if (m_DatasetPosition - 784 > 0)
            {
                m_DatasetPosition -= 784;
                m_LabelPosition--;
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
                    webClient.DownloadFile(m_ImagesDownloadLink, directories.CompressedImagesFileDir);
                    webClient.DownloadFile(m_LabelsDownloadLink, directories.CompressedLabelsFileDir);
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
                ifsLabels.Seek(m_LabelPosition, SeekOrigin.Begin);
                ifsImages.Seek(m_DatasetPosition, SeekOrigin.Begin);
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
                DatasetInformation += dImage.ToString();

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex.ToString());
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
            var directory = CurrentDirectory.NavigateToDirectory(@"..\..\Assets\MNISTDataset");
            var compressedImagesFileDir = string.Format(@"{0}\{1}", directory, m_CompressedImageFileName);
            var compressedLabelsFileDir = string.Format(@"{0}\{1}", directory, m_CompressedLabelsFileName);
            var imagesFileDir = string.Format(@"{0}\{1}", directory, m_ImageFileName);
            var labelsFileDir = string.Format(@"{0}\{1}", directory, m_LabelsFileName);
            
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
