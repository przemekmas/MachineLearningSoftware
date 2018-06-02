using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using System.Windows.Controls;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for ExceptionLog.xaml
    /// </summary>
    public partial class ExceptionLog : Page, IResourceItemEntity
    {
        public Page Page => this;

        public string Name => "Exception Log";

        public Control IconControl => new ExceptionLogIcon();

        public bool IsVisible => true;

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
