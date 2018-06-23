using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for WideDeep.xaml
    /// </summary>
    [ViewExport(typeof(WideDeep), typeof(IResourceItemEntity), "Wide Deep", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class WideDeep : Page, IResourceItemEntity
    {
        [ImportingConstructor]
        public WideDeep(WideDeepViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public Page Page => this;

        public Control IconControl => new DefaultIcon();
    }
}
