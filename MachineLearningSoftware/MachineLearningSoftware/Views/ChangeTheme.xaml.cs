using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for ChangeTheme.xaml
    /// </summary>
    [ViewExport(typeof(ChangeTheme), typeof(IResourceItemEntity), "Change Theme", true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ChangeTheme : UserControl, IResourceItemEntity
    {
        private string _selectedTheme;

        [ImportingConstructor]
        public ChangeTheme(ChangeThemeViewModel changeThemeViewModel)
        {
            InitializeComponent();
            LoadAllThemes();
            DataContext = changeThemeViewModel;
        }

        public UserControl Page => this;

        public Control IconControl => new ThemeButtonIcon();
        
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTheme = ThemeComboBox.SelectedValue.ToString();
            LoadProperties(false, true);
        }

        private void LoadAllThemes()
        {
            LoadProperties(true, false);
        }

        private void ApplyThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTheme != null)
            {
                LoadProperties(false, false);
            }
        }

        private void LoadProperties(bool action, bool isMock)
        {
            var instance = new ThemeConstantsEntity();
            foreach (var prop in typeof(ThemeConstantsEntity).GetProperties())
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(instance, null) as ThemeEntity;

                    if (isMock)
                    {
                        if (string.Equals(_selectedTheme, value.ThemeName, StringComparison.OrdinalIgnoreCase))
                        {
                            ChangeMockTheme(value.ThemeSource);
                        }
                    }
                    else if (action)
                    {
                        ThemeComboBox.Items.Add(value.ThemeName);                        
                    }
                    else
                    {
                        if(string.Equals(_selectedTheme, value.ThemeName, StringComparison.OrdinalIgnoreCase))
                        {
                            Properties.Settings.Default["ApplicationTheme"] = value.ThemeName;
                            Properties.Settings.Default.Save();
                            ((App)Application.Current).ChangeTheme(value.ThemeSource);
                        }
                    }
                }
            }
        }

        private void ChangeMockTheme(string uri)
        {
            var dictionaryUri = new Uri(uri, UriKind.Relative);
            var resourceDict = Application.LoadComponent(dictionaryUri) as ResourceDictionary;
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
