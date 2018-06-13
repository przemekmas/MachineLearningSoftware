using Emgu.TF;
using System.Windows;
using System.Windows.Controls;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Common;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for PeopleDetection.xaml
    /// </summary>
    [ViewExport(typeof(PeopleDetection), typeof(IResourceItemEntity), "People Detection", true)]
    public partial class PeopleDetection : Page, IResourceItemEntity
    {
        private PeopleDetectionViewModel _viewModel;
        
        public PeopleDetection()
        {
            InitializeComponent();
            TfInvoke.CheckLibraryLoaded();
            _viewModel = new PeopleDetectionViewModel();
            DataContext = _viewModel;
        }

        public Page Page => this;

        public Control IconControl => new PeopleDetectionIcon();

        private void ChooseImageButton1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ChooseImage();            
        }  
    }
}
