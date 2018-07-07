using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.MainView;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MachineLearningSoftware.Views.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowTabControl.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainWindowTabControl : TabControl
    {
        public MainWindowTabControl()
        {
            InitializeComponent();
        }

        public void AddUserControlToTabControl(IResourceItemEntity instance, Lazy<IResourceItemEntity, IResourceItemMetadata> export)
        {
            var tabItemsCount = Items.Count;
            tabItemsCount = tabItemsCount++;
            var mainWindowTabItem = new MainWindowTabItem();
            var itemFrame = new Frame();
            itemFrame.Content = SetupPageUserControls(instance);
            mainWindowTabItem.Content = itemFrame;
            mainWindowTabItem.Header.Text = export.Metadata.PageName;
            Items.Insert(tabItemsCount, mainWindowTabItem);
            SelectedIndex = tabItemsCount;
        }

        private TabViewPlaceholder SetupPageUserControls(IResourceItemEntity resourceItemEntityInstance)
        {
            var pageContent = resourceItemEntityInstance.Page.Content;
            var tabViewPlaceholder = new TabViewPlaceholder();
            tabViewPlaceholder.DataContext = resourceItemEntityInstance.Page.DataContext;
            var windowHeaderControl = new WindowHeaderControl();
            var tabViewContent = (tabViewPlaceholder.Content as Grid);
            tabViewContent.Children.Add(windowHeaderControl);
            tabViewContent.Children.Add(resourceItemEntityInstance.Page);
            Grid.SetRow(windowHeaderControl, 0);
            Grid.SetRow(resourceItemEntityInstance.Page, 1);
            return tabViewPlaceholder;
        }
    }
}
