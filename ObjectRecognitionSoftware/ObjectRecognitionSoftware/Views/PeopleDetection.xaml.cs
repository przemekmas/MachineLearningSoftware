using Emgu.TF;
using System.Windows;
using System.Windows.Controls;
using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using ObjectRecognitionSoftware.ViewModels;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for PeopleDetection.xaml
    /// </summary>
    public partial class PeopleDetection : Page, IResourceItemEntity
    {
        private PeopleDetectionViewModel m_ViewModel;
        
        public PeopleDetection()
        {
            InitializeComponent();
            TfInvoke.CheckLibraryLoaded();
            m_ViewModel = new PeopleDetectionViewModel();
            DataContext = m_ViewModel;
        }

        public string Name => "People Detection";

        public Page Page => this;

        public Control IconControl => new PeopleDetectionIcon();

        public bool IsVisible => true;

        private void ChooseImageButton1_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.ChooseImage();            
        }  
    }
}
