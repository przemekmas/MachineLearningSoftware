using MachineLearningSoftware.Entities;
using System.Windows.Controls;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Common;
using System.ComponentModel.Composition;
using MachineLearningSoftware.Views.ViewModels;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for PerformanceIndicator.xaml
    /// </summary>
    [ViewExport(typeof(PerformanceIndicator), typeof(IResourceItemEntity), "Performance Indicator", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PerformanceIndicator : UserControl, IResourceItemEntity
    {
        [ImportingConstructor]
        public PerformanceIndicator(PerformanceIndicatorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public UserControl Page => this;

        public Control IconControl => new DefaultIcon();
    }
}
