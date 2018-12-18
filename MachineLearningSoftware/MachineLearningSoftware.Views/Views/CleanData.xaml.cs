using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using MachineLearningSoftware.Views.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Views
{
    /// <summary>
    /// Interaction logic for CleanData.xaml
    /// </summary>
    [ViewExport(typeof(CleanData), typeof(IResourceItemEntity), "Clean Data", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CleanData : UserControl, IResourceItemEntity
    {
        private readonly CleanDataViewModel _cleanDataViewModel;
        public UserControl Page => this;
        public Control IconControl => new DefaultIcon();

        [ImportingConstructor]
        public CleanData(CleanDataViewModel cleanDataViewModel)
        {
            InitializeComponent();
            _cleanDataViewModel = cleanDataViewModel;
            DataContext = _cleanDataViewModel;
        }
    }
}
