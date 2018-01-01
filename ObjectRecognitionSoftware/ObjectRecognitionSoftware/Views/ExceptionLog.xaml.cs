using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using System.Windows.Controls;
using System.Windows.Shapes;
using System;
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

        private ExceptionLogViewModel viewmodel;
        
        public ExceptionLog()
        {
            InitializeComponent();
            viewmodel = new ExceptionLogViewModel();
            this.DataContext = viewmodel;
        }
    }
}
