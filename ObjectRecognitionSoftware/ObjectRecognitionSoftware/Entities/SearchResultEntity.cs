using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Entities
{
    public class SearchResultEntity : NotifyPropertyChanged
    {
        private string _heading;
        private string _description;
        private string _url;
        private bool _isPage;
        private Control _icon;
        
        public string Heading
        {
            get { return _heading; }
            set
            {
                _heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public Control Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public bool IsPage
        {
            get { return _isPage; }
            set
            {
                _isPage = value;
                OnPropertyChanged(nameof(IsPage));
            }
        }
    }
}
