using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowTab.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainWindowTabItem : TabItem
    {
        public MainWindowTabItem()
        {
            InitializeComponent();
        }

        private void OnCloseTabButton(object sender, System.Windows.RoutedEventArgs e)
        {
            (Parent as MainWindowTabControl).Items.Remove(this);
        }

        private void OnMiddleClickTabItem(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle &&
                e.ButtonState == MouseButtonState.Pressed)
            {
                (Parent as MainWindowTabControl).Items.Remove(this);
            }
        }
    }
}
