using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls;
using MachineLearningSoftware.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindowControl
    {
        private MainWindowViewModel _viewModel = DependencyInjection.ResolveSingle<MainWindowViewModel>();
        private MainWindowFunctions _mainWindowFunctions = DependencyInjection.ResolveSingle<MainWindowFunctions>();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _mainWindowFunctions.SetMainWindowTabControl(TabControl);
            _mainWindowFunctions.LoadMenuItems(MainMenu1);            
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainMenu1.SelectedItem is MainMenuButtonControl selectedItem)
            {
                _mainWindowFunctions.OpenPage(selectedItem.PageTitle.ToString());
                MainMenu1.SelectedItem = null;
            }
        }

        private void HideMenuButton1_Click(object sender, RoutedEventArgs e)
        {
            ShowOrHideMenu();
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

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            _mainWindowFunctions.AddStartPage();
            SetBinding(ExitWindowCommandProperty, "DisplayExitDialogCommand");
        }
    }
}