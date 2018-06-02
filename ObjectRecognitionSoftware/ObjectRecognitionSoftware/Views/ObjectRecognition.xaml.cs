using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using System.Windows;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for ObjectRecognition.xaml
    /// </summary>
    public partial class ObjectRecognition : Page, IResourceItemEntity
    {
        private ObjectRecognitionViewModel _viewModel;

        public ObjectRecognition()
        {
            InitializeComponent();
            _viewModel = new ObjectRecognitionViewModel();
            DataContext = _viewModel;
        }

        public string Name => "Object Recognition";

        public Page Page => this;

        public Control IconControl => new ObjectRecognitionIcon();

        public bool IsVisible => true;

        private void ChooseImageButton1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenWindowsDialog(FileDialogOption.ChooseImage);
        }

        private void ChooseInceptionGraphButton1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenWindowsDialog(FileDialogOption.ChooseInceptionGraph);
        }
        
        private void ChooseOutputLabelButton1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenWindowsDialog(FileDialogOption.ChooseOutputLabels);
        }
    }
}
