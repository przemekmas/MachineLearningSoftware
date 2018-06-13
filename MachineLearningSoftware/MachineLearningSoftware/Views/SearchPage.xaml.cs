using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    [ViewExport(typeof(SearchPage), typeof(IResourceItemEntity), "Search", false)]
    public partial class SearchPage : Page, IResourceItemEntity
    {
        private SearchPageViewModel _viewModel;

        public SearchPage()
        {
            InitializeComponent();
            _viewModel = new SearchPageViewModel();
            DataContext = _viewModel;
        }

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
