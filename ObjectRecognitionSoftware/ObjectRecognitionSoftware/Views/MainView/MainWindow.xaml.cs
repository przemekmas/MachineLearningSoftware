using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            _viewModel.SetTabControl(TabControl);
            _viewModel.SetMainMenu(MainMenu1);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((MainMenuButtonControl)MainMenu1.SelectedItem).TextBlock.Text.ToString();
            _viewModel.OpenPage(selectedItem);                      
        }

        private void HideMenuButton1_Click(object sender, RoutedEventArgs e)
        {
            ShowOrHideMenu();
        }

        private void TopBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }                
        }
        
        private void OnClickMaximiseWindow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void OnClickMinimiseWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnClickShowMenu(object sender, RoutedEventArgs e)
        {
            ShowOrHideMenu(); 
        }

        private void ShowOrHideMenu()
        {
            double mainMenuWidth1 = MainMenu1.Width;
            var mainMenuGridColumn1 = MainMenuGridColumn1.Width;

            if (mainMenuWidth1 > 0)
            {
                ShowOrHideMenuItem.IsChecked = false;
                MainMenu1.Width = 0;
                HideMenuButton1.Content = "Show Menu ▼";
            }
            else
            {
                ShowOrHideMenuItem.IsChecked = true;
                MainMenu1.Width = 140;
                HideMenuButton1.Content = "Hide Menu ▲";
            }

            MainMenuGrid1.Width = GridLength.Auto;
            MainMenuGridColumn1.Width = GridLength.Auto;
        }
    }
}