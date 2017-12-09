using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for ExceptionLog.xaml
    /// </summary>
    public partial class ExceptionLog : Page, IResourceItemEntity
    {
        public Page Page => this;

        public string Name => "Exception Log";

        private ExceptionLogViewModel viewmodel;
        
        public ExceptionLog()
        {
            InitializeComponent();
            viewmodel = new ExceptionLogViewModel();
            this.DataContext = viewmodel;
        }
    }
}
