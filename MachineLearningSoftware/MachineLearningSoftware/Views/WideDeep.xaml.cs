using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for RetrainInceptionModel.xaml
    /// </summary>
    [ViewExport(typeof(WideDeep), typeof(IResourceItemEntity), "Wide Deep", true)]
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

        public Control IconControl => new DefaultIcon();
    }
}
