using MachineLearningSoftware.Entities;

namespace MachineLearningSoftware.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        private bool _isModalVisible;
        private HeaderControlEntity _headerControl;

        public HeaderControlEntity HeaderControl
        {
            get { return _headerControl; }
            set
            {
                if (_headerControl != value)
                {
                    _headerControl = value;
                    OnPropertyChanged(nameof(HeaderControl));
                }
            }
        }

        public bool IsModalVisible
        {
            get { return _isModalVisible; }
            set
            {
                _isModalVisible = value;
                OnPropertyChanged(nameof(IsModalVisible));
            }
        }

        public BaseViewModel()
        {
            ConfigureHeaderControl(false);
        }

        protected bool CanExecute(object context)
        {
            return true;
        }

        protected void ConfigureHeaderControl(HeaderControlEntity headerControl)
        {
            HeaderControl = headerControl;
        }

        protected void ConfigureHeaderControl(bool isHeaderVisible, bool isTitleVisible = false, string title = null, bool isDescriptionVisible = false,
            string description = null)
        {
            var headerControl = new HeaderControlEntity()
            {
                IsHeaderVisible = isHeaderVisible,
                IsTitleVisible = isTitleVisible,
                IsDescriptionVisible = isDescriptionVisible,
                Title = title,
                Description = description
            };
            HeaderControl = headerControl;
        }
    }
}
