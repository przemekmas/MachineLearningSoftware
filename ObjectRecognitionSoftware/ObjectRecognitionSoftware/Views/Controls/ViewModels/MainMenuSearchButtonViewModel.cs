using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;

namespace ObjectRecognitionSoftware.Views.Controls.ViewModels
{
    public class MainMenuSearchButtonViewModel : NotifyPropertyChanged
    {
        private string _searchPageName = "Search";
        private MainWindowFunctions _mainWindowFunctions;

        public MainMenuSearchButtonViewModel()
        {
            _mainWindowFunctions = MainWindowFunctions.Instance;
        }

        public void Search(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                _mainWindowFunctions.OpenPage(_searchPageName, input);
            }
        }       
    }
}