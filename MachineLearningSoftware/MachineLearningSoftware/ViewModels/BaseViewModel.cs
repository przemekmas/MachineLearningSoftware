using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.Views.DialogBoxes;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace MachineLearningSoftware.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        private bool _isModalVisible;
        private HeaderControlEntity _headerControl;
        private double _modalPercentage;
        private bool _isProgressModalVisible;
        private int _operationTotalCount;
        private int _currentOperation;
        private string _informationWindowText;
        private InformationWindow _informationWindow;

        public string ModalPercentageProgress
        {
            get { return string.Format(CultureInfo.InvariantCulture, Properties.LoadingDialogBoxResource.EstimatedTimeMessage, 
                ModalPercentage.ToString("0.##")); }
        }

        public ICommand CloseInformationWindowCommand
        {
            get { return new CommandDelegate(CloseInformationWindow, CanExecute); }
        }
        
        public double ModalPercentage
        {
            get { return _modalPercentage; }
            set
            {
                if (_modalPercentage != value)
                {
                    _modalPercentage = value;
                    OnPropertyChanged(nameof(ModalPercentage));
                    OnPropertyChanged(nameof(ModalPercentageProgress));
                }
            }
        }
        
        public string InformationWindowText
        {
            get { return _informationWindowText; }
            set
            {
                if (_informationWindowText != value)
                {
                    _informationWindowText = value;
                    OnPropertyChanged(nameof(InformationWindowText));
                }
            }
        }

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

        public bool IsProgressModalVisible
        {
            get { return _isProgressModalVisible; }
            set
            {
                _isProgressModalVisible = value;
                OnPropertyChanged(nameof(IsProgressModalVisible));
            }
        }

        public BaseViewModel()
        {
            ConfigureHeaderControl(false);
        }

        public double ConvertToPercentage(int currentIteration, int collectionSize)
        {
            var cIteration = Convert.ToDouble(currentIteration);
            var cSize = Convert.ToDouble(collectionSize);

            return (cIteration / cSize) * 100; ;
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

        protected object ShowNewWindow<T>(object dataContext)
        {
            return ShowWindow<T>(dataContext);
        }

        protected void ShowNewWindow<T>()
        {
            ShowWindow<T>(null);
        }

        protected void ShowInformationWindow(string information, object dataContext)
        {
            InformationWindowText = information;
            if (ShowNewWindow<InformationWindow>(dataContext) is InformationWindow informationWindow)
            {
                _informationWindow = informationWindow;
            }
        }

        private void CloseInformationWindow(object obj)
        {
            _informationWindow.Close();
        }

        private object ShowWindow<T>(object dataContext)
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
                return newWindow;
            }
            return null;
        }
    }
}
