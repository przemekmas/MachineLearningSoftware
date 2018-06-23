using MachineLearningSoftware.Common;
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
        private static ExceptionLogDataAccess _exceptionLogging = DependencyInjection.ResolveSingle<ExceptionLogDataAccess>();

        public void ChangeTheme(string uri)
        {
            var dictionaryUri = new Uri(uri, UriKind.Relative);
            var resourceDict = LoadComponent(dictionaryUri) as ResourceDictionary;
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        [STAThread]
        public static void Main()
        {
            try
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
            catch (Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }
        }
    }
}
