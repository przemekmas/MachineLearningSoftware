using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Common
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainWindowFunctions : NotifyPropertyChanged
    {
        private TabControl _tabControl;
        private List<string> _openPages = new List<string>();
        
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
            var export = DependencyInjection.Container.GetExports<IResourceItemEntity, IResourceItemMetadata>()
                .FirstOrDefault(x => string.Equals(x.Metadata.PageName, name, StringComparison.OrdinalIgnoreCase));
            instance = export.Value;
            
            if (string.Equals(name, "Search", StringComparison.OrdinalIgnoreCase) 
                && !string.IsNullOrEmpty(searchParameter) && instance is SearchPage searchInstance)
            {
                searchInstance.SearchResult(searchParameter);
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
            var exports = DependencyInjection.Container.GetExports<IResourceItemEntity, IResourceItemMetadata>();

            foreach (var export in exports)
            {
                if (export.Metadata.IsPageVisible)
                {
                    var instance = export.Value;
                    var resources = new MainMenuButtonControl();
                    resources.MinHeight = 40;
                    resources.Grid.Children.Add(instance.IconControl);
                    resources.TextBlock.Text = export.Metadata.PageName;
                    mainMenuListBox.Items.Add(resources);
                }
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
