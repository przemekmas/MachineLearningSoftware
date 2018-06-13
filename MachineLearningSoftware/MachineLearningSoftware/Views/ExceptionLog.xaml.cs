using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for ExceptionLog.xaml
    /// </summary>
    [ViewExport(typeof(ExceptionLog), typeof(IResourceItemEntity), "Exception Log", true)]
    public partial class ExceptionLog : Page, IResourceItemEntity
    {
        public Page Page => this;

        public Control IconControl => new ExceptionLogIcon();

        private ExceptionLogViewModel _viewmodel;
        
        public ExceptionLog()
        {
            InitializeComponent();
            _viewmodel = new ExceptionLogViewModel();
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
