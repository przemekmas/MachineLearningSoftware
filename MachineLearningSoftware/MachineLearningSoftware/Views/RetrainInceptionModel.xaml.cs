using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for RetrainInceptionModel.xaml
    /// </summary>
    [ViewExport(typeof(PeopleDetection), typeof(IResourceItemEntity), "Retrain Inception Model", true)]
    public partial class RetrainInceptionModel : Page, IResourceItemEntity
    {
        private RetrainInceptionModelViewModel _viewmodel;

        public RetrainInceptionModel()
        {
            _viewmodel = new RetrainInceptionModelViewModel();
            DataContext = _viewmodel;
            InitializeComponent();
        }

        public Page Page => this;

        public System.Windows.Controls.Control IconControl => new RetrainInceptionModelIcon();

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            _viewmodel.InstallRequiredPackages();
        }

        private void BuildButton_Click(object sender, RoutedEventArgs e)
        {
            _viewmodel.RetrainInceptionModel();
        }

        private void ChooseImageFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.SelectedPath;
            if (!string.IsNullOrEmpty(filename))
            {
                _viewmodel.imageDirectory = filename;
                _viewmodel.TextBoxLog += filename;
            }
        }
    }
}
