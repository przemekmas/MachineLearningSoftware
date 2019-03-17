using MachineLearningSoftware.Controls;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views.DialogBoxes
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class InformationWindow : CustomWindowControl
    {
        public InformationWindow()
        {
            InitializeComponent();
        }
    }
}
