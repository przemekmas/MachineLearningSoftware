using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for ChangeTheme.xaml
    /// </summary>
    public partial class ChangeTheme : Page, IResourceItemEntity
    {
        private App _currentApplication;
        private string _selectedTheme;

        public ChangeTheme()
        {
            InitializeComponent();
            LoadAllThemes();
        }

        public string Name => "Change Theme";

        public Page Page => this;

        public Control IconControl => new ThemeButtonIcon();

        public bool IsVisible => true;

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
            if(_selectedTheme != null)
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
                    else if (action == true)
                    {
                        ThemeComboBox.Items.Add(value.ThemeName);
                    }
                    else
                    {
                        if(string.Equals(_selectedTheme, value.ThemeName, StringComparison.OrdinalIgnoreCase))
                        {
                            _currentApplication = (App)Application.Current;
                            _currentApplication.ChangeTheme(value.ThemeSource);
                        }
                    }
                }
            }
        }

        private void ChangeMockTheme(string uri)
        {
            Uri dictionaryUri = new Uri(uri, UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(dictionaryUri) as ResourceDictionary;
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
