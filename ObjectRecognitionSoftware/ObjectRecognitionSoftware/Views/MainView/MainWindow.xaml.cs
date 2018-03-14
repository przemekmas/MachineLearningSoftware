using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls;
using ObjectRecognitionSoftware.Views.DialogBoxes;
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
        private MainWindowViewModel m_ViewModel;
        private HelpDialogBox m_HelpDialog;
        private CloseSoftwareDialog m_CloseDialog;
        private MainWindowFunctions m_MainWindowFunctions;
        
        public MainWindow()
        {
            InitializeComponent();
            m_ViewModel = new MainWindowViewModel();
            this.DataContext = m_ViewModel;
            m_MainWindowFunctions = MainWindowFunctions.Instance;
            m_MainWindowFunctions.TabControl = TabControl;
            m_MainWindowFunctions.LoadPanels(MainMenu1);
        }
        
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((MainMenuButtonControl)MainMenu1.SelectedItem).TextBlock.Text.ToString();
            m_MainWindowFunctions.OpenPage(selectedItem);            
        }

        private void HideMenuButton1_Click(object sender, RoutedEventArgs e)
        {
            double mainMenuWidth1 = MainMenu1.Width;
            var mainMenuGridColumn1 = MainMenuGridColumn1.Width;

            if (mainMenuWidth1 > 0)
            {
                MainMenu1.Width = 0;
                HideMenuButton1.Content = "Show Menu ▼";        
            }
            else
            {
                MainMenu1.Width = 140;
                HideMenuButton1.Content = "Hide Menu ▲";       
            }

            MainMenuGrid1.Width = GridLength.Auto;
            MainMenuGridColumn1.Width = GridLength.Auto;
        }

        private void TopBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        
        private void OnClickMaximiseWindow(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void OnClickMinimiseWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnClickCloseWindow(object sender, RoutedEventArgs e)
        {
            DisplayExitDialog();
        }
        
        private void AboutSoftware_Click(object sender, RoutedEventArgs e)
        {
            DisplayHelpDialog();
        }
        
        private void DisplayHelpDialog()
        {
            m_HelpDialog = new HelpDialogBox();
            m_HelpDialog.ShowDialog();
        }

        private void DisplayExitDialog()
        {
            m_CloseDialog = new CloseSoftwareDialog();
            m_CloseDialog.ShowDialog();
        }

        private void ExitSoftware_Click(object sender, RoutedEventArgs e)
        {
            m_CloseDialog = new CloseSoftwareDialog();
            m_CloseDialog.ShowDialog();
        }

        private void OnClickTensorflowWebsite(object sender, RoutedEventArgs e)
        {
            HyperlinkNavigation.NavigateTo("https://www.tensorflow.org/");
        }
    }
}

