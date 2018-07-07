using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace MachineLearningSoftware
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    [ViewExport(typeof(SearchView), typeof(IResourceItemEntity), "Search", false)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SearchView : UserControl, IResourceItemEntity
    {
        private SearchViewModel _viewModel;

        [ImportingConstructor]
        public SearchView(SearchViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        public UserControl Page => this;

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
