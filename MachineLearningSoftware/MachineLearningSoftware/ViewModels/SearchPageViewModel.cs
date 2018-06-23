using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MachineLearningSoftware.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchPageViewModel : BaseViewModel
    {
        private string _searchResultText;
        private string _searchResults = "Displaying Search Results For \"{0}\"";
        private MainWindowFunctions _mainWindowFunctions;
        private OnlineSearchResultsDataAccess _onlineSearchResultsDataAccess;
        private ObservableCollection<SearchResultEntity> _searchResult = new ObservableCollection<SearchResultEntity>();

        public string SearchResultText
        {
            get { return _searchResultText; }
            set
            {
                _searchResultText = value;
                OnPropertyChanged(nameof(SearchResultText));
            }
        }

        public ObservableCollection<SearchResultEntity> SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                OnPropertyChanged(nameof(SearchResult));
            }
        }

        [ImportingConstructor]
        public SearchPageViewModel(MainWindowFunctions mainWindowFunctions, OnlineSearchResultsDataAccess onlineSearchResultsDataAccess)
        {
            _mainWindowFunctions = mainWindowFunctions;
            _onlineSearchResultsDataAccess = onlineSearchResultsDataAccess;
        }

        public void DisplaySearchResults(string input)
        {
            IsModalVisible = true;
            Task.Run(() => DisplayAllSearchResults(input));
        }

        private void DisplayAllSearchResults(string input)
        {
            SearchResultText = string.Format(_searchResults, input);
            SearchResult = new ObservableCollection<SearchResultEntity>(_onlineSearchResultsDataAccess.GetSearchResults(input));
            DisplayMatchingPage(input);
            MovePageToTop();
            IsModalVisible = false;
        }

        private void MovePageToTop()
        {
            var result = SearchResult.Where(x => x.IsPage).FirstOrDefault();
            if (result != null)
            {
                var pageIndex = SearchResult.IndexOf(result);
                Application.Current.Dispatcher.Invoke(() => SearchResult.Move(pageIndex, 0));
            }            
        }

        public void NavigateTo(SearchResultEntity clickedResult)
        {
            if (clickedResult.IsPage)
            {
                _mainWindowFunctions.OpenPage(clickedResult.Heading);
            }
            else
            {
                HyperlinkNavigation.NavigateTo(clickedResult.Url);
            }
        }

        private void DisplayMatchingPage(string input)
        {
            var export = DependencyInjection.Container.GetExports<IResourceItemEntity, IResourceItemMetadata>()
                .FirstOrDefault(x => string.Equals(x.Metadata.PageName, input, StringComparison.OrdinalIgnoreCase));

            if (export != null)
            {
                Application.Current.Dispatcher.Invoke(() => ApplyMachingPageSearchResult(input, export));
            }
        }

        private void ApplyMachingPageSearchResult(string input, Lazy<IResourceItemEntity, IResourceItemMetadata> export)
        {
            var instance = export.Value;
            SearchResult.Add(new SearchResultEntity()
            {
                Heading = export.Metadata.PageName,
                Description = "Machine Learning Software Page",
                Icon = instance.IconControl,
                IsPage = true
            });
        }
    }
}
