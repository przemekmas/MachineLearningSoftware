using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for MNIST.xaml
    /// </summary>
    public partial class MNIST : Page, IResourceItemEntity
    {
        private MNISTViewModel viewModel;

        public MNIST()
        {
            InitializeComponent();
            viewModel = new MNISTViewModel();
            this.DataContext = viewModel;
        }

        public string Name => "MNIST";

        public Page Page => this;

        public Control IconControl => new DefaultIcon();
        
        private void PreviousButtonClick1(object sender, RoutedEventArgs e)
        {
            viewModel.PreviousDatasetImage();
        }

        private void NextButtonClick1(object sender, RoutedEventArgs e)
        {
            viewModel.NextDatasetImage();
        }
    }
}
