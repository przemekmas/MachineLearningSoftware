using ObjectRecognitionSoftware.Entities;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for ChangeTheme.xaml
    /// </summary>
    public partial class ChangeTheme : Page, IResourceItemEntity
    {
        private App m_CurrentApplication;
        private string m_SelectedTheme;

        public ChangeTheme()
        {
            InitializeComponent();
            LoadAllThemes();
        }

        public string Name => "Change Theme";

        public Page Page => this;

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_SelectedTheme = ThemeComboBox.SelectedValue.ToString();
        }

        private void LoadAllThemes()
        {
            LoadProperties(true);
        }

        private void ApplyThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if(m_SelectedTheme != null)
            {
                LoadProperties(false);
            }
        }

        private void LoadProperties(bool action)
        {
            var instance = new ThemeConstantsEntity();
            foreach (var prop in typeof(ThemeConstantsEntity).GetProperties())
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(instance, null) as ThemeEntity;

                    if (action == true)
                    {
                        ThemeComboBox.Items.Add(value.themeName);
                    }
                    else
                    {
                        if(string.Equals(m_SelectedTheme, value.themeName, StringComparison.OrdinalIgnoreCase))
                        {
                            m_CurrentApplication = (App)Application.Current;
                            m_CurrentApplication.ChangeTheme(value.themeSource);
                        }
                    }
                }
            }
        }
    }
}
