using Microsoft.Win32;
using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.DialogBoxes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Shapes;
using System;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for RetrainInceptionModel.xaml
    /// </summary>
    public partial class RetrainInceptionModel : Page, IResourceItemEntity
    {
        private RetrainInceptionModelViewModel viewmodel;

        public RetrainInceptionModel()
        {
            viewmodel = new RetrainInceptionModelViewModel();
            this.DataContext = viewmodel;
            InitializeComponent();
        }

        public Page Page => this;

        public string Name => "Retrain Inception Model";

        public System.Windows.Controls.Control IconControl => new RetrainInceptionModelIcon();

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.InstallRequiredPackages();
        }

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.BuildTensorFlow();
        }

        private void ChooseImageFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.SelectedPath;
            if (!string.IsNullOrEmpty(filename))
            {
                viewmodel.imageDirectory = filename;
                viewmodel.TextBoxLog += filename;
            }
        }

        private void OpenHelpDialog_Click(object sender, RoutedEventArgs e)
        {
            new InceptionModelHelpDialog().ShowDialog();
        }
    }
}
