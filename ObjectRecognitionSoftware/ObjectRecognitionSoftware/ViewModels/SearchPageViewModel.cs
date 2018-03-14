using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class SearchPageViewModel : NotifyPropertyChanged
    {
        private string m_SearchResultText;
        private string m_NoSearchResults = "No Search Results For \"{0}\"";
        private string m_SearchResults = "Displaying Search Results For \"{0}\"";
        private MainWindowFunctions m_MainWindowFunctions;
        private List<SearchResultEntity> m_SearchResult = new List<SearchResultEntity>();

        public string SearchResultText
        {
            get { return m_SearchResultText; }
            set
            {
                m_SearchResultText = value;
                OnPropertyChanged(nameof(SearchResultText));
            }
        }

        public List<SearchResultEntity> SearchResult
        {
            get { return m_SearchResult; }
            set
            {
                m_SearchResult = value;
                OnPropertyChanged(nameof(SearchResult));
            }
        }

        public SearchPageViewModel()
        {
            m_MainWindowFunctions = MainWindowFunctions.Instance;
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
                m_MainWindowFunctions.OpenPage(clickedResult.Heading);
            }
            else
            {
                HyperlinkNavigation.NavigateTo(clickedResult.Url);
            }
        }

        private void DisplaySearchResult(string input)
        {
            var instances = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IResourceItemEntity))
                                && t.GetConstructor(Type.EmptyTypes) != null).Select(t => Activator.CreateInstance(t) as IResourceItemEntity)
                                .Where(t => string.Equals(t.Name, input, StringComparison.OrdinalIgnoreCase) && t.IsVisible == true);
            
            if (instances != null)
            {
                SearchResultText = string.Format(m_SearchResults, input);
                foreach (var instance in instances)
                {
                    SearchResult.Add(new SearchResultEntity()
                    {
                        Heading = instance.Name,
                        Description = "Object Class Recognition Page",
                        Icon = instance.IconControl,
                        IsPage = true
                    });
                }                
            }
            else
            {
                SearchResultText = string.Format(m_NoSearchResults, input);
            }
        }
        
        private void DisplayWebSearchResults(string input)
        {
            SearchResult.AddRange(OnlineSearchResults.GetSearchResults(input));
        }
    }
}
