using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for MNIST.xaml
    /// </summary>
    [ViewExport(typeof(MNIST), typeof(IResourceItemEntity), "MNIST", true)]
    public partial class MNIST : Page, IResourceItemEntity
    {
        private MNISTViewModel _viewModel;

        public MNIST()
        {
            InitializeComponent();
            _viewModel = new MNISTViewModel();
            DataContext = _viewModel;
        }

        public Page Page => this;

        public Control IconControl => new DefaultIcon();
        
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
