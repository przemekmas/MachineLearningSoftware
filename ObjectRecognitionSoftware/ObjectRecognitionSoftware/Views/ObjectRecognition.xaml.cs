using Emgu.TF;
using Emgu.TF.Models;
using Microsoft.Win32;
using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.DialogBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for ObjectRecognition.xaml
    /// </summary>
    public partial class ObjectRecognition : Page, IResourceItemEntity
    {
        private LoadingDialogBox loadingDialogBox;

        public ObjectRecognition()
        {
            InitializeComponent();
            loadingDialogBox = new LoadingDialogBox();
        }
        public string Name
        {
            get { return "Object Recognition"; }
        }

        public Page Page
        {
            get { return this; }
        }

        private void Recognise(string fileName)
        {
            var imageSource = new BitmapImage(new Uri(fileName));

            ChooseImageLabel1.Text = fileName;
            ChooseImage1.Source = imageSource;

            //Use the following code for the full inception model
            //Inception inceptionGraph = new Inception();
            //Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 224, 224, 128.0f, 1.0f / 128.0f);

            //Uncomment the following code to use a retrained model to recognize followers, downloaded from the internet
            //Inception inceptionGraph = new Inception(null, new string[] {"optimized_graph.pb", "output_labels.txt"}, "https://github.com/emgucv/models/raw/master/inception_flower_retrain/", "Mul", "final_result");
            //Tensor imageTensor = ImageIO.ReadTensorFromImageFile(fileName, 299, 299, 128.0f, 1.0f / 128.0f);

            //Uncomment the following code to use a retrained model to recognize followers, if you deployed the models with the application
            //For ".pb" and ".txt" bundled with the application, set the url to null
            Inception inceptionGraph = new Inception(null, new string[] {"optimized_graph.pb", "output_labels.txt"}, null, "Mul", "final_result");
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
            InceptionInformationText1.Text = resStr;
            DisposeObjects(inceptionGraph, imageTensor);
        }

        private void DisposeObjects(Inception graph, Tensor tensor)
        {
            graph.Dispose();
            tensor.Dispose();
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
                Recognise(filename);
                loadingDialogBox.Hide();
            }
        }
    }
}
