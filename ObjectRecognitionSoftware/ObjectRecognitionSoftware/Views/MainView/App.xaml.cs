using ObjectRecognitionSoftware.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeTheme(string uri)
        {
            Uri dictionaryUri = new Uri(uri, UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(dictionaryUri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
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
