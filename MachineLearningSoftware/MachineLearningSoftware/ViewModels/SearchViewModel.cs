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
    public class SearchViewModel : BaseViewModel
    {
        private string _searchResults = "Displaying Search Results For \"{0}\"";
        private MainWindowFunctions _mainWindowFunctions;
        private OnlineSearchResultsDataAccess _onlineSearchResultsDataAccess;
        private ObservableCollection<SearchResultEntity> _searchResult = new ObservableCollection<SearchResultEntity>();
        private HeaderControlEntity _headerControl;
        
        public ObservableCollection<SearchResultEntity> SearchResult
        {
            get { return _searchResult; }
            set
            {
                if (_searchResult != value)
                {
                    _searchResult = value;
                    OnPropertyChanged(nameof(SearchResult));
                }
            }
        }

        [ImportingConstructor]
        public SearchViewModel(MainWindowFunctions mainWindowFunctions, OnlineSearchResultsDataAccess onlineSearchResultsDataAccess)
        {
            _headerControl = new HeaderControlEntity()
            {
                IsHeaderVisible = true,
                IsTitleVisible = true,
                IsDescriptionVisible = true,
                Title = Properties.SearchViewResource.Title
            };
            ConfigureHeaderControl(_headerControl);
            _mainWindowFunctions = mainWindowFunctions;
            _onlineSearchResultsDataAccess = onlineSearchResultsDataAccess;
        }

        public void DisplaySearchResults(string input)
        {
            IsModalVisible = true;
            Task.Run(() => DisplayAllSearchResults(input));
        }

        public void DisplaySearchResults(string input, bool showHeader)
        {
            IsModalVisible = true;
            Task.Run(() => DisplayAllSearchResults(input, showHeader));
        }

        private void DisplayAllSearchResults(string input, bool showHeader = true)
        {
            _headerControl.Description = string.Format(_searchResults, input);
            if (showHeader)
            {
                ConfigureHeaderControl(_headerControl);
            }
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
                .FirstOrDefault(x => string.Equals(x.Metadata.PageName, input, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(x.Metadata.ClassType.Name, input, StringComparison.OrdinalIgnoreCase)
                    || x.Metadata.ClassType.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase));

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
