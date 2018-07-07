using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MachineLearningSoftware.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExceptionLogViewModel : BaseViewModel
    {
        private ObservableCollection<ExceptionEntity> _exceptionList;
        private ExceptionLogDataAccess _exceptionLogging;
        private string _exceptionCount;
        private string _exceptionDetails;

        public ObservableCollection<ExceptionEntity> ExceptionList
        {
            get { return _exceptionList; }
            set
            {
                _exceptionList = value;
                OnPropertyChanged(nameof(ExceptionList));
            }
        }

        public string ExceptionCount
        {
            get { return _exceptionCount; }
            set
            {
                _exceptionCount = value;
                OnPropertyChanged(nameof(ExceptionCount));
            }
        }

        public string ExceptionDetails
        {
            get { return _exceptionDetails; }
            set
            {
                _exceptionDetails = value;
                OnPropertyChanged(nameof(ExceptionDetails));
            }
        }
        
        [ImportingConstructor]
        public ExceptionLogViewModel(ExceptionLogDataAccess exceptionLogging)
        {
            ConfigureHeaderControl(true, true, Properties.ExceptionLogResource.Title);
            _exceptionLogging = exceptionLogging;
            IsModalVisible = true;
            Task.Run(() => SetExceptions());
        }
                
        public void InitiateTimer()
        {
            var timerEvent = new TimerEvent();
            timerEvent.InititateTimer(1000, OnUpdateEvent);
        }

        public void SetExceptionDetails(string exception)
        {
            ExceptionDetails = exception;
        }

        private void OnUpdateEvent(Object source, ElapsedEventArgs e)
        {
            if(ExceptionList.Count < _exceptionLogging.GetExceptions().Count)
            {
                ExceptionList = _exceptionLogging.GetExceptions();
                UpdateExceptionCount(ExceptionList.Count);
            }            
        }

        private void UpdateExceptionCount(int exceptionValue)
        {
            ExceptionCount = string.Format(Properties.ExceptionLogResource.ExceptionCount, exceptionValue);
        }

        private void SetExceptions()
        {
            Thread.Sleep(100);
            ExceptionList = _exceptionLogging.GetExceptions();
            UpdateExceptionCount(ExceptionList.Count);
            InitiateTimer();
            IsModalVisible = false;
        }
    }
}
