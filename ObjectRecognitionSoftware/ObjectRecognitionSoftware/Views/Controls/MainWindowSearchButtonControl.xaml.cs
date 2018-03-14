using ObjectRecognitionSoftware.Views.Controls.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognitionSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowSearchButtonControl.xaml
    /// </summary>
    public partial class MainWindowSearchButtonControl : UserControl
    {
        private MainMenuSearchButtonViewModel m_ViewModel;

        public MainWindowSearchButtonControl()
        {
            InitializeComponent();
            m_ViewModel = new MainMenuSearchButtonViewModel();
            this.DataContext = m_ViewModel;
        }
        
        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            m_ViewModel.Search(TextBoxSearchInput.Text);
        }

        private void TextBoxSearchInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                m_ViewModel.Search(TextBoxSearchInput.Text);
            }
        }
    }
}
