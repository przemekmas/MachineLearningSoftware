using MachineLearningSoftware.Entities;

namespace MachineLearningSoftware.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        private bool _isModalVisible;

        public bool IsModalVisible
        {
            get { return _isModalVisible; }
            set
            {
                _isModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        protected bool CanExecute(object context)
        {
            return true;
        }
    }
}
