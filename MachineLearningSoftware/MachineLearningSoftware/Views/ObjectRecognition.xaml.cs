using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for ObjectRecognition.xaml
    /// </summary>
    [ViewExport(typeof(ObjectRecognition), typeof(IResourceItemEntity), "Object Recognition", true)]
    public partial class ObjectRecognition : Page, IResourceItemEntity
    {
        private ObjectRecognitionViewModel _viewModel;

        public ObjectRecognition()
        {
            InitializeComponent();
            _viewModel = new ObjectRecognitionViewModel();
            DataContext = _viewModel;
        }

        public Page Page => this;

        public Control IconControl => new ObjectRecognitionIcon();

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
