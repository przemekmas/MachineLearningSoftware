using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowTab.xaml
    /// </summary>
    public partial class MainWindowTab : TabItem
    {
        private TabControl m_MainTabControl;

        public MainWindowTab(TabControl mainTabControl)
        {
            InitializeComponent();
            m_MainTabControl = mainTabControl;
        }

        private void CloseTabButton(object sender, System.Windows.RoutedEventArgs e)
        {
            m_MainTabControl.Items.Remove(this);
        }
    }
}
