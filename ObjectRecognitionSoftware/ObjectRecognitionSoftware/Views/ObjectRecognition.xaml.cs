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
        private ObjectRecognitionViewModel m_ViewModel;

        public ObjectRecognition()
        {
            InitializeComponent();
            m_ViewModel = new ObjectRecognitionViewModel();
            DataContext = m_ViewModel;
        }

        public string Name => "Object Recognition";

        public Page Page => this;

        public Control IconControl => new ObjectRecognitionIcon();

        public bool IsVisible => true;

        private void ChooseImageButton1_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.OpenWindowsDialog(FileDialogOption.chooseImage);
        }

        private void ChooseInceptionGraphButton1_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.OpenWindowsDialog(FileDialogOption.chooseInceptionGraph);
        }
        
        private void ChooseOutputLabelButton1_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.OpenWindowsDialog(FileDialogOption.chooseOutputLabels);
        }
    }
}
