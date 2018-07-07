using MachineLearningSoftware.Entities;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;
using System.ComponentModel.Composition;
using MachineLearningSoftware.Views.ViewModels;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for WideDeep.xaml
    /// </summary>
    [ViewExport(typeof(WideDeep), typeof(IResourceItemEntity), "Wide Deep", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class WideDeep : UserControl, IResourceItemEntity
    {
        [ImportingConstructor]
        public WideDeep(WideDeepViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public UserControl Page => this;

        public Control IconControl => new DefaultIcon();
    }
}
