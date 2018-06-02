using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using System.Windows.Controls;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;

namespace ObjectRecognitionSoftware.Views
{
    /// <summary>
    /// Interaction logic for RetrainInceptionModel.xaml
    /// </summary>
    public partial class WideDeep : Page, IResourceItemEntity
    {
        private WideDeepViewModel _viewmodel;

        public WideDeep()
        {
            _viewmodel = new WideDeepViewModel();
            DataContext = _viewmodel;
            InitializeComponent();
        }

        public Page Page => this;

        public string Name => "Wide Deep";

        public bool IsVisible => true;

        public Control IconControl => new DefaultIcon();
    }
}
