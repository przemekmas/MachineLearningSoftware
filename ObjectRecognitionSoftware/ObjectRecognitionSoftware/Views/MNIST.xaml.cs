using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using System.Windows;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for MNIST.xaml
    /// </summary>
    public partial class MNIST : Page, IResourceItemEntity
    {
        private MNISTViewModel _viewModel;

        public MNIST()
        {
            InitializeComponent();
            _viewModel = new MNISTViewModel();
            DataContext = _viewModel;
        }

        public string Name => "MNIST";

        public Page Page => this;

        public Control IconControl => new DefaultIcon();

        public bool IsVisible => true;
        
        private void PreviousButtonClick1(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousDatasetImage();
        }

        private void NextButtonClick1(object sender, RoutedEventArgs e)
        {
            _viewModel.NextDatasetImage();
        }
    }
}
