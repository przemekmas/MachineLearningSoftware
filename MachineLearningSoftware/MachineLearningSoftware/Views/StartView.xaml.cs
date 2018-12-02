using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Controls.ButtonIcons;
using System.Windows.Controls;
using System.Windows.Input;

namespace MachineLearningSoftware.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl, IResourceItemEntity
    {
        private readonly StartViewModel _startViewModel;

        public UserControl Page => this;

        public Control IconControl => new DefaultIcon();

        public StartView()
        {
            InitializeComponent();
            _startViewModel = DependencyInjection.ResolveSingle<StartViewModel>();
            DataContext = _startViewModel;
        }

        private void OnClickSearchResult(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is SearchResultEntity result)
            {
                _startViewModel.NavigateTo(result);
            }
        }
    }
}
