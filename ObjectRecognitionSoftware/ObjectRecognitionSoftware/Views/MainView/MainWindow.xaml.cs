using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls;
using ObjectRecognitionSoftware.Views.DialogBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private List<string> m_OpenPages;

        public MainWindow()
        {            
            InitializeComponent();
            m_ViewModel = new MainWindowViewModel();
            m_OpenPages = new List<string>();
            this.DataContext = m_ViewModel;
            LoadPanels();
        }
        
        private void OpenPage(string name)
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity);

            foreach (var instance in instances.OrderBy(i => i.Name))
            {
                if(string.Equals(name, instance.Name) && !m_OpenPages.Contains(instance.Name))
                {
                    m_OpenPages.Add(instance.Name);
                    var tabItems = TabControl1.Items.Count;
                    var tabItem = new TabItem();
                    var itemFrame = new Frame();
                    itemFrame.Content = instance.Page;

                    tabItem.MouseDown += new MouseButtonEventHandler(TabItemMouse_Click);
                    tabItem.Content = itemFrame;
                    tabItem.Header = instance.Name;
                    TabControl1.Items.Insert(tabItems++, tabItem);
                }                
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((MainMenuButtonControl)MainMenu1.SelectedItem).TextBlock.Text.ToString();
            OpenPage(selectedItem);            
        }

        private void HideMenuButton1_Click(object sender, RoutedEventArgs e)
        {
            double mainMenuWidth1 = MainMenu1.Width;
            var mainMenuGridColumn1 = MainMenuGridColumn1.Width;
            
            //var showStyle = this.FindResource("ShowMainMenuButtons") as Style;
            //var hideStyle = this.FindResource("HideMainMenuButtons") as Style;

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

        private void TabItemMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle &&
                e.ButtonState == MouseButtonState.Pressed)
            {
                TabControl1.Items.Remove(sender);
                var tabItem = sender as TabItem;
                m_OpenPages.Remove(tabItem.Header.ToString());
            }
        }

        private void LoadPanels()
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity);

            foreach (var instance in instances.OrderBy(i => i.Name))
            {
                var resources = new MainMenuButtonControl();
                //resources.Width = 120;
                resources.MinHeight = 40;
                //resources.TextWrapping = TextWrapping.Wrap;
                resources.Grid.Children.Add(instance.IconControl);
                //resources.ControlPlaceholder = instance.IconControl;
                resources.TextBlock.Text = instance.Name;
                MainMenu1.Items.Add(resources);
            }
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
    }
}

