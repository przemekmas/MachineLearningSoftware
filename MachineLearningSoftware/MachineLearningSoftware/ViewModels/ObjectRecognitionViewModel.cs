using Emgu.TF;
using Emgu.TF.Models;
using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace MachineLearningSoftware.ViewModels
{
    public class ObjectRecognitionViewModel : BaseViewModel
    {
        #region

        private string _inceptionGraphFileLocation;
        private string _outputLabelsFileLocation;
        private string _informationText;
        private string _imageFileName;
        private BitmapFrame _fileSource;

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
        
        #endregion

        #region Constructor

        public ObjectRecognitionViewModel()
        {
            FileSource = BitmapFrame.Create(BitmapConverter.CreateBitmapSourceFromBitmap(Properties.Resources.ImagePlaceholder));
        }

        #endregion

        #region Public Methods
        
        public void Recognise(string fileName)
        {
            var imageSource = new BitmapImage(new Uri(fileName));
            Inception inceptionGraph = null;
            FileSource = BitmapFrame.Create(new Uri(fileName));
            ImageFileName = fileName;

            //Use the following code for the full inception model
            //Inception inceptionGraph = new Inception();
            //Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 224, 224, 128.0f, 1.0f / 128.0f);

            //Uncomment the following code to use a retrained model to recognize followers, downloaded from the internet
            //Inception inceptionGraph = new Inception(null, new string[] {"optimized_graph.pb", "output_labels.txt"}, "https://github.com/emgucv/models/raw/master/inception_flower_retrain/", "Mul", "final_result");
            //Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 299, 299, 128.0f, 1.0f / 128.0f);

            //Uncomment the following code to use a retrained model to recognize followers, if you deployed the models with the application
            //For ".pb" and ".txt" bundled with the application, set the url to null
            byte[] model = File.ReadAllBytes(@"C:\Users\Przem\Downloads\flowers.jpg");

            if (_inceptionGraphFileLocation != null && _outputLabelsFileLocation != null)
            {
                inceptionGraph = new Inception(null, new string[] { _inceptionGraphFileLocation, _outputLabelsFileLocation }, null, "Mul", "final_result");
            }
            else
            {
                inceptionGraph = new Inception(null, new string[] { "optimized_graph.pb", "output_labels.txt" }, "https://github.com/emgucv/models/raw/master/inception_flower_retrain/", "Mul", "final_result");
            }

            Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 299, 299, 128.0f, 1.0f / 128.0f);

            float[] probability = inceptionGraph.Recognize(imageTensor);
            String resStr = String.Empty;
            if (probability != null)
            {
                String[] labels = inceptionGraph.Labels;
                float maxVal = 0;
                int maxIdx = 0;
                for (int i = 0; i < probability.Length; i++)
                {
                    if (probability[i] > maxVal)
                    {
                        maxVal = probability[i];
                        maxIdx = i;
                    }
                }
                resStr = String.Format("Object is {0} with {1}% probability.", labels[maxIdx], maxVal * 100);
            }
            InformationText = resStr;
            DisposeObjects(inceptionGraph, imageTensor);
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
                else if (option == FileDialogOption.ChooseInceptionGraph)
                {
                    SetInceptionGraph(filename);
                }
                else if (option == FileDialogOption.ChooseOutputLabels)
                {
                    SetOutputLabels(filename);
                }
            }
        }

        #endregion

        #region Private Methods
        
        private void SetOutputLabels(string filename)
        {
            _outputLabelsFileLocation = filename;
        }

        private void SetInceptionGraph(string filename)
        {
            _inceptionGraphFileLocation = filename;
        }
        
        private void DisposeObjects(Inception graph, Tensor tensor)
        {
            graph.Dispose();
            tensor.Dispose();
        }

        #endregion
    }
}
