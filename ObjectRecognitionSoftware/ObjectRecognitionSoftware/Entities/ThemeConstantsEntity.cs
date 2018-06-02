namespace ObjectRecognitionSoftware.Entities
{
    public class ThemeConstantsEntity
    {
        public ThemeEntity BlackTheme { get; } = new ThemeEntity() { ThemeName = "Black Theme", ThemeSource = "Views/Themes/BlackTheme.xaml" };

        public ThemeEntity BlueTheme { get; } = new ThemeEntity() { ThemeName = "Blue Theme", ThemeSource = "Views/Themes/BlueTheme.xaml" };

        public ThemeEntity TensorFlowTheme { get; } = new ThemeEntity() { ThemeName = "TensorFlow Theme", ThemeSource = "Views/Themes/TensorFlowTheme.xaml" };

        public ThemeEntity GradientTheme { get; } = new ThemeEntity() { ThemeName = "Gradient Theme", ThemeSource = "Views/Themes/GradientTheme.xaml" };
    }
}
