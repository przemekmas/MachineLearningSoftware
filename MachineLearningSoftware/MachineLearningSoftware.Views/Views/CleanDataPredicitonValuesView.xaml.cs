using MachineLearningSoftware.Controls;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views.Views
{
    /// <summary>
    /// Interaction logic for CleanDataPredicitonValuesView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CleanDataPredicitonValuesView : CustomWindowControl
    {
        public CleanDataPredicitonValuesView()
        {
            InitializeComponent();
        }
    }
}
