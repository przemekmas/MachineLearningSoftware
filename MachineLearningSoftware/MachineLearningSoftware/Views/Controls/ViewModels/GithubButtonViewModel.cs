using MachineLearningSoftware.Common;
using MachineLearningSoftware.Entities;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.Controls.ViewModels
{
    public class GithubButtonViewModel : NotifyPropertyChanged
    {
        public ICommand GithubButtonCommand
        {
            get { return new CommandDelegate(NavigateToGitHubRepository, CanExecute); }
        }

        private void NavigateToGitHubRepository(object context)
        {
            HyperlinkNavigation.NavigateTo("https://github.com/przemekmas?tab=repositories");
        }

        private bool CanExecute(object context)
        {
            return true;
        }
    }
}