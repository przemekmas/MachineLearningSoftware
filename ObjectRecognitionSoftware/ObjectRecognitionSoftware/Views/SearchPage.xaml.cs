using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using System.Windows.Controls;
using System.Windows.Input;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page, IResourceItemEntity
    {
        private SearchPageViewModel m_ViewModel;

        public SearchPage()
        {
            InitializeComponent();
            m_ViewModel = new SearchPageViewModel();
            this.DataContext = m_ViewModel;
        }

        public string Name => "Search";

        public Page Page => this;

        public Control IconControl => new DefaultIcon();

        public void SearchResult(string input)
        {
            m_ViewModel.DisplaySearchResults(input);
        }
        
        private void SelectedResult(object sender, SelectionChangedEventArgs e)
        {
            m_ViewModel.NavigateTo(SearchResultList.SelectedItem as SearchResultEntity);
        }
    }
}
