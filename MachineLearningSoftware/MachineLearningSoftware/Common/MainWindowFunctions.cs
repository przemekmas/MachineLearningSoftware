using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Common
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
            var export = GetAssemblyCompositionContainer().GetExports<IResourceItemEntity, IResourceItemMetadata>()
                .FirstOrDefault(x => string.Equals(x.Metadata.PageName, name, StringComparison.OrdinalIgnoreCase));

            if (string.Equals(name, "Search", StringComparison.OrdinalIgnoreCase) 
                && !string.IsNullOrEmpty(searchParameter))
            {
                var searchInstance = new SearchPage();
                searchInstance.SearchResult(searchParameter);
                instance = searchInstance;
            }
            else
            {
                instance = Activator.CreateInstance(export.Metadata.ClassType) as IResourceItemEntity;
            }
            
            _openPages.Add(export.Metadata.PageName);
            var tabItems = TabControl.Items.Count;
            tabItems = tabItems++;
            var tabItem = new MainWindowTab(TabControl);
            var itemFrame = new Frame();
            itemFrame.Content = instance.Page;

            tabItem.MouseDown += new MouseButtonEventHandler(TabItemMouse_Click);
            tabItem.Content = itemFrame;
            tabItem.Header.Text = export.Metadata.PageName;
            TabControl.Items.Insert(tabItems, tabItem);
            TabControl.SelectedIndex = tabItems;
        }

        public void LoadPanels(ListBox mainMenuListBox)
        {
            var exports = GetAssemblyCompositionContainer().GetExports<IResourceItemEntity, IResourceItemMetadata>();
            foreach (var export in exports)
            {
                if (export.Metadata.IsPageVisible)
                {
                    var instance = Activator.CreateInstance(export.Metadata.ClassType) as IResourceItemEntity;
                    var resources = new MainMenuButtonControl();
                    resources.MinHeight = 40;
                    resources.Grid.Children.Add(instance.IconControl);
                    resources.TextBlock.Text = export.Metadata.PageName;
                    mainMenuListBox.Items.Add(resources);
                }
            }
        }

        public CompositionContainer GetAssemblyCompositionContainer()
        {
            var catalog = new AssemblyCatalog(typeof(App).Assembly);
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            container.SatisfyImportsOnce(this);
            return container;
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
