using MachineLearningSoftware.Views.Controls.ViewModels;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Controls.CustomButtons
{
    /// <summary>
    /// Interaction logic for GithubButtonControl.xaml
    /// </summary>
    public partial class GithubButtonControl : UserControl
    {
        public GithubButtonControl()
        {
            DataContext = new GithubButtonViewModel();
            InitializeComponent();
        }
    }
}
