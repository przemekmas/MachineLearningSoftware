using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
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
        private MainWindowViewModel viewModel;

        public MainWindow()
        {            
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
            LoadPanels();
        }

        private void OpenPage(string name)
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity);

            foreach (var instance in instances.OrderBy(i => i.Name))
            {
                if(string.Equals(name, instance.Name))
                {
                    var tabItems = TabControl1.Items.Count;
                    var tabItem = new TabItem();
                    var itemFrame = new Frame();
                    itemFrame.Content = instance.Page;
                    
                    tabItem.Content = itemFrame;
                    tabItem.Header = instance.Name;
                    TabControl1.Items.Insert(tabItems++, tabItem);
                }                
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ((Label)MainMenu1.SelectedItem).Content.ToString();
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

        private void CloseMainWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void MaximizeMainWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void MinimiseMainWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnClickMaximiseWindow(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(SystemCommands.MaximizeWindowCommand);
            if(SystemCommands.MaximizeWindowCommand.Equals(""))
            {

            }
            SystemCommands.MaximizeWindow(this);
        }

        private void OnClickMinimiseWindow(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnClickCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void AboutSoftware_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.MainWindowResource.SoftwareInformation);
        }

        private void LoadPanels()
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity);

            foreach (var instance in instances.OrderBy(i => i.Name))
            {
                var resources = new Label();
                resources.Content = instance.Name;
                MainMenu1.Items.Add(resources);
            }
        }
    }
}

