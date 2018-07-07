using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for ExceptionLog.xaml
    /// </summary>
    [ViewExport(typeof(ExceptionLog), typeof(IResourceItemEntity), "Exception Log", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ExceptionLog : UserControl, IResourceItemEntity
    {
        public UserControl Page => this;

        public Control IconControl => new ExceptionLogIcon();
        
        private ExceptionLogViewModel _viewmodel;
        
        [ImportingConstructor]
        public ExceptionLog(ExceptionLogViewModel viewModel)
        {
            InitializeComponent();
            _viewmodel = viewModel;
            DataContext = _viewmodel;
        }

        private void ExceptionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExceptionDataGrid.SelectedItem is ExceptionEntity selectedException)
            {
                _viewmodel.SetExceptionDetails(selectedException.Exception);
            }
        }
    }
}
