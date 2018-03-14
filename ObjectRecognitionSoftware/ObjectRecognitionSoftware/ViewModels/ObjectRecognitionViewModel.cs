using Emgu.TF;
using Emgu.TF.Models;
using ObjectRecognitionSoftware.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class ObjectRecognitionViewModel : NotifyPropertyChanged
    {
        private string m_InceptionGraphFileLocation;
        private string m_OutputLabelsFileLocation;
        private string m_InformationText;
        private string m_ImageFileName;
        private BitmapFrame m_FileSource;
        private bool m_IsModalVisible;

        public BitmapFrame FileSource
        {
            get { return m_FileSource; }
            set
            {
                m_FileSource = value;
                OnPropertyChanged(nameof(FileSource));
            }
        }

        public string InformationText
        {
            get { return m_InformationText; }
            set
            {
                m_InformationText = value;
                OnPropertyChanged(nameof(InformationText));
            }
        }

        public string ImageFileName
        {
            get { return m_ImageFileName; }
            set
            {
                m_ImageFileName = value;
                OnPropertyChanged(nameof(ImageFileName));
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

            if (m_InceptionGraphFileLocation != null && m_OutputLabelsFileLocation != null)
            {
                inceptionGraph = new Inception(null, new string[] { m_InceptionGraphFileLocation, m_OutputLabelsFileLocation }, null, "Mul", "final_result");
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

                if (option == FileDialogOption.chooseImage)
                {
                    IsModalVisible = true;                    
                    Task.Run(() => Recognise(filename));
                }
                else if (option == FileDialogOption.chooseInceptionGraph)
                {
                    SetInceptionGraph(filename);
                }
                else if (option == FileDialogOption.chooseOutputLabels)
                {
                    SetOutputLabels(filename);
                }
            }
        }

        #endregion

        #region Private Methods
        
        private void SetOutputLabels(string filename)
        {
            m_OutputLabelsFileLocation = filename;
        }

        private void SetInceptionGraph(string filename)
        {
            m_InceptionGraphFileLocation = filename;
        }
        
        private void DisposeObjects(Inception graph, Tensor tensor)
        {
            graph.Dispose();
            tensor.Dispose();
        }

        #endregion
    }
}
