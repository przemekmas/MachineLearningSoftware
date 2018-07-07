using MachineLearningSoftware.Common;
using MachineLearningSoftware.ViewModels;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.Views.Controls.ViewModels
{
    [Export]
    public class MainMenuSearchButtonViewModel : BaseViewModel
    {
        private string _searchViewName = "Search";
        private MainWindowFunctions _mainWindowFunctions;

        [ImportingConstructor]
        public MainMenuSearchButtonViewModel(MainWindowFunctions mainWindowFunctions)
        {
            _mainWindowFunctions = mainWindowFunctions;
        }

        public void Search(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                _mainWindowFunctions.OpenPage(_searchViewName, input);
            }
        }       
    }
}