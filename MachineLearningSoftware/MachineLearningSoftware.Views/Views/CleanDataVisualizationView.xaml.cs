using MachineLearningSoftware.Controls;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views.Views
{
    /// <summary>
    /// Interaction logic for CleanDataVisualizationView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CleanDataVisualizationView : CustomWindowControl
    {
        public CleanDataVisualizationView()
        {
            InitializeComponent();
        }
    }
}
