namespace ObjectRecognitionSoftware.Entities
{
    public class ThemeConstantsEntity
    {
        private ThemeEntity m_BlackTheme = new ThemeEntity() { themeName = "Black Theme", themeSource = "Views/Themes/BlackTheme.xaml" };
        private ThemeEntity m_BlueTheme = new ThemeEntity() { themeName = "Blue Theme", themeSource = "Views/Themes/BlueTheme.xaml" };
        private ThemeEntity m_TensorFlowTheme = new ThemeEntity() { themeName = "TensorFlow Theme", themeSource = "Views/Themes/TensorFlowTheme.xaml" };

        public ThemeEntity BlackTheme
        {
            get { return m_BlackTheme; }
        }

        public ThemeEntity BlueTheme
        {
            get { return m_BlueTheme; }
        }

        public ThemeEntity TensorFlowTheme
        {
            get { return m_TensorFlowTheme; }
        }
    }
}
