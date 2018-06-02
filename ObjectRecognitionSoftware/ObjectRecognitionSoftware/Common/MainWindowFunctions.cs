using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognitionSoftware.Common
{
    public class MainWindowFunctions : NotifyPropertyChanged
    {
        private static MainWindowFunctions _instance;
        private TabControl _tabControl;
        private List<string> _openPages = new List<string>();

        public MainWindowFunctions()
        {

        }

        public static MainWindowFunctions Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainWindowFunctions();
                }
                return _instance;
            }
        }

        public TabControl TabControl
        {
            get { return _tabControl; }
            set
            {
                _tabControl = value;
                OnPropertyChanged(nameof(TabControl));
            }
        }

        public void OpenPage(string name)
        {
            OpenPage(name, string.Empty);
        }

        public void OpenPage(string name, string searchParameter)
        {
            IResourceItemEntity instance;
            if (string.Equals(name, "Search", StringComparison.OrdinalIgnoreCase) 
                && !string.IsNullOrEmpty(searchParameter))
            {
                var searchInstance = new SearchPage();
                searchInstance.SearchResult(searchParameter);
                instance = searchInstance;
            }
            else
            {
                instance = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity).FirstOrDefault(t => string.Equals(t.Name, name));
            }
            
            _openPages.Add(instance.Name);
            var tabItems = TabControl.Items.Count;
            tabItems = tabItems++;
            var tabItem = new MainWindowTab(TabControl);
            var itemFrame = new Frame();
            itemFrame.Content = instance.Page;

            tabItem.MouseDown += new MouseButtonEventHandler(TabItemMouse_Click);
            tabItem.Content = itemFrame;
            tabItem.Header.Text = instance.Name;
            TabControl.Items.Insert(tabItems, tabItem);
            TabControl.SelectedIndex = tabItems;
        }

        public void LoadPanels(ListBox mainMenuListBox)
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity);

            foreach (var instance in instances.OrderBy(i => i.Name).Where(i => i.IsVisible == true))
            {
                var resources = new MainMenuButtonControl();
                resources.MinHeight = 40;
                resources.Grid.Children.Add(instance.IconControl);
                resources.TextBlock.Text = instance.Name;
                mainMenuListBox.Items.Add(resources);
            }
        }

        private void TabItemMouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle &&
                e.ButtonState == MouseButtonState.Pressed)
            {
                TabControl.Items.Remove(sender);
                var tabItem = sender as MainWindowTab;
                _openPages.Remove(tabItem.Header.ToString());
            }
        }
    }
}
