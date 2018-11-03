using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.Controls.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HidePanelViewModel : BaseViewModel
    {
        private bool _isPanelVisible;

        public bool IsPanelVisible
        {
            get { return _isPanelVisible; }
            set
            {
                if (_isPanelVisible != value)
                {
                    _isPanelVisible = value;
                    OnPropertyChanged(nameof(IsPanelVisible));
                }
            }
        }

        public ICommand ShowHidePanelCommand
        {
            get { return new CommandDelegate(ShowHidePanel, CanExecute); }
        }

        public HidePanelViewModel()
        {
            IsPanelVisible = true;
        }

        private void ShowHidePanel(object context)
        {
            IsPanelVisible = !IsPanelVisible;
        }
    }
}
