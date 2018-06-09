using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowTab.xaml
    /// </summary>
    public partial class MainWindowTab : TabItem
    {
        private TabControl _mainTabControl;

        public MainWindowTab(TabControl mainTabControl)
        {
            InitializeComponent();
            _mainTabControl = mainTabControl;
        }

        private void CloseTabButton(object sender, System.Windows.RoutedEventArgs e)
        {
            _mainTabControl.Items.Remove(this);
        }
    }
}
