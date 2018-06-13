using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearningSoftware.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private string _searchResultText;
        private string _noSearchResults = "No Search Results For \"{0}\"";
        private string _searchResults = "Displaying Search Results For \"{0}\"";
        private MainWindowFunctions _mainWindowFunctions;
        private List<SearchResultEntity> _searchResult = new List<SearchResultEntity>();

        public string SearchResultText
        {
            get { return _searchResultText; }
            set
            {
                _searchResultText = value;
                OnPropertyChanged(nameof(SearchResultText));
            }
        }

        public List<SearchResultEntity> SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                OnPropertyChanged(nameof(SearchResult));
            }
        }

        public SearchPageViewModel()
        {
            _mainWindowFunctions = MainWindowFunctions.Instance;
        }

        public void DisplaySearchResults(string input)
        {
            DisplaySearchResult(input);
            DisplayWebSearchResults(input);
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

        private void DisplaySearchResult(string input)
        {
            var export = _mainWindowFunctions.GetAssemblyCompositionContainer().GetExports<IResourceItemEntity, IResourceItemMetadata>()
                .FirstOrDefault(x => string.Equals(x.Metadata.PageName, input, StringComparison.OrdinalIgnoreCase));

            if (export != null)
            {
                var instance = Activator.CreateInstance(export.Metadata.ClassType) as IResourceItemEntity;
                SearchResultText = string.Format(_searchResults, input);
                SearchResult.Add(new SearchResultEntity()
                {
                    Heading = export.Metadata.PageName,
                    Description = "Machine Learning Software Page",
                    Icon = instance.IconControl,
                    IsPage = true
                });             
            }
            else
            {
                SearchResultText = string.Format(_noSearchResults, input);
            }
        }
        
        private void DisplayWebSearchResults(string input)
        {
            SearchResult.AddRange(OnlineSearchResults.GetSearchResults(input));
        }
    }
}
