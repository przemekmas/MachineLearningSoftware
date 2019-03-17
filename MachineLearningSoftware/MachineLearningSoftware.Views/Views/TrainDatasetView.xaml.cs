using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Views.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Views
{
    /// <summary>
    /// Interaction logic for TrainDatasetView.xaml
    /// </summary>
    [ViewExport(typeof(TensorBoard), typeof(IResourceItemEntity), "Train Dataset", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TrainDatasetView : UserControl, IResourceItemEntity
    {
        public UserControl Page => this;

        public Control IconControl => new DefaultIcon();

        [ImportingConstructor]
        public TrainDatasetView(TrainDatasetViewModel trainDatasetViewModel)
        {
            InitializeComponent();
            DataContext = trainDatasetViewModel;
        }
    }
}
