using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls;
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

            _mainWindowTabControl.AddUserControlToTabControl(instance, export);
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
                    mainMenuButton.Grid.Children.Add(instance.IconControl);
                    mainMenuButton.TextBlock.Text = export.Metadata.PageName;
                    mainMenuListBox.Items.Add(mainMenuButton);
                }
            }
        }
    }
}
