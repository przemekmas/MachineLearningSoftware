using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls;
using MachineLearningSoftware.Entities;
using System;
using System.Reflection;
using System.Windows;

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

        protected void ShowNewWindow<T>(object dataContext)
        {
            ShowWindow<T>(dataContext);
        }

        protected void ShowNewWindow<T>()
        {
            ShowWindow<T>(null);
        }

        private void ShowWindow<T>(object dataContext)
        {
            var newWindow = DependencyInjection.ResolveSingle<T>();
            if (newWindow != null && newWindow is Window)
            {
                var window = (Window)(object)newWindow;
                window.Show();
                if (dataContext != null)
                {
                    window.DataContext = dataContext;
                }
            }
        }
    }
}
