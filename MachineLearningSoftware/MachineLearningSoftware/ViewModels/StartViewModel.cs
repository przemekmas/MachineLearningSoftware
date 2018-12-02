using MachineLearningSoftware.Common;
using System;
using System.ComponentModel.Composition;

namespace MachineLearningSoftware.ViewModels
{
    [Export]
    public class StartViewModel : SearchViewModel
    {
        private Action<string> _searchControlAction;

        public Action<string> SearchControlAction
        {
            get { return _searchControlAction; }
            set
            {
                if (_searchControlAction != value)
                {
                    _searchControlAction = value;
                    OnPropertyChanged(nameof(SearchControlAction));
                }
            }
        }

        [ImportingConstructor]
        public StartViewModel(MainWindowFunctions mainWindowFunctions, OnlineSearchResultsDataAccess onlineSearchResultsDataAccess)
            : base(mainWindowFunctions, onlineSearchResultsDataAccess)
        {
            ConfigureHeaderControl(false);
            SearchControlAction = DisplayStartViewResults;
        }

        private void DisplayStartViewResults(string searchInput)
        {
            DisplaySearchResults(searchInput, false);
        }
    }
}
