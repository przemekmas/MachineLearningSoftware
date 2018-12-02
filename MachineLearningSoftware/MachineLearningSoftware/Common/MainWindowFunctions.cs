using MachineLearningSoftware.Controls;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views;
using MachineLearningSoftware.Views.MainView;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;

namespace MachineLearningSoftware.Common
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainWindowFunctions : NotifyPropertyChanged
    {
        MainWindowTabControl _mainWindowTabControl;

        public void SetMainWindowTabControl(MainWindowTabControl mainWindowTabControl)
        {
            _mainWindowTabControl = mainWindowTabControl;            
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
                && !string.IsNullOrEmpty(searchParameter) && instance is SearchView searchInstance)
            {
                searchInstance.SearchResult(searchParameter);
            }

            AddPage(instance, export.Metadata.PageName);
        }

        public void AddStartPage()
        {
            AddPage((IResourceItemEntity)Activator.CreateInstance(typeof(StartView)), "Start Page", false);
        }

        private void AddPage(IResourceItemEntity instance, string pageName, bool isClosable = true)
        {
            var mainWindowTabItem = new MainWindowTabItem();
            var itemFrame = new Frame();
            itemFrame.Content = SetupPageUserControls(instance);
            mainWindowTabItem.Content = itemFrame;
            mainWindowTabItem.Title = pageName;
            mainWindowTabItem.IsClosable = isClosable;
            _mainWindowTabControl.AddPage.Invoke(mainWindowTabItem);
        }

        public void LoadMenuItems(ListBox mainMenuListBox)
        {
            var exports = DependencyInjection.Container.GetExports<IResourceItemEntity, IResourceItemMetadata>();

            foreach (var export in exports)
            {
                if (export.Metadata.IsPageVisible)
                {
                    var instance = export.Value;
                    var mainMenuButton = new MainMenuButtonControl();
                    mainMenuButton.MinHeight = 40;
                    mainMenuButton.ContentPlaceholder = instance.IconControl;
                    mainMenuButton.PageTitle = export.Metadata.PageName;
                    mainMenuListBox.Items.Add(mainMenuButton);
                }
            }
        }

        private TabViewPlaceholder SetupPageUserControls(IResourceItemEntity resourceItemEntityInstance)
        {
            var pageContent = resourceItemEntityInstance.Page.Content;
            var tabViewPlaceholder = new TabViewPlaceholder();
            tabViewPlaceholder.DataContext = resourceItemEntityInstance.Page.DataContext;
            var windowHeaderControl = new WindowHeaderControl();
            tabViewPlaceholder.Header.Content = windowHeaderControl;
            tabViewPlaceholder.Main.Content = resourceItemEntityInstance.Page;
            return tabViewPlaceholder;
        }
    }
}
