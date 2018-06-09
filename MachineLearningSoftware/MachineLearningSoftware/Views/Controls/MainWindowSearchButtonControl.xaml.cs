using MachineLearningSoftware.Views.Controls.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowSearchButtonControl.xaml
    /// </summary>
    public partial class MainWindowSearchButtonControl : UserControl
    {
        private MainMenuSearchButtonViewModel _viewModel;

        public MainWindowSearchButtonControl()
        {
            InitializeComponent();
            _viewModel = new MainMenuSearchButtonViewModel();
            this.DataContext = _viewModel;
        }
        
        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Search(TextBoxSearchInput.Text);
        }

        private void TextBoxSearchInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.Search(TextBoxSearchInput.Text);
            }
        }
    }
}
