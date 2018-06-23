using MachineLearningSoftware.Common;
using MachineLearningSoftware.Enumerations;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = DependencyInjection.ResolveSingle<MainWindowViewModel>();
        private MainWindowState _windowState;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _viewModel.SetTabControl(TabControl);
            _viewModel.SetMainMenu(MainMenu1);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu1.SelectedItem is MainMenuButtonControl selectedItem)
            {
                _viewModel.OpenPage(selectedItem.TextBlock.Text.ToString());
            }
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
            if (_windowState == MainWindowState.Normal)
            {
                MaximiseWindow();
            }
            else
            {
                RestoreWindow();
            }
        }

        private void RestoreWindow()
        {
            Width = 800;
            Height = 600;
            Left = (SystemParameters.WorkArea.Width - Width) / 2;
            Top = (SystemParameters.WorkArea.Height - Height) / 2;
            _windowState = MainWindowState.Normal;
        }

        private void MaximiseWindow()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
            Left = 0;
            Top = 0;
            _windowState = MainWindowState.Maximised;
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