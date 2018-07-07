namespace MachineLearningSoftware.Entities
{
    public class HeaderControlEntity : NotifyPropertyChanged
    {
        private bool _isHeaderVisible;
        private bool _isTitleVisible;
        private bool _isDescriptionVisible;
        private string _title;
        private string _description;

        public bool IsHeaderVisible
        {
            get { return _isHeaderVisible; }
            set
            {
                if (_isHeaderVisible != value)
                {
                    _isHeaderVisible = value;
                    OnPropertyChanged(nameof(IsHeaderVisible));
                }
            }
        }

        public bool IsTitleVisible
        {
            get { return _isTitleVisible; }
            set
            {
                if (_isTitleVisible != value)
                {
                    _isTitleVisible = value;
                    OnPropertyChanged(nameof(IsTitleVisible));
                }
            }
        }

        public bool IsDescriptionVisible
        {
            get { return _isDescriptionVisible; }
            set
            {
                if (_isDescriptionVisible != value)
                {
                    _isDescriptionVisible = value;
                    OnPropertyChanged(nameof(IsDescriptionVisible));
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
    }
}
