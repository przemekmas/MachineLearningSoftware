using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.ViewModels;
using ObjectRecognitionSoftware.Views.Controls.ButtonIcons;
using System.Windows.Controls;

namespace ObjectRecognitionSoftware
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page, IResourceItemEntity
    {
        private SearchPageViewModel _viewModel;

        public SearchPage()
        {
            InitializeComponent();
            _viewModel = new SearchPageViewModel();
            this.DataContext = _viewModel;
        }

        public string Name => "Search";

        public Page Page => this;

        public Control IconControl => new DefaultIcon();

        public void SearchResult(string input)
        {
            _viewModel.DisplaySearchResults(input);
        }
        
        private void SelectedResult(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.NavigateTo(SearchResultList.SelectedItem as SearchResultEntity);
        }
    }
}
