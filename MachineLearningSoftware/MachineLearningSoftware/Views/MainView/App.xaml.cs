using MachineLearningSoftware.DataAccess;
using System;
using System.Windows;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeTheme(string uri)
        {
            Uri dictionaryUri = new Uri(uri, UriKind.Relative);
            ResourceDictionary resourceDict = LoadComponent(dictionaryUri) as ResourceDictionary;
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        [STAThread]
        public static void Main()
        {
            try
            {
                InitiateClasses();
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
            catch (Exception e)
            {
                ExceptionLogging.LogException(e.ToString());
            }            
        }

        private static void InitiateClasses()
        {
            new ExceptionLogging();
        }
    }
}
