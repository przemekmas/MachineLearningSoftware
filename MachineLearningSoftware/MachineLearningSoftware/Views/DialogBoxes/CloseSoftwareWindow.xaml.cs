using MachineLearningSoftware.Controls;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views.DialogBoxes
{
    /// <summary>
    /// Interaction logic for CloseSoftwareWindow.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CloseSoftwareWindow : CustomWindowControl
    {
        public CloseSoftwareWindow()
        {
            InitializeComponent();
        }
    }
}
