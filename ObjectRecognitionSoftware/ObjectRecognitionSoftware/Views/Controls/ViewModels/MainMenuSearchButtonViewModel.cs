using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;

namespace ObjectRecognitionSoftware.Views.Controls.ViewModels
{
    public class MainMenuSearchButtonViewModel : NotifyPropertyChanged
    {
        private string m_SearchPageName = "Search";
        private MainWindowFunctions m_MainWindowFunctions;

        public MainMenuSearchButtonViewModel()
        {
            m_MainWindowFunctions = MainWindowFunctions.Instance;
        }

        public void Search(string input)
        {
            if (string.IsNullOrEmpty(input))
            {

            }
            else
            {
                m_MainWindowFunctions.OpenPage(m_SearchPageName, input);
            }
        }       
    }
}