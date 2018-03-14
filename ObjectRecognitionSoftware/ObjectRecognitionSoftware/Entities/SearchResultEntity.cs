using System.Windows.Controls;

namespace ObjectRecognitionSoftware.Entities
{
    public class SearchResultEntity : NotifyPropertyChanged
    {
        private string m_Heading;
        private string m_Description;
        private string m_Url;
        private bool m_IsPage;
        private Control m_Icon;
        
        public string Heading
        {
            get { return m_Heading; }
            set
            {
                m_Heading = value;
                OnPropertyChanged(nameof(Heading));
            }
        }

        public string Description
        {
            get { return m_Description; }
            set
            {
                m_Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public Control Icon
        {
            get { return m_Icon; }
            set
            {
                m_Icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public string Url
        {
            get { return m_Url; }
            set
            {
                m_Url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public bool IsPage
        {
            get { return m_IsPage; }
            set
            {
                m_IsPage = value;
                OnPropertyChanged(nameof(IsPage));
            }
        }
    }
}
